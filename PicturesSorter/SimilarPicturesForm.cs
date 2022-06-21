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


        public DirectoryInfo directory;

        public SimilarPicturesForm()
        {
            InitializeComponent();
        }

        HashSet<PictureSignature> DistinctSignatures = new HashSet<PictureSignature>();
        Dictionary<PictureSignature, List<PictureSignature>> SimilarSignatures = new Dictionary<PictureSignature, List<PictureSignature>>(new PictureSignatureComparer());

        void ReceiveSignature(PictureSignature Signature)
        {
            ProgressBar.Value += 1;
            foreach (var s in SimilarSignatures.Keys)
            {
                if (s.GetSimilarityWith(Signature) > .95)
                {
                    SimilarSignatures[Signature].Add(Signature);
                    Signature.PictureBox = CreatePictureBox(SimilarSignatures[Signature].Count() * PICTURE_WIDTH, s.PictureBox.Top, PICTURE_WIDTH, PICTURE_HEIGHT, Signature.FileInfo);
                }
                return;
            }
            foreach (var s in DistinctSignatures)
            {
                if (s.GetSimilarityWith(Signature) > .95)
                {
                    var top = SimilarSignatures.Count() * PICTURE_WIDTH;
                    s.PictureBox = CreatePictureBox(0, top, PICTURE_WIDTH, PICTURE_HEIGHT, s.FileInfo);
                    Signature.PictureBox = CreatePictureBox(PICTURE_WIDTH, top, PICTURE_WIDTH, PICTURE_HEIGHT, Signature.FileInfo);
                    SimilarSignatures.Add(s, new[] { Signature }.ToList());
                    return;
                }
            }
            DistinctSignatures.Add(Signature);
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
            if (directory is null)
            {
                Close();
                return;
            }
            var files = directory.EnumerateFiles("*.jpg", SearchOption.AllDirectories).ToArray();
            ProgressBar.Maximum = files.Length;

            var done = new List<(FileInfo, List<ushort>)>();
            foreach (var batch in files
                .OrderByDescending(fi => fi.Length) // better (and heavier) images first
                .Select((fi, i) => ( 
                    FileInfo: fi, 
                    Signature: new PictureSignature(fi, 16, 4).GetSignatureAsync(ReceiveSignature)))
                .Batch(8)
            )
            {
                Task.WaitAll(batch.Select(b => b.Signature).ToArray());
                done.AddRange(batch.Select(b => (b.FileInfo, b.Signature.Result)));
            }
        }
    }
}
