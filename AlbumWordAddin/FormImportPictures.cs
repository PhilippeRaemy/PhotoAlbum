namespace AlbumWordAddin
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;

    using UserPreferences;

    public partial class FormImportPictures : Form, IProgress
    {
        public FormImportPictures()
        {
            InitializeComponent();
            var userprefs=new PersistedUserPreferences();
            textStartFolder.Text = userprefs.FolderImportStart;
            textEndFolder.Text = userprefs.FolderImportEnd;
            textIncludeFiles.Text = userprefs.IncludeFiles;
            textExcludeFiles.Text = userprefs.ExcludeFiles;
            comboMaxPicsPerFile.Text = userprefs.MaxPicturesPerFile.ToString();
            ChkConfirmOverwrite.CheckState = userprefs.ConfirmFileOverwrite ? CheckState.Checked : CheckState.Unchecked;
            textTemplate.Text = userprefs.NewDocumentTemplate;
        }

        void ChkConfirmOverwrite_CheckedChanged(object sender, EventArgs e)
        {

        }

        void comboMaxPicsPerFile_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void buttonOpenStartFolder_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog {SelectedPath = textStartFolder.Text};
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textStartFolder.Text = fbd.SelectedPath;
            }
        }

        void buttonOpenEndFolder_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog { SelectedPath = textEndFolder.Text };
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textEndFolder.Text = fbd.SelectedPath;
            }
        }

        void buttonGo_Click(object sender, EventArgs e)
        {
            using (var userprefs = new PersistedUserPreferences())
            {
                userprefs.FolderImportStart = textStartFolder.Text;
                userprefs.FolderImportEnd = textEndFolder.Text;
                userprefs.IncludeFiles = textIncludeFiles.Text;
                userprefs.ExcludeFiles = textExcludeFiles.Text;
                userprefs.NewDocumentTemplate = textTemplate.Text;
                int maxPics;
                userprefs.MaxPicturesPerFile =
                    int.TryParse(comboMaxPicsPerFile.SelectedItem.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out maxPics)
                        ? maxPics: -1;
                userprefs.ConfirmFileOverwrite = ChkConfirmOverwrite.CheckState ==CheckState.Checked;
            }
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

        void buttonOpenTemplate_Click(object sender, EventArgs e)
        {
            FileInfo fi=null;
            try
            {
                fi = new FileInfo(textTemplate.Text);
            }
            catch
            {
                // ignored
            }
            var fbd = new OpenFileDialog()
            {
                FileName= fi?.Exists == true ? fi.Name : "*.dot?",
                InitialDirectory = fi?.Exists == true ? fi.DirectoryName : string.Empty,
                CheckFileExists = true,
                DefaultExt = "dotx",
                Filter = "*.docx|*.dotm",
                Multiselect = false,
                Title = "Select the template for new album documents"
            };
            if (fbd.ShowDialog() == DialogResult.OK && fbd.SafeFileName!=null)
            {
                textTemplate.Text = fbd.FileName;
            }
        }
    }
}
