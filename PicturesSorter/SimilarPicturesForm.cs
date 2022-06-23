using MoreLinq;
using PictureHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturesSorter
{
    using System.Threading;

    public partial class SimilarPicturesForm : Form
    {
        const int PICTURE_WIDTH = 50;
        const int PICTURE_HEIGHT = 50;
        const int MAX_TASKS = 1;

        DirectoryInfo _directory;

        public SimilarPicturesForm()
        {
            InitializeComponent();
        }

        HashSet<PictureSignature> DistinctSignatures = new HashSet<PictureSignature>();
        Dictionary<PictureSignature, List<PictureSignature>> SimilarSignatures = new Dictionary<PictureSignature, List<PictureSignature>>(new PictureSignatureComparer());

        void IncrementProgress()
        {
            if (ProgressBar.InvokeRequired)
                ProgressBar.Invoke(new Action(IncrementProgress));
            else
                ProgressBar.Value += 1;
        }

        void ReceiveSignature(PictureSignature signature)
        {
            IncrementProgress();
            foreach (var s in SimilarSignatures.Keys)
            {
                if (s.GetSimilarityWith( signature) > .95)
                {
                    SimilarSignatures[signature].Add(signature);
                    signature._pictureBox = CreatePictureBox(SimilarSignatures[signature].Count() * PICTURE_WIDTH, s._pictureBox.Top, PICTURE_WIDTH, PICTURE_HEIGHT, signature.FileInfo);
                }
                return;
            }
            foreach (var s in DistinctSignatures)
            {
                if (s.GetSimilarityWith(signature) > .95)
                {
                    var top = SimilarSignatures.Count * PICTURE_WIDTH;
                    s._pictureBox = CreatePictureBox(0, top, PICTURE_WIDTH, PICTURE_HEIGHT, s.FileInfo);
                    signature._pictureBox = CreatePictureBox(PICTURE_WIDTH, top, PICTURE_WIDTH, PICTURE_HEIGHT, signature.FileInfo);
                    SimilarSignatures.Add(s, new[] { signature }.ToList());
                    return;
                }
            }
            DistinctSignatures.Add(signature);
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
            pb.Image = PictureHelper.ReadImageFromFileInfo(fileInfo);
            PanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) pb).EndInit();
            return pb;
        }

        void SimilarPicturesForm_Load(object sender, EventArgs e)
        {
        }


        public void LoadPictures(DirectoryInfo directory)
        {
            _directory = directory;
            if (directory is null)
            {
                Close();
                return;
            }

            var files = new Queue<FileInfo>(
                directory.EnumerateFiles("*.jpg", SearchOption.AllDirectories).ToArray()
                    .OrderByDescending(fi => fi.Length)); // better (and heavier) images first
            var signatures = new List<List<ushort>>();

            ProgressBar.Maximum = files.Count;

            void LoadPictureThread()
            {
                FileInfo file;

                while(true){
                    lock (files)
                    {
                        if (files.Count == 0) return; // we're done!
                        file = files.Dequeue();
                    }

                    var signature = new PictureSignature(file, 16, 4).GetSignature(ReceiveSignature);
                    lock (signatures) signatures.Add(signature);
                }
            }

            var threads = Enumerable.Range(0, MAX_TASKS)
                    .Select(_ => new Thread(LoadPictureThread));

            foreach (var thread in threads) thread.Start();

        }
    }
}
