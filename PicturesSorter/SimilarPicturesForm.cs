using PictureHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PicturesSorter
{
    using System.Configuration;
    using System.Threading;

    public partial class SimilarPicturesForm : Form
    {
        const int PICTURE_WIDTH = 150;
        const int PICTURE_HEIGHT = 150;
        const int MAX_TASKS = 1;
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
            IncrementProgress();
            foreach (var s in _similarSignatures.Keys)
            {
                if (s.GetSimilarityWith(signature) > _similarityFactor)
                {
                    _similarSignatures[s].Add(signature);
                    signature._pictureBox = CreatePictureBox(_similarSignatures[s].Count * PICTURE_WIDTH,
                        s._pictureBox.Top, PICTURE_WIDTH, PICTURE_HEIGHT, signature.FileInfo);
                    return;
                }

            }

            foreach (var s in _distinctSignatures.ToArray())
            {
                if (s.GetSimilarityWith(signature) > _similarityFactor)
                {
                    var top = 0;
                    var left = 0;
                    top = _similarSignatures.Count * PICTURE_WIDTH;
                    s._pictureBox = CreatePictureBox(0, top, PICTURE_WIDTH, PICTURE_HEIGHT, s.FileInfo);
                    top = s._pictureBox.Top;

                    signature._pictureBox = CreatePictureBox(PICTURE_WIDTH, top, PICTURE_WIDTH, PICTURE_HEIGHT,
                        signature.FileInfo);
                    _similarSignatures.Add(s, new[] {signature}.ToList());
                    _distinctSignatures.Remove(s);
                    return;
                }
            }

            _distinctSignatures.Add(signature);
        }

        PictureBox CreatePictureBox(int x, int y, int w, int h, FileInfo fileInfo, PictureBox ppb = null)
        {
            var pb = ppb ?? new PictureBox();
            if (PanelMain.InvokeRequired)
            {

                PanelMain.Invoke(new Action(() => CreatePictureBox(x, y, w, h, fileInfo, pb)));
                return pb;
            }

            pb = new PictureBox();

            PanelMain.Controls.Add(pb);

            PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) pb).BeginInit();

            pb.Location = new System.Drawing.Point(x, y);
            pb.Name = "pictureBox_" + fileInfo.FullName;
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
                    FileInfo file;

                    while (true)
                    {
                        lock (files)
                        {
                            if (files.Count == 0) return; // we're done!
                            file = files.Dequeue();
                        }

                        var signature = new PictureSignature(file, 16, 4);
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
