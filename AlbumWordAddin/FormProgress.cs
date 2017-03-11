namespace AlbumWordAddin
{
    using System;
    using System.Windows.Forms;

    using UserPreferences;

    public partial class FormProgress : Form, IProgress
    {
        public FormProgress()
        {
            InitializeComponent();
        }

        void buttonGo_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void InitProgress(int max)
        {
            progressBar.Maximum = max;
            progressBar.Minimum = 0;
            progressBar.Value = 0;
            progressBar.Visible = true;
        }

        public void Progress()
        {
            progressBar.Value++;
        }

        public void CloseProgress()
        {
            progressBar.Visible = false;
        }
    }
}
