namespace PictureHandler
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class SelectablePictureBox : Panel, IDisposable, ISupportInitialize
    {
        const int BORDER_WIDTH = 10;
        readonly PictureSignature _parentSignature;
        readonly Label _labelBox;

        readonly PictureBox _pictureBox;


        public SelectablePictureBox(PictureSignature parentSignature, Label labelBox)
        {
            _parentSignature = parentSignature;
            _labelBox = labelBox;
            _pictureBox = new PictureBox();
            _pictureBox.BackColor=Color.WhiteSmoke;
            _pictureBox.Click += PictureBoxClick; ;
            Click += PictureBoxClick;
            Controls.Add(_pictureBox);
            Resize += SelectablePictureBox_Resize;
            MouseHover += Pb_MouseHover();
            _pictureBox.MouseHover += Pb_MouseHover();
        }

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                var oldColor = base.BackColor;
                if(InvokeRequired) Invoke(new Action(() => { base.BackColor = value; }));
                else base.BackColor = value;
                Console.WriteLine($"Setting color of {_parentSignature.FileInfo.Name} from {oldColor} to {base.BackColor}");
            }
        }

        EventHandler Pb_MouseHover() => (sender, args) =>
            SetLabelFileText($"{_parentSignature.FileInfo.FullName}({_parentSignature.FileInfo.Length / 1024.0 / 1024.0:f2}Mb)[{Image.Width}x{Image.Height}]");

        void SetLabelFileText(string text)
        {
            if (_labelBox.InvokeRequired)
                _labelBox.Invoke(new Action(() => SetLabelFileText(text)));
            else
                _labelBox.Text = text;
        }

        void PictureBoxClick(object sender, EventArgs e) => Selected = !Selected;

        void SelectablePictureBox_Resize(object sender, EventArgs e)
        {
            _pictureBox.Left = BORDER_WIDTH;
            _pictureBox.Top = BORDER_WIDTH;
            _pictureBox.Width = ClientSize.Width - 2 * BORDER_WIDTH;
            _pictureBox.Height = ClientSize.Height - 2 * BORDER_WIDTH;
            Debug.Assert(_pictureBox?.Image is null);
        }

        public FileInfo FileInfo => _parentSignature.FileInfo;

        public bool Selected
        {
            get => BorderStyle == BorderStyle.Fixed3D;
            set => _pictureBox.BorderStyle = BorderStyle = value ? BorderStyle.Fixed3D : BorderStyle.None;
        }

        public PictureBoxSizeMode SizeMode { get => _pictureBox.SizeMode; set => _pictureBox.SizeMode=value; }

        public Image Image
        {
            get => _pictureBox.Image;
            set => _pictureBox.Image = value.Resize(new Size(_pictureBox.Width, _pictureBox.Height));
        }

        public new void Dispose()
        {
            base.Dispose();
            _pictureBox?.Dispose();
        }

        public void BeginInit() => ((ISupportInitialize)_pictureBox).BeginInit();
        public void EndInit() => ((ISupportInitialize)_pictureBox).EndInit();
    }
}