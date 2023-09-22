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

    public partial class SimilarPicturesForm : Form
    {
        const int PICTURE_WIDTH = 250;
        const int PICTURE_HEIGHT = 250;
        const int MAX_TASKS = 16;
        double _similarityFactor = .95;
        bool _formIsAlive = true;
        readonly TimeSpan _loadPictureTimeout = TimeSpan.FromSeconds(5);
        SimilarPicturesHandler _similarPicturesHandler;

        public static bool MuteDialogs { get; set; }

        public SimilarPicturesForm() => InitializeComponent();

        void IncrementProgress()
        {
            if (ProgressBar.InvokeRequired)
                // new Task(() => ProgressBar.Invoke(new Action(IncrementProgress))).Start();
                ProgressBar.Invoke(new Action(IncrementProgress));
            else
            {
                if (ProgressBar.Value < ProgressBar.Maximum)
                    ProgressBar.Value += 1;
                labelProgressBar.Text = $"{ProgressBar.Value}/{ProgressBar.Maximum}";
                labelProgressBar.Update();
            }
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
                foreach (var s in g.OrderByDescending(p => p.FileInfo.Length).ThenBy(p => p.FileInfo.Name))
                {
                    s.PictureBox = CreatePictureBox(
                        s.SetLocation(x * PICTURE_WIDTH, y * PICTURE_HEIGHT),
                        PICTURE_WIDTH, PICTURE_HEIGHT,
                        x > 0 && s.FileInfo.Length <= maxLength,
                        maxLength == minLength
                            ? hiResColor
                            : InterpolateColor(lowResColor, hiResColor, minLength, maxLength,
                                s.FileInfo));
                    x++;
                }

                y++;
            }
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

            Console.WriteLine(
                $"   Creating picture at ({signature.Location}) for {fileInfo.Name}. Selected : {selected}");
            pb = new SelectablePictureBox(signature, labelFile);

            PanelMain.Controls.Add(pb);

            PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb).BeginInit();

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
            ((System.ComponentModel.ISupportInitialize)pb).EndInit();
            return pb;
        }

        void SimilarPicturesForm_Load(object sender, EventArgs e)
        {
        }


        public async void LoadPictures(DirectoryInfo directory)
        {
            using (new StateKeeper().Hourglass(this).Disable(similarityFactor).Disable(buttonGo))
            {
                _similarPicturesHandler = new SimilarPicturesHandler
                {
                    Directory = directory,
                    SimilarityFactor = (double)similarityFactor.Value / 100,
                    LoadPictureTimeout = _loadPictureTimeout,
                    MaxTasks = MAX_TASKS,
                    CloseAction = Close,
                    SetProgressMaxAction = max => ProgressBar.Maximum = max,
                    IncrementProgressAction = IncrementProgress,
                    KeepGoingFunc = () => _formIsAlive
                };
                var similarSignatures = await _similarPicturesHandler.LoadPictures();

                DisplaySignatures(similarSignatures);
            }
        }


        public void buttonGo_Click(object sender, EventArgs e)
        {
            using (new StateKeeper().Hourglass(this).Disable(similarityFactor).Disable(buttonGo))
            {
                foreach (var control in PanelMain.Controls.Cast<Control>().ToArray())
                    if (control is Panel)
                        PanelMain.Controls.Remove(control);
                _similarityFactor = (double)similarityFactor.Value / 100;
                ProgressBar.Value = 0;
                labelProgressBar.Text = $"{ProgressBar.Value}/{ProgressBar.Maximum}";
                labelProgressBar.Update();

                ProgressBar.Maximum = _similarPicturesHandler.SignatureCount;
                var similarSignatures = _similarPicturesHandler.ReCompare();
                lock (_similarPicturesHandler)
                    DisplaySignatures(similarSignatures);
            }

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
                    foreach (var child in control.Controls.Cast<Control>().ToArray())
                    foreach (var sb in GetSelectedPictureBoxes(child))
                        if (sb.Selected)
                            yield return sb;
                    break;
            }
        }

        readonly Dictionary<Keys, Action<SimilarPicturesForm, object, KeyEventArgs>> _keyMapping =
            new Dictionary<Keys, Action<SimilarPicturesForm, object, KeyEventArgs>>
            {
                [Keys.Delete] = (f, s, e) => StagePictures(GetSelectedPictureBoxes(s)),
                [Keys.Delete | Keys.Shift] = (f, s, e) => DeletePictures(GetSelectedPictureBoxes(s)),
                [Keys.F5] = (f, s, e) => f.buttonGo_Click(s, e),
                [Keys.Escape] = (f, s, e) => f.Close(),
                [Keys.W | Keys.Control] = (f, s, e) => f.Close(),
                [Keys.F4 | Keys.Alt] = (f, s, e) => f.Close(),
            };

        Dictionary<PictureSignature, List<PictureSignature>> _similarSignatures;

        public void SimilarPicturesForm_KeyUp(object sender, KeyEventArgs evt)
        {
            var key = evt.KeyCode |
                      (evt.Shift ? Keys.Shift : Keys.None) |
                      (evt.Alt ? Keys.Alt : Keys.None) |
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
                        pb.FileInfo.MoveTo(Path.Combine(stageDirectory.FullName, pb.FileInfo.Name));
                        Trace.WriteLine($"{pb.FileInfo.Name} staged to {stageDirectory.FullName}");
                    }
                    else
                    {
                        pb.FileInfo.Delete();
                        Trace.WriteLine($"{pb.FileInfo.Name} deleted");
                    }

                    pb.Parent.Controls.Remove(pb);
                    count++;
                }
                catch (Exception e)
                {
                    var msg = $":{newLine}{pb.FileInfo?.FullName ?? "Unknown file"}: {e.Message}";
                    Trace.TraceError(msg);
                    errorMessage.Append(msg);
                    countErrors++;
                }
            }


            var message = $"{count} pictures {(stage ? "staged" : "deleted")}." +
                          (countErrors > 0
                              ? $"{newLine}{countErrors} pictures couldn't be {(stage ? "staged" : "deleted")}:{newLine}" +
                                errorMessage.ToString()
                              : string.Empty);
            if (MuteDialogs)
                Console.WriteLine(message);
            else MessageBox.Show(message, "Deleting files");
        }

        void SimilarPicturesForm_FormClosing(object sender, FormClosingEventArgs e) => _formIsAlive = false;
    }
}