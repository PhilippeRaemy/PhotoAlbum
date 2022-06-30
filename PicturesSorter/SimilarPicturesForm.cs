using PictureHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PicturesSorter
{
    using System.Configuration;
    using System.Drawing;
    using System.Threading;

    public partial class SimilarPicturesForm : Form
    {
        const int PICTURE_WIDTH = 250;
        const int PICTURE_HEIGHT = 250;
        const int MAX_TASKS = 8;
        List<PictureSignature> _signatures = new List<PictureSignature>();
        double _similarityFactor = .95;

        DirectoryInfo _directory;

        public SimilarPicturesForm()
        {
            InitializeComponent();
        }

        readonly HashSet<PictureSignature> _distinctSignatures = new HashSet<PictureSignature>();

        readonly Dictionary<PictureSignature, List<PictureSignature>> _similarSignatures =
            new Dictionary<PictureSignature, List<PictureSignature>>(new PictureSignatureComparer());

        void IncrementProgress()
        {
            if (ProgressBar.InvokeRequired)
                ProgressBar.Invoke(new Action(IncrementProgress));
            else
                ProgressBar.Value += 1;
        }

        void SetLabelFileText(string text)
        {
            if (labelFile.InvokeRequired)
                labelFile.Invoke(new Action(() => SetLabelFileText(text)));
            else
                labelFile.Text = text;
        }

        void ReceiveSignature(PictureSignature signature)
        {
            var actions = new List<Action>();
            Console.WriteLine($"Got {signature.FileInfo.FullName}");
            IncrementProgress();
            var handled = false;
            lock (_similarSignatures)
            {
                foreach (var s in _similarSignatures.Keys)
                    if (s.GetSimilarityWith(signature) > _similarityFactor)
                    {
                        Console.WriteLine($"    Found similar with {s.FileInfo.Name}. Pre-existing.");
                        _similarSignatures[s].Add(signature);
                        actions.Add(() =>
                        {
                            signature._pictureBox = CreatePictureBox(
                                signature.SetLocation(_similarSignatures[s].Count * PICTURE_WIDTH, s.Location.Y), 
                                PICTURE_WIDTH, PICTURE_HEIGHT);
                        });
                        handled = true;
                    }

                if(!handled)
                    foreach (var s in _distinctSignatures.ToArray())
                    {
                        if (s.GetSimilarityWith(signature) > _similarityFactor)
                        {
                            Console.WriteLine($"    Found similar with {s.FileInfo.Name}. New.");
                            var top = _similarSignatures.Count * PICTURE_HEIGHT;
                            actions.Add(() =>
                            {
                                s._pictureBox = CreatePictureBox(s.SetLocation(0, top), PICTURE_WIDTH, PICTURE_HEIGHT);
                            });

                            actions.Add(() =>
                            {
                                signature._pictureBox = CreatePictureBox(signature.SetLocation(PICTURE_WIDTH, top), PICTURE_WIDTH,
                                    PICTURE_HEIGHT);
                            });
                            _similarSignatures.Add(s, new[] { signature }.ToList());
                            Console.WriteLine($"    {s.FileInfo.Name} removed from distincts.");
                            _distinctSignatures.Remove(s);
                            handled = true;
                        }
                    }
                if(!handled)
                {
                    Console.WriteLine($"    {signature.FileInfo.Name} added to distincts.");
                    _distinctSignatures.Add(signature);
                }
            }

            foreach (var action in actions) action.Invoke();
        }

        PictureBox CreatePictureBox(PictureSignature signature, int w, int h, PictureBox ppb = null)
        {
            var pb = ppb ?? new PictureBox();
            var fileInfo = signature.FileInfo;
            if (PanelMain.InvokeRequired)
            {
                Console.WriteLine($"   Invoke creating picture at ({signature.Location}) for {fileInfo.Name}.");
                PanelMain.Invoke(new Action(() => CreatePictureBox(signature, w, h, pb)));
                return pb;
            }

            Console.WriteLine($"   Creating picture at ({signature.Location}) for {fileInfo.Name}.");
            pb = new PictureBox();

            PanelMain.Controls.Add(pb);

            PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) pb).BeginInit();

            pb.Location = signature.Location;
            pb.Name = "pictureBox_" + fileInfo.Name;
            pb.Size = new System.Drawing.Size(w, h);
            pb.TabIndex = int.MaxValue;
            pb.TabStop = true;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Image = PictureHelper.ReadImageFromFileInfo(fileInfo);

            pb.MouseHover += Pb_MouseHover(
                $"{fileInfo.FullName}({fileInfo.Length / 1024 / 1024}Mb)[{pb.Image.Width}x{pb.Image.Height}]");

            PanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) pb).EndInit();
            return pb;
        }

        EventHandler Pb_MouseHover(string text)
        {
            return (sender, args) => SetLabelFileText(text);
        }

        void SimilarPicturesForm_Load(object sender, EventArgs e)
        {
        }


        public void LoadPictures(DirectoryInfo directory)
        {
            using (new StateKeeper().Hourglass(this).Disable(similarityFactor).Disable(buttonGo))
            {
                _similarityFactor = (double) similarityFactor.Value / 100;
                _directory = directory;
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
                    while (true)
                    {
                        FileInfo file;
                        lock (files)
                        {
                            if (files.Count == 0) return; // we're done!
                            file = files.Dequeue();
                        }

                        var signature = new PictureSignature(file, 16, 4);
                        Console.WriteLine($"LoadPictureThread: {file.Name}");
                        signature.GetSignature(ReceiveSignature);
                        lock (_signatures) _signatures.Add(signature);
                    }
                }

                var threads = Enumerable.Range(0, MAX_TASKS)
                    .Select(_ => new Thread(LoadPictureThread));

                foreach (var thread in threads) thread.Start();

            }
        }

        void buttonGo_Click(object sender, EventArgs e)
        {
            _distinctSignatures.Clear();
            _similarSignatures.Clear();
            using (new StateKeeper().Hourglass(this).Disable(similarityFactor).Disable(buttonGo))
            {
                foreach (var control in PanelMain.Controls.Cast<Control>().ToArray())
                    if (control is PictureBox)
                        PanelMain.Controls.Remove(control);
                _similarityFactor = (double)similarityFactor.Value / 100;
                ProgressBar.Value = 0;
                lock (_signatures)
                    foreach (var signature in _signatures)
                    {
                        if (signature._pictureBox != null)
                        {
                            signature._pictureBox.Dispose();
                            signature._pictureBox = null;
                        }

                        ReceiveSignature(signature);
                    }
            }
        }

    }
}
