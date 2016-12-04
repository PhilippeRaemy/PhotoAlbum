namespace AlbumWordAddin
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;

    using UserPreferences;

    public partial class FormImportPictures : Form
    {
        public FormImportPictures()
        {
            InitializeComponent();
            var userprefs=new PersistedUserPreferences();
            textStartFolder.Text = userprefs.FolderImportStart;
            textEndFolder.Text = userprefs.FolderImportEnd;
            comboMaxPicsPerFile.Text = userprefs.MaxPicturesPerFile.ToString();
            ChkConfirmOverwrite.CheckState = userprefs.ConfirmFileOverwrite ? CheckState.Checked : CheckState.Unchecked;
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
                int maxPics;
                userprefs.MaxPicturesPerFile =
                    int.TryParse(comboMaxPicsPerFile.SelectedItem.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out maxPics)
                        ? maxPics: -1;
                userprefs.ConfirmFileOverwrite = ChkConfirmOverwrite.CheckState ==CheckState.Checked;
            }
            Close();
        }
    }
}
