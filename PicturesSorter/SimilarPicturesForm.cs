using MoreLinq;
using PictureHandler;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PicturesSorter
{
    public partial class SimilarPicturesForm : Form
    {
        public DirectoryInfo directory;

        public SimilarPicturesForm()
        {
            InitializeComponent();
        }

        private void SimilarPicturesForm_Load(object sender, EventArgs e)
        {
            if (directory is null)
            {
                Close();
                return;
            }
            var files = directory.EnumerateFiles("*.jpg", SearchOption.AllDirectories).ToArray();
            ProgressBar.Maximum = files.Length;


            var fileSignatures = files
                .OrderByDescending(fi => fi.Length) // better images first
                .Select((fi, i) => new { FileInfo = fi, Signature = new PictureSignature(fi, 16, 4)})
                .Pipe(fi => ProgressBar.Value+=1)
                .
                .ToArray();
            // 
            // pictureBox1
            // 
            var  pictureBox1 = new PictureBox();

            PanelMain.Controls.Add(pictureBox1);

            PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();

            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(164, 147);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;

            //             // pictureBox1
            // 
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(100, 50);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;


            PanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();

        }
    }
}
