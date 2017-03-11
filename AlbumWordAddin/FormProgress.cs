namespace AlbumWordAddin
{
    using System;
    using System.Windows.Forms;

    public partial class FormProgress : Form, IProgress
    {
        public FormProgress()
        {
            InitializeComponent();
        }

        void buttonCancel_Click(object sender, EventArgs e)
        {
            CancelEvent?.Invoke(this, new EventArgs());
            Close();
        }

        public void InitProgress(int max, string caption)
        {
            progressBar.Maximum = max;
            progressBar.Minimum = 0;
            progressBar.Value = 0;
            progressBar.Visible = true;
            Show();
        }

        public void Progress(string text)
        {
            progressBar.Value++;
        }

        public void CloseProgress()
        {
            progressBar.Visible = false;
        }

        public event EventHandler<EventArgs> CancelEvent;
    }
}
