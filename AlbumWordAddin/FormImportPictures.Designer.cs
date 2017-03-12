namespace AlbumWordAddin
{
    partial class FormImportPictures
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImportPictures));
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOpenStartFolder = new System.Windows.Forms.Button();
            this.panelStartFolder = new System.Windows.Forms.Panel();
            this.textStartFolder = new System.Windows.Forms.TextBox();
            this.panelEndFolder = new System.Windows.Forms.Panel();
            this.textEndFolder = new System.Windows.Forms.TextBox();
            this.buttonOpenEndFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboMaxPicsPerFile = new System.Windows.Forms.ComboBox();
            this.ChkConfirmOverwrite = new System.Windows.Forms.CheckBox();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.buttonGo = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelExcludeFiles = new System.Windows.Forms.Panel();
            this.textExcludeFiles = new System.Windows.Forms.TextBox();
            this.labelExcludeFiles = new System.Windows.Forms.Label();
            this.panelIncludeFiles = new System.Windows.Forms.Panel();
            this.textIncludeFiles = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.textTemplate = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonOpenTemplate = new System.Windows.Forms.Button();
            this.labelTemplate = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelStartFolder.SuspendLayout();
            this.panelEndFolder.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelProgress.SuspendLayout();
            this.panelExcludeFiles.SuspendLayout();
            this.panelIncludeFiles.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start Folder:";
            // 
            // buttonOpenStartFolder
            // 
            this.buttonOpenStartFolder.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonOpenStartFolder.Location = new System.Drawing.Point(521, 0);
            this.buttonOpenStartFolder.Name = "buttonOpenStartFolder";
            this.buttonOpenStartFolder.Size = new System.Drawing.Size(25, 21);
            this.buttonOpenStartFolder.TabIndex = 2;
            this.buttonOpenStartFolder.Text = "...";
            this.buttonOpenStartFolder.UseVisualStyleBackColor = true;
            this.buttonOpenStartFolder.Click += new System.EventHandler(this.buttonOpenStartFolder_Click);
            // 
            // panelStartFolder
            // 
            this.panelStartFolder.Controls.Add(this.textStartFolder);
            this.panelStartFolder.Controls.Add(this.buttonOpenStartFolder);
            this.panelStartFolder.Controls.Add(this.label1);
            this.panelStartFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStartFolder.Location = new System.Drawing.Point(0, 0);
            this.panelStartFolder.Name = "panelStartFolder";
            this.panelStartFolder.Size = new System.Drawing.Size(546, 21);
            this.panelStartFolder.TabIndex = 2;
            // 
            // textStartFolder
            // 
            this.textStartFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textStartFolder.Location = new System.Drawing.Point(91, 0);
            this.textStartFolder.Name = "textStartFolder";
            this.textStartFolder.Size = new System.Drawing.Size(430, 20);
            this.textStartFolder.TabIndex = 1;
            this.toolTip.SetToolTip(this.textStartFolder, resources.GetString("textStartFolder.ToolTip"));
            // 
            // panelEndFolder
            // 
            this.panelEndFolder.Controls.Add(this.textEndFolder);
            this.panelEndFolder.Controls.Add(this.buttonOpenEndFolder);
            this.panelEndFolder.Controls.Add(this.label2);
            this.panelEndFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEndFolder.Location = new System.Drawing.Point(0, 21);
            this.panelEndFolder.Name = "panelEndFolder";
            this.panelEndFolder.Size = new System.Drawing.Size(546, 21);
            this.panelEndFolder.TabIndex = 3;
            // 
            // textEndFolder
            // 
            this.textEndFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEndFolder.Location = new System.Drawing.Point(91, 0);
            this.textEndFolder.Name = "textEndFolder";
            this.textEndFolder.Size = new System.Drawing.Size(430, 20);
            this.textEndFolder.TabIndex = 3;
            this.toolTip.SetToolTip(this.textEndFolder, resources.GetString("textEndFolder.ToolTip"));
            // 
            // buttonOpenEndFolder
            // 
            this.buttonOpenEndFolder.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonOpenEndFolder.Location = new System.Drawing.Point(521, 0);
            this.buttonOpenEndFolder.Name = "buttonOpenEndFolder";
            this.buttonOpenEndFolder.Size = new System.Drawing.Size(25, 21);
            this.buttonOpenEndFolder.TabIndex = 4;
            this.buttonOpenEndFolder.Text = "...";
            this.buttonOpenEndFolder.UseVisualStyleBackColor = true;
            this.buttonOpenEndFolder.Click += new System.EventHandler(this.buttonOpenEndFolder_Click);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "End Folder:";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.label4);
            this.panelButtons.Controls.Add(this.label3);
            this.panelButtons.Controls.Add(this.comboMaxPicsPerFile);
            this.panelButtons.Controls.Add(this.ChkConfirmOverwrite);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 127);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(546, 57);
            this.panelButtons.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Confirm overwrite files:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Max pictures per file:";
            // 
            // comboMaxPicsPerFile
            // 
            this.comboMaxPicsPerFile.FormattingEnabled = true;
            this.comboMaxPicsPerFile.Items.AddRange(new object[] {
            "10",
            "25",
            "50",
            "100",
            "150",
            "Unlimited"});
            this.comboMaxPicsPerFile.Location = new System.Drawing.Point(130, 24);
            this.comboMaxPicsPerFile.Name = "comboMaxPicsPerFile";
            this.comboMaxPicsPerFile.Size = new System.Drawing.Size(121, 21);
            this.comboMaxPicsPerFile.TabIndex = 8;
            this.toolTip.SetToolTip(this.comboMaxPicsPerFile, "Indicates the maximum if picture files included in a single \r\nWord document.\r\nif " +
        "there are more picture files in a folder than this setting allows, \r\nseveral wor" +
        "d documents will be created.");
            this.comboMaxPicsPerFile.SelectedIndexChanged += new System.EventHandler(this.comboMaxPicsPerFile_SelectedIndexChanged);
            // 
            // ChkConfirmOverwrite
            // 
            this.ChkConfirmOverwrite.AutoSize = true;
            this.ChkConfirmOverwrite.Location = new System.Drawing.Point(130, 8);
            this.ChkConfirmOverwrite.Name = "ChkConfirmOverwrite";
            this.ChkConfirmOverwrite.Size = new System.Drawing.Size(15, 14);
            this.ChkConfirmOverwrite.TabIndex = 7;
            this.toolTip.SetToolTip(this.ChkConfirmOverwrite, resources.GetString("ChkConfirmOverwrite.ToolTip"));
            this.ChkConfirmOverwrite.UseVisualStyleBackColor = true;
            this.ChkConfirmOverwrite.CheckedChanged += new System.EventHandler(this.ChkConfirmOverwrite_CheckedChanged);
            // 
            // panelProgress
            // 
            this.panelProgress.Controls.Add(this.buttonSave);
            this.panelProgress.Controls.Add(this.buttonGo);
            this.panelProgress.Controls.Add(this.progressBar);
            this.panelProgress.Controls.Add(this.labelProgress);
            this.panelProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelProgress.Location = new System.Drawing.Point(0, 184);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(546, 43);
            this.panelProgress.TabIndex = 10;
            // 
            // buttonGo
            // 
            this.buttonGo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonGo.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonGo.Location = new System.Drawing.Point(471, 0);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 30);
            this.buttonGo.TabIndex = 12;
            this.buttonGo.Text = "Go...";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(0, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(546, 30);
            this.progressBar.TabIndex = 11;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelProgress.Location = new System.Drawing.Point(0, 30);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(0, 13);
            this.labelProgress.TabIndex = 10;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // panelExcludeFiles
            // 
            this.panelExcludeFiles.Controls.Add(this.textExcludeFiles);
            this.panelExcludeFiles.Controls.Add(this.labelExcludeFiles);
            this.panelExcludeFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExcludeFiles.Location = new System.Drawing.Point(0, 63);
            this.panelExcludeFiles.Name = "panelExcludeFiles";
            this.panelExcludeFiles.Size = new System.Drawing.Size(546, 21);
            this.panelExcludeFiles.TabIndex = 2;
            // 
            // textExcludeFiles
            // 
            this.textExcludeFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textExcludeFiles.Location = new System.Drawing.Point(91, 0);
            this.textExcludeFiles.Name = "textExcludeFiles";
            this.textExcludeFiles.Size = new System.Drawing.Size(455, 20);
            this.textExcludeFiles.TabIndex = 6;
            this.toolTip.SetToolTip(this.textExcludeFiles, "A semi-colon delimited list of file masks, to indicate which picture files \r\nwill" +
        " be not imported.\r\nFor example *small*");
            // 
            // labelExcludeFiles
            // 
            this.labelExcludeFiles.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelExcludeFiles.Location = new System.Drawing.Point(0, 0);
            this.labelExcludeFiles.Name = "labelExcludeFiles";
            this.labelExcludeFiles.Size = new System.Drawing.Size(91, 21);
            this.labelExcludeFiles.TabIndex = 15;
            this.labelExcludeFiles.Text = "Exclude Files:";
            // 
            // panelIncludeFiles
            // 
            this.panelIncludeFiles.Controls.Add(this.textIncludeFiles);
            this.panelIncludeFiles.Controls.Add(this.label5);
            this.panelIncludeFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelIncludeFiles.Location = new System.Drawing.Point(0, 42);
            this.panelIncludeFiles.Name = "panelIncludeFiles";
            this.panelIncludeFiles.Size = new System.Drawing.Size(546, 21);
            this.panelIncludeFiles.TabIndex = 2;
            // 
            // textIncludeFiles
            // 
            this.textIncludeFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textIncludeFiles.Location = new System.Drawing.Point(91, 0);
            this.textIncludeFiles.Name = "textIncludeFiles";
            this.textIncludeFiles.Size = new System.Drawing.Size(455, 20);
            this.textIncludeFiles.TabIndex = 5;
            this.toolTip.SetToolTip(this.textIncludeFiles, "A semi-colon delimited list of file masks, to indicate which picture files \r\nwill" +
        " be imported.\r\nFor example *.jpg;*.jpeg");
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 21);
            this.label5.TabIndex = 13;
            this.label5.Text = "Include Files:";
            // 
            // toolTip
            // 
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // textTemplate
            // 
            this.textTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textTemplate.Location = new System.Drawing.Point(91, 0);
            this.textTemplate.Name = "textTemplate";
            this.textTemplate.Size = new System.Drawing.Size(430, 20);
            this.textTemplate.TabIndex = 1;
            this.toolTip.SetToolTip(this.textTemplate, resources.GetString("textTemplate.ToolTip"));
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textTemplate);
            this.panel1.Controls.Add(this.buttonOpenTemplate);
            this.panel1.Controls.Add(this.labelTemplate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 21);
            this.panel1.TabIndex = 11;
            // 
            // buttonOpenTemplate
            // 
            this.buttonOpenTemplate.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonOpenTemplate.Location = new System.Drawing.Point(521, 0);
            this.buttonOpenTemplate.Name = "buttonOpenTemplate";
            this.buttonOpenTemplate.Size = new System.Drawing.Size(25, 21);
            this.buttonOpenTemplate.TabIndex = 2;
            this.buttonOpenTemplate.Text = "...";
            this.buttonOpenTemplate.UseVisualStyleBackColor = true;
            this.buttonOpenTemplate.Click += new System.EventHandler(this.buttonOpenTemplate_Click);
            // 
            // labelTemplate
            // 
            this.labelTemplate.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelTemplate.Location = new System.Drawing.Point(0, 0);
            this.labelTemplate.Name = "labelTemplate";
            this.labelTemplate.Size = new System.Drawing.Size(91, 21);
            this.labelTemplate.TabIndex = 0;
            this.labelTemplate.Text = "Template:";
            // 
            // buttonSave
            // 
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSave.Location = new System.Drawing.Point(396, 0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 30);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormImportPictures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 227);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelProgress);
            this.Controls.Add(this.panelExcludeFiles);
            this.Controls.Add(this.panelIncludeFiles);
            this.Controls.Add(this.panelEndFolder);
            this.Controls.Add(this.panelStartFolder);
            this.Name = "FormImportPictures";
            this.Text = "Import Pictures";
            this.panelStartFolder.ResumeLayout(false);
            this.panelStartFolder.PerformLayout();
            this.panelEndFolder.ResumeLayout(false);
            this.panelEndFolder.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            this.panelExcludeFiles.ResumeLayout(false);
            this.panelExcludeFiles.PerformLayout();
            this.panelIncludeFiles.ResumeLayout(false);
            this.panelIncludeFiles.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOpenStartFolder;
        private System.Windows.Forms.Panel panelStartFolder;
        private System.Windows.Forms.TextBox textStartFolder;
        private System.Windows.Forms.Panel panelEndFolder;
        private System.Windows.Forms.TextBox textEndFolder;
        private System.Windows.Forms.Button buttonOpenEndFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.ComboBox comboMaxPicsPerFile;
        private System.Windows.Forms.CheckBox ChkConfirmOverwrite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel panelExcludeFiles;
        private System.Windows.Forms.Panel panelIncludeFiles;
        private System.Windows.Forms.Label labelExcludeFiles;
        private System.Windows.Forms.TextBox textIncludeFiles;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.TextBox textExcludeFiles;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textTemplate;
        private System.Windows.Forms.Button buttonOpenTemplate;
        private System.Windows.Forms.Label labelTemplate;
        private System.Windows.Forms.Button buttonSave;
    }
}