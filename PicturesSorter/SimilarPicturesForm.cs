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
    public partial class SimilarPicturesForm : Form
    {
        const int PICTURE_WIDTH = 50;
        const int PICTURE_HEIGHT = 50;
        const int MAX_TASKS = 2;

        DirectoryInfo _directory;

        public SimilarPicturesForm()
        {
            InitializeComponent();
        }

        HashSet<PictureSignature> DistinctSignatures = new HashSet<PictureSignature>();
        Dictionary<PictureSignature, List<PictureSignature>> SimilarSignatures = new Dictionary<PictureSignature, List<PictureSignature>>(new PictureSignatureComparer());

        async Task ReceiveSignature(PictureSignature signature)
        {
            ProgressBar.Value += 1;
            foreach (var s in SimilarSignatures.Keys)
            {
                if (await s.GetSimilarityWithAsync( signature) > .95)
                {
                    SimilarSignatures[signature].Add(signature);
                    signature.PictureBox = CreatePictureBox(SimilarSignatures[signature].Count() * PICTURE_WIDTH, s.PictureBox.Top, PICTURE_WIDTH, PICTURE_HEIGHT, signature.FileInfo);
                }
                return;
            }
            foreach (var s in DistinctSignatures)
            {
                if (await s.GetSimilarityWithAsync(signature) > .95)
                {
                    var top = SimilarSignatures.Count * PICTURE_WIDTH;
                    s.PictureBox = CreatePictureBox(0, top, PICTURE_WIDTH, PICTURE_HEIGHT, s.FileInfo);
                    signature.PictureBox = CreatePictureBox(PICTURE_WIDTH, top, PICTURE_WIDTH, PICTURE_HEIGHT, signature.FileInfo);
                    SimilarSignatures.Add(s, new[] { signature }.ToList());
                    return;
                }
            }
            DistinctSignatures.Add(signature);
        }

        PictureBox CreatePictureBox(int x, int y, int w, int h, FileInfo fileInfo)
        {
            var pb = new PictureBox();

            PanelMain.Controls.Add(pb);

            PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb).BeginInit();

            pb.Location = new System.Drawing.Point(x,y);
            pb.Name = "pictureBox_" + fileInfo.FullName;
            pb.Size = new System.Drawing.Size(w,h);
            pb.TabIndex = int.MaxValue;
            pb.TabStop = true;
            pb.Image = PictureHelper.ReadImageFromFileInfo(fileInfo);
            PanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pb).EndInit();
            return pb;
        }

        void SimilarPicturesForm_Load(object sender, EventArgs e)
        {
        }

        public async void LoadPictures(DirectoryInfo directory)
        {
            _directory = directory;
            if (directory is null)
            {
                Close();
                return;
            }
            var files = directory.EnumerateFiles("*.jpg", SearchOption.AllDirectories).ToArray();
            ProgressBar.Maximum = files.Length;

            var done = new List<(FileInfo, List<ushort>)>();
            var tasksInFlight = new HashSet<Task>();
            foreach (var fi in files
                .OrderByDescending(fi => fi.Length) // better (and heavier) images first
                )
            {
                if (tasksInFlight.Count >= MAX_TASKS)
                { // need to wait
                    var task = await Task.WhenAny(tasksInFlight.ToArray());
                    tasksInFlight.Remove(task);
                }
                tasksInFlight.Add(new PictureSignature(fi, 16, 4).GetSignatureAsync(async s => await ReceiveSignature(s)));
            }
            Task.WaitAll(tasksInFlight.ToArray());
        }
    }
}
