using PictureHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PicturesSorter
{
    using System.Diagnostics;
    using System.Drawing;
    using System.Text;
    using System.Threading.Tasks;
    using MoreLinq;

    public partial class SimilarPicturesForm : Form
    {
        const int PICTURE_WIDTH = 250;
        const int PICTURE_HEIGHT = 250;
        const int MAX_TASKS = 8;
        readonly List<PictureSignature> _signatures = new List<PictureSignature>();
        double _similarityFactor = .95;
        bool _formIsAlive = true;

        public static bool MuteDialogs { get; set; }

        public SimilarPicturesForm() => InitializeComponent();

        readonly HashSet<PictureSignature> _distinctSignatures = new HashSet<PictureSignature>();

        readonly Dictionary<PictureSignature, List<PictureSignature>> _similarSignatures =
            new Dictionary<PictureSignature, List<PictureSignature>>(new PictureSignatureComparer());

        void IncrementProgress()
        {
            if (ProgressBar.InvokeRequired)
                new Task(()=> ProgressBar.Invoke(new Action(IncrementProgress))).Start();
            else
                ProgressBar.Value += 1;
        }

        Color InterpolateColor(Color lowResColor, Color hiResColor, long minLength, long maxLength, FileInfo fi)
        {
            var percent = Math.Abs(Math.Log(1d * fi.Length / minLength) / Math.Log(1d * maxLength / minLength));
            Debug.Assert(percent >= 0 && percent <= 1);
            var color = InterpolateColor(lowResColor, hiResColor,
                maxLength == minLength ? 1
                : fi.Length == minLength ? 0
                : percent);
            Console.WriteLine(
                $"{fi.Name} : InterpolateColor({lowResColor}, {hiResColor}, {minLength}, {maxLength}, {fi.Length})[%={percent}]={color}");
            return color;
        }

        Color InterpolateColor(Color lowResColor, Color hiResColor, double percent)
            => percent > 1 || percent < 0
                ? throw new InvalidOperationException($"Invalid percentage {percent} in colors interpolation")
                : Color.FromArgb(
                    (int)(lowResColor.R + percent * (hiResColor.R - lowResColor.R)),
                    (int)(lowResColor.G + percent * (hiResColor.G - lowResColor.G)),
                    (int)(lowResColor.B + percent * (hiResColor.B - lowResColor.B))
                );

        void DisplaySignatures(Dictionary<PictureSignature, List<PictureSignature>> similarSignatures)
        {
            var lowResColor = Color.Red;
            var hiResColor = Color.FromArgb(0, 255, 0);
            // look for 2 or more similar pictures already displayed: adding 1
            var y = 0;
            foreach (var g in similarSignatures.Values)
            {
                var maxLength = g.Max(ss => ss.FileInfo.Length);
                var minLength = g.Min(ss => ss.FileInfo.Length);

                var x = 0;
                foreach (var s in g.OrderByDescending(p => p.FileInfo.Length))
                    s.PictureBox = CreatePictureBox(
                        s.SetLocation(x++ * PICTURE_WIDTH, y * PICTURE_HEIGHT),
                        PICTURE_WIDTH, PICTURE_HEIGHT,
                        s.FileInfo.Length < maxLength,
                        maxLength == minLength
                            ? hiResColor
                            : InterpolateColor(lowResColor, hiResColor, minLength, maxLength,
                                s.FileInfo));
                y++;
            }
        }


        void ReceiveSignatureNew(PictureSignature newSignature)
        {
            Console.WriteLine($"Got {newSignature.FileInfo.FullName}");
            IncrementProgress();
            var handled = false;
            lock (_similarSignatures)
            {
                // look for 2 or more similar pictures already displayed: adding 1
                foreach (var s in _similarSignatures.Keys
                             .Where(s => s.GetSimilarityWith(newSignature) > _similarityFactor))
                {
                    Console.WriteLine($"    Found similar with {s.FileInfo.Name}. {_similarSignatures[s].Count} pre-existing.");
                    _similarSignatures[s].Add(newSignature);
                    handled = true;
                }

                if (!handled) // look for one similar yet to be displayed picture: adding 2
                    foreach (var previous in _distinctSignatures.ToArray())
                    {
                        if (previous.GetSimilarityWith(newSignature) > _similarityFactor)
                        {
                            Console.WriteLine($"    Found similar with {previous.FileInfo.Name}. New.");
                            _similarSignatures.Add(previous, new[] { previous, newSignature }.ToList());
                            Console.WriteLine($"    {previous.FileInfo.Name} removed from distincts.");
                            _distinctSignatures.Remove(previous);
                            handled = true;
                        }
                    }
                if (!handled) // this is a brand new signature
                {
                    Console.WriteLine($"    {newSignature.FileInfo.Name} added to distincts.");
                    _distinctSignatures.Add(newSignature);
                }
            }
        }

        void ReceiveSignature(PictureSignature newSignature)
        {
            var lowResColor = Color.Red;
            var hiResColor = Color.FromArgb(0, 255, 0);
            var actions = new List<Action>();
            Console.WriteLine($"Got {newSignature.FileInfo.FullName}");
            IncrementProgress();
            var handled = false;
            lock (_similarSignatures)
            {
                // look for 2 or more similar pictures already displayed: adding 1
                foreach (var s in _similarSignatures.Keys
                             .Where(s => s.GetSimilarityWith(newSignature) > _similarityFactor))
                {
                    Console.WriteLine($"    Found similar with {s.FileInfo.Name}. {_similarSignatures[s].Count} pre-existing.");
                    _similarSignatures[s].Add(newSignature);
                    var maxLength = _similarSignatures[s].Max(ss => ss.FileInfo.Length);
                    var minLength = _similarSignatures[s].Min(ss => ss.FileInfo.Length);

                    actions.Add(() =>
                    {
                        newSignature.PictureBox = CreatePictureBox(
                            newSignature.SetLocation((_similarSignatures[s].Count-1) * PICTURE_WIDTH, s.Location.Y),
                            PICTURE_WIDTH, PICTURE_HEIGHT,
                            newSignature.FileInfo.Length < maxLength,
                            maxLength == minLength
                                ? hiResColor
                                : InterpolateColor(lowResColor, hiResColor, minLength, maxLength,
                                    newSignature.FileInfo));
                    });
                    actions.AddRange(
                        from ss in _similarSignatures[s]
                        select (Action)(() =>
                        {
                            ss.Selected = ss.FileInfo.Length <= maxLength;
                            ss.PictureBox.BackColor = InterpolateColor(Color.Black, Color.Blue, minLength, maxLength, ss.FileInfo);
                        }));

                    handled = true;
                }

                if(!handled) // look for one similar yet to be displayed picture: adding 2
                    foreach (var previous in _distinctSignatures.ToArray())
                    {
                        if (previous.GetSimilarityWith(newSignature) > _similarityFactor)
                        {
                            Console.WriteLine($"    Found similar with {previous.FileInfo.Name}. New.");
                            var top = _similarSignatures.Count * PICTURE_HEIGHT;
                            Color previousColor;
                            Color newColor;
                            var previousSelected = false;
                            var newSelected = false;
                            if (previous.FileInfo.Length < newSignature.FileInfo.Length)
                            {
                                previousSelected = true;
                                previousColor = lowResColor;
                                newColor = hiResColor;
                            }
                            else if (previous.FileInfo.Length > newSignature.FileInfo.Length)
                            {
                                newSelected = true;
                                newColor = lowResColor;
                                previousColor = hiResColor;
                            }
                            else // they are same size: we preselect the second one!
                            {
                                newSelected = true;
                                newColor = hiResColor;
                                previousColor = hiResColor;
                            }

                            actions.Add(() =>
                            {
                                previous.PictureBox = CreatePictureBox(previous.SetLocation(0, top), 
                                    PICTURE_WIDTH, PICTURE_HEIGHT, previousSelected, previousColor);
                            });

                            actions.Add(() =>
                            {
                                newSignature.PictureBox = CreatePictureBox(newSignature.SetLocation(PICTURE_WIDTH, top),
                                    PICTURE_WIDTH, PICTURE_HEIGHT, newSelected, newColor);
                            });
                            _similarSignatures.Add(previous, new[] {previous, newSignature }.ToList());
                            Console.WriteLine($"    {previous.FileInfo.Name} removed from distincts.");
                            _distinctSignatures.Remove(previous);
                            handled = true;

                            Console.WriteLine($"{previous.FileInfo.Name} : Color = {previousColor}");
                            Console.WriteLine($"{newSignature.FileInfo.Name} : Color = {newColor}");
                        }
                    }
                if(!handled) // this is a brand new signature
                {
                    Console.WriteLine($"    {newSignature.FileInfo.Name} added to distincts.");
                    _distinctSignatures.Add(newSignature);
                }
            }

            foreach (var action in actions) action.Invoke();

        }

        SelectablePictureBox CreatePictureBox(PictureSignature signature, int w, int h, bool selected, Color backColor,
            SelectablePictureBox ppb = null)
        {
            var pb = ppb ?? new SelectablePictureBox(signature, labelFile);
            var fileInfo = signature.FileInfo;
            if (PanelMain.InvokeRequired)
            {
                Console.WriteLine($"   Invoke creating picture at ({signature.Location}) for {fileInfo.Name}.");
                PanelMain.Invoke(new Action(() => CreatePictureBox(signature, w, h, selected, backColor, pb)));
                return pb;
            }

            Console.WriteLine($"   Creating picture at ({signature.Location}) for {fileInfo.Name}. Selected : {selected}");
            pb = new SelectablePictureBox(signature, labelFile);

            PanelMain.Controls.Add(pb);

            PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) pb).BeginInit();

            pb.Location = signature.Location;
            pb.Name = "pictureBox_" + fileInfo.Name;
            pb.Size = new Size(w, h);
            pb.TabIndex = int.MaxValue;
            pb.TabStop = true;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Image = PictureHelper.ReadImageFromFileInfo(fileInfo);
            pb.Selected = selected;
            pb.BackColor = backColor;

            pb.Tag = fileInfo;

            PanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) pb).EndInit();
            return pb;
        }

        void SimilarPicturesForm_Load(object sender, EventArgs e) { }


        public void LoadPictures(DirectoryInfo directory)
        {
            using (new StateKeeper().Hourglass(this).Disable(similarityFactor).Disable(buttonGo))
            {
                _similarityFactor = (double) similarityFactor.Value / 100;
                if (directory is null)
                {
                    Close();
                    return;
                }

                var files = new Queue<FileInfo>(
                    directory.EnumerateFiles("*.jpg", SearchOption.AllDirectories).ToArray()
                        .OrderByDescending(fi => fi.Length)); // better (and heavier) images first

                ProgressBar.Maximum = files.Count;

                void LoadPictureThread()
                {
                    while (_formIsAlive)
                    {
                        FileInfo file;
                        lock (files)
                        {
                            if (files.Count == 0) return; // we're done!
                            file = files.Dequeue();
                        }

                        var signature = new PictureSignature(file, 16, 4, false);
                        Console.WriteLine($"LoadPictureThread: {file.Name}");
                        signature.GetSignature(ReceiveSignatureNew);
                        lock (_signatures) _signatures.Add(signature);
                    }
                }


                var tasks = Enumerable.Range(0, MAX_TASKS)
                    .Select(_ => new Task(LoadPictureThread))
                    .Pipe(s => s.Start())
                    .ToArray();

                Task.WaitAll(tasks);

                DisplaySignatures(_similarSignatures);
            }
        }

        public void buttonGo_Click(object sender, EventArgs e)
        {
            _distinctSignatures.Clear();
            _similarSignatures.Clear();
            using (new StateKeeper().Hourglass(this).Disable(similarityFactor).Disable(buttonGo))
            {
                foreach (var control in PanelMain.Controls.Cast<Control>().ToArray())
                    if (control is Panel)
                        PanelMain.Controls.Remove(control);
                _similarityFactor = (double)similarityFactor.Value / 100;
                ProgressBar.Value = 0;
                lock (_signatures)
                    foreach (var signature in _signatures)
                    {
                        if (!_formIsAlive) break;
                        if (signature.PictureBox != null)
                        {
                            signature.PictureBox.Dispose();
                            signature.PictureBox = null;
                        }
                        signature.FileInfo.Refresh();
                        if (signature.FileInfo.Exists) ReceiveSignatureNew(signature);
                    }
            }
            DisplaySignatures(_similarSignatures);
        }

        static IEnumerable<SelectablePictureBox> GetSelectedPictureBoxes(object sender)
        {
            switch (sender)
            {
                case SelectablePictureBox pb:
                    if (pb.Selected)
                        yield return pb;
                    break;
                case Control control:
                {
                    foreach (Control child in control.Controls)
                    foreach (var sb in GetSelectedPictureBoxes(child))
                        if (sb.Selected)
                            yield return sb;
                    break;
                }
            }
        }

        readonly Dictionary<Keys, Action<SimilarPicturesForm, object, KeyEventArgs>> _keyMapping =
            new Dictionary<Keys, Action<SimilarPicturesForm, object, KeyEventArgs>>
            {
                [Keys.Delete] = (f, s, e) => StagePictures(GetSelectedPictureBoxes(s)),
                [Keys.Delete | Keys.Shift] = (f, s, e) => DeletePictures(GetSelectedPictureBoxes(s)),
                [Keys.F5] = (f, s, e) => f.buttonGo_Click(s, e)
            };

        public void SimilarPicturesForm_KeyUp(object sender, KeyEventArgs evt)
        {
            var key = evt.KeyCode |
                      (evt.Shift   ? Keys.Shift   : Keys.None) |
                      (evt.Alt     ? Keys.Alt     : Keys.None) |
                      (evt.Control ? Keys.Control : Keys.None);
            if (!_keyMapping.ContainsKey(key)) return;

            _keyMapping[key](this, sender, evt);
            evt.Handled = evt.SuppressKeyPress = true;
        }

        static void StagePictures(IEnumerable<SelectablePictureBox> pictures)
            => DeletePicturesImpl(pictures, true);

        static void DeletePictures(IEnumerable<SelectablePictureBox> pictures)
            => DeletePicturesImpl(pictures, false);

        static void DeletePicturesImpl(IEnumerable<SelectablePictureBox> pictures, bool stage)
        {
            var count = 0;
            var countErrors = 0;
            var errorMessage = new StringBuilder();
            var newLine = Environment.NewLine;
            foreach (var pb in pictures)
            {
                pb.Image.Dispose();
                try
                {
                    if (stage)
                    {
                        var stageDirectory = new DirectoryInfo(Path.Combine(pb.FileInfo.DirectoryName, "spare"));
                        stageDirectory.Create();
                        pb.FileInfo.MoveTo(stageDirectory.FullName);
                    }
                    else
                    {
                        pb.FileInfo.Delete();
                    }

                    pb.Parent.Controls.Remove(pb);
                    count++;
                }
                catch (Exception e)
                {
                    errorMessage.Append($":{newLine}{pb.FileInfo?.FullName ?? "Unknown file"}: {e.Message}");
                    countErrors++;
                }
            }


            var message = $"{count} pictures {(stage ? "staged" : "deleted")}." +
                        (countErrors > 0
                            ? $"{newLine}{countErrors} pictures couldn't be {(stage ? "staged" : "deleted")}:{newLine}" + errorMessage.ToString()
                            : string.Empty);
            if(MuteDialogs)
                Console.WriteLine(message);
            else MessageBox.Show(message, "Deleting files");
        }

        void SimilarPicturesForm_FormClosing(object sender, FormClosingEventArgs e) => _formIsAlive = false;
    }
}
