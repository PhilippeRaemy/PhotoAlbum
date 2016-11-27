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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOpenStartFolder = new System.Windows.Forms.Button();
            this.panelStartFolder = new System.Windows.Forms.Panel();
            this.textStartFolder = new System.Windows.Forms.TextBox();
            this.panelEndFolder = new System.Windows.Forms.Panel();
            this.textEndFolder = new System.Windows.Forms.TextBox();
            this.buttonOpenEndFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonGo = new System.Windows.Forms.Button();
            this.comboMaxPicsPerFile = new System.Windows.Forms.ComboBox();
            this.ChkConfirmOverwrite = new System.Windows.Forms.CheckBox();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelStartFolder.SuspendLayout();
            this.panelEndFolder.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start Folder";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonOpenStartFolder
            // 
            this.buttonOpenStartFolder.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonOpenStartFolder.Location = new System.Drawing.Point(685, 0);
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
            this.panelStartFolder.Size = new System.Drawing.Size(710, 21);
            this.panelStartFolder.TabIndex = 2;
            this.panelStartFolder.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // textStartFolder
            // 
            this.textStartFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textStartFolder.Location = new System.Drawing.Point(65, 0);
            this.textStartFolder.Name = "textStartFolder";
            this.textStartFolder.Size = new System.Drawing.Size(620, 20);
            this.textStartFolder.TabIndex = 3;
            // 
            // panelEndFolder
            // 
            this.panelEndFolder.Controls.Add(this.textEndFolder);
            this.panelEndFolder.Controls.Add(this.buttonOpenEndFolder);
            this.panelEndFolder.Controls.Add(this.label2);
            this.panelEndFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEndFolder.Location = new System.Drawing.Point(0, 21);
            this.panelEndFolder.Name = "panelEndFolder";
            this.panelEndFolder.Size = new System.Drawing.Size(710, 21);
            this.panelEndFolder.TabIndex = 3;
            // 
            // textEndFolder
            // 
            this.textEndFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEndFolder.Location = new System.Drawing.Point(65, 0);
            this.textEndFolder.Name = "textEndFolder";
            this.textEndFolder.Size = new System.Drawing.Size(620, 20);
            this.textEndFolder.TabIndex = 3;
            // 
            // buttonOpenEndFolder
            // 
            this.buttonOpenEndFolder.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonOpenEndFolder.Location = new System.Drawing.Point(685, 0);
            this.buttonOpenEndFolder.Name = "buttonOpenEndFolder";
            this.buttonOpenEndFolder.Size = new System.Drawing.Size(25, 21);
            this.buttonOpenEndFolder.TabIndex = 2;
            this.buttonOpenEndFolder.Text = "...";
            this.buttonOpenEndFolder.UseVisualStyleBackColor = true;
            this.buttonOpenEndFolder.Click += new System.EventHandler(this.buttonOpenEndFolder_Click);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "End Folder";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.label4);
            this.panelButtons.Controls.Add(this.label3);
            this.panelButtons.Controls.Add(this.buttonGo);
            this.panelButtons.Controls.Add(this.comboMaxPicsPerFile);
            this.panelButtons.Controls.Add(this.ChkConfirmOverwrite);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 71);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(710, 57);
            this.panelButtons.TabIndex = 7;
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
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(632, 28);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 23);
            this.buttonGo.TabIndex = 9;
            this.buttonGo.Text = "Go...";
            this.buttonGo.UseVisualStyleBackColor = true;
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
            this.comboMaxPicsPerFile.SelectedIndexChanged += new System.EventHandler(this.comboMaxPicsPerFile_SelectedIndexChanged);
            // 
            // ChkConfirmOverwrite
            // 
            this.ChkConfirmOverwrite.AutoSize = true;
            this.ChkConfirmOverwrite.Location = new System.Drawing.Point(130, 8);
            this.ChkConfirmOverwrite.Name = "ChkConfirmOverwrite";
            this.ChkConfirmOverwrite.Size = new System.Drawing.Size(15, 14);
            this.ChkConfirmOverwrite.TabIndex = 7;
            this.ChkConfirmOverwrite.UseVisualStyleBackColor = true;
            this.ChkConfirmOverwrite.CheckedChanged += new System.EventHandler(this.ChkConfirmOverwrite_CheckedChanged);
            // 
            // panelProgress
            // 
            this.panelProgress.Controls.Add(this.progressBar);
            this.panelProgress.Controls.Add(this.labelProgress);
            this.panelProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelProgress.Location = new System.Drawing.Point(0, 128);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(710, 43);
            this.panelProgress.TabIndex = 10;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar.Location = new System.Drawing.Point(0, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(710, 23);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Confirm overwrite files:";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // FormImportPictures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 171);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelProgress);
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
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.ComboBox comboMaxPicsPerFile;
        private System.Windows.Forms.CheckBox ChkConfirmOverwrite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}