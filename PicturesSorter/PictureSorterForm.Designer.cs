namespace PicturesSorter
{
    using System.ComponentModel;
    using System.Windows.Forms;

    partial class PictureSorterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PictureSorterForm));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pickDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.previousFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.openInWindowsExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftPreviousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightPreviousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RotateLeftClock = new System.Windows.Forms.ToolStripMenuItem();
            this.RotateLeftAnti = new System.Windows.Forms.ToolStripMenuItem();
            this.RotateRightClock = new System.Windows.Forms.ToolStripMenuItem();
            this.RotateRightAnti = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(647, 37);
            this.panel1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(612, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(647, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pickDirectoryToolStripMenuItem,
            this.nextFolder,
            this.previousFolder,
            this.openInWindowsExplorerToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.fileToolStripMenuItem.Text = "&Folder";
            // 
            // pickDirectoryToolStripMenuItem
            // 
            this.pickDirectoryToolStripMenuItem.Name = "pickDirectoryToolStripMenuItem";
            this.pickDirectoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.pickDirectoryToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.pickDirectoryToolStripMenuItem.Text = "&Open";
            this.pickDirectoryToolStripMenuItem.Click += new System.EventHandler(this.pickDirectoryToolStripMenuItem_Click);
            // 
            // nextFolder
            // 
            this.nextFolder.Name = "nextFolder";
            this.nextFolder.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Next)));
            this.nextFolder.Size = new System.Drawing.Size(253, 22);
            this.nextFolder.Text = "Next";
            this.nextFolder.Click += new System.EventHandler(this.nextFolder_Click);
            // 
            // previousFolder
            // 
            this.previousFolder.Name = "previousFolder";
            this.previousFolder.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.PageUp)));
            this.previousFolder.Size = new System.Drawing.Size(253, 22);
            this.previousFolder.Text = "Previous";
            this.previousFolder.Click += new System.EventHandler(this.previousFolder_Click);
            // 
            // openInWindowsExplorerToolStripMenuItem
            // 
            this.openInWindowsExplorerToolStripMenuItem.Name = "openInWindowsExplorerToolStripMenuItem";
            this.openInWindowsExplorerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.openInWindowsExplorerToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.openInWindowsExplorerToolStripMenuItem.Text = "Open in Windows Explorer";
            this.openInWindowsExplorerToolStripMenuItem.Click += new System.EventHandler(this.openInWindowsExplorerToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previousToolStripMenuItem,
            this.nextToolStripMenuItem,
            this.leftPreviousToolStripMenuItem,
            this.leftNextToolStripMenuItem,
            this.rightPreviousToolStripMenuItem,
            this.rightNextToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // previousToolStripMenuItem
            // 
            this.previousToolStripMenuItem.Name = "previousToolStripMenuItem";
            this.previousToolStripMenuItem.ShortcutKeyDisplayString = "<- Left ";
            this.previousToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.previousToolStripMenuItem.Text = "&Previous";
            this.previousToolStripMenuItem.Click += new System.EventHandler(this.previousToolStripMenuItem_Click);
            // 
            // nextToolStripMenuItem
            // 
            this.nextToolStripMenuItem.Name = "nextToolStripMenuItem";
            this.nextToolStripMenuItem.ShortcutKeyDisplayString = "Right ->";
            this.nextToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.nextToolStripMenuItem.Text = "&Next";
            this.nextToolStripMenuItem.Click += new System.EventHandler(this.nextToolStripMenuItem_Click);
            // 
            // leftPreviousToolStripMenuItem
            // 
            this.leftPreviousToolStripMenuItem.Name = "leftPreviousToolStripMenuItem";
            this.leftPreviousToolStripMenuItem.ShortcutKeyDisplayString = "<-Ctrl+Left ";
            this.leftPreviousToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.leftPreviousToolStripMenuItem.Text = "Left P&revious";
            this.leftPreviousToolStripMenuItem.Click += new System.EventHandler(this.leftPreviousToolStripMenuItem_Click);
            // 
            // leftNextToolStripMenuItem
            // 
            this.leftNextToolStripMenuItem.Name = "leftNextToolStripMenuItem";
            this.leftNextToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Right ->";
            this.leftNextToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.leftNextToolStripMenuItem.Text = "Left Next";
            this.leftNextToolStripMenuItem.Click += new System.EventHandler(this.leftNextToolStripMenuItem_Click);
            // 
            // rightPreviousToolStripMenuItem
            // 
            this.rightPreviousToolStripMenuItem.Name = "rightPreviousToolStripMenuItem";
            this.rightPreviousToolStripMenuItem.ShortcutKeyDisplayString = "<- Ctrl+Shift+Left";
            this.rightPreviousToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.rightPreviousToolStripMenuItem.Text = "Right Previous";
            this.rightPreviousToolStripMenuItem.Click += new System.EventHandler(this.rightPreviousToolStripMenuItem_Click);
            // 
            // rightNextToolStripMenuItem
            // 
            this.rightNextToolStripMenuItem.Name = "rightNextToolStripMenuItem";
            this.rightNextToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+Right->";
            this.rightNextToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.rightNextToolStripMenuItem.Text = "Right N&ext";
            this.rightNextToolStripMenuItem.Click += new System.EventHandler(this.rightNextToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archiveToolStripMenuItem,
            this.rotateToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leftToolStripMenuItem,
            this.rightToolStripMenuItem});
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.archiveToolStripMenuItem.Text = "Archive";
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.ShortcutKeyDisplayString = "F1, 1";
            this.leftToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.leftToolStripMenuItem.Text = "Left";
            this.leftToolStripMenuItem.Click += new System.EventHandler(this.archiveLeftToolStripMenuItem_Click);
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.ShortcutKeyDisplayString = "F2, 2";
            this.rightToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.rightToolStripMenuItem.Text = "Right";
            this.rightToolStripMenuItem.Click += new System.EventHandler(this.archiveRightToolStripMenuItem_Click);
            // 
            // rotateToolStripMenuItem
            // 
            this.rotateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RotateLeftClock,
            this.RotateLeftAnti,
            this.RotateRightClock,
            this.RotateRightAnti});
            this.rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            this.rotateToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.rotateToolStripMenuItem.Text = "Rotate";
            // 
            // RotateLeftClock
            // 
            this.RotateLeftClock.Name = "RotateLeftClock";
            this.RotateLeftClock.ShortcutKeyDisplayString = "";
            this.RotateLeftClock.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.RotateLeftClock.Size = new System.Drawing.Size(205, 22);
            this.RotateLeftClock.Text = "Left clock";
            this.RotateLeftClock.Click += new System.EventHandler(this.RotateLeftClock_Click);
            // 
            // RotateLeftAnti
            // 
            this.RotateLeftAnti.Name = "RotateLeftAnti";
            this.RotateLeftAnti.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.RotateLeftAnti.Size = new System.Drawing.Size(205, 22);
            this.RotateLeftAnti.Text = "Left anti";
            this.RotateLeftAnti.Click += new System.EventHandler(this.RotateLeftAnti_Click);
            // 
            // RotateRightClock
            // 
            this.RotateRightClock.Name = "RotateRightClock";
            this.RotateRightClock.ShortcutKeyDisplayString = "";
            this.RotateRightClock.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.RotateRightClock.Size = new System.Drawing.Size(205, 22);
            this.RotateRightClock.Text = "Right clock";
            this.RotateRightClock.Click += new System.EventHandler(this.RotateRightClock_Click);
            // 
            // RotateRightAnti
            // 
            this.RotateRightAnti.Name = "RotateRightAnti";
            this.RotateRightAnti.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.RotateRightAnti.Size = new System.Drawing.Size(205, 22);
            this.RotateRightAnti.Text = "Right anti";
            this.RotateRightAnti.Click += new System.EventHandler(this.RotateRightAnti_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Location = new System.Drawing.Point(387, 61);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(260, 260);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(252, 260);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // PictureSorterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 321);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PictureSorterForm";
            this.Text = "PictureSorterForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PictureSorterForm_FormClosing);
            this.Load += new System.EventHandler(this.PictureSorterForm_Load);
            this.Resize += new System.EventHandler(this.PictureSorterForm_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private FolderBrowserDialog folderBrowserDialog;
        private Panel panel1;
        private Label label2;
        private Label label1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem pickDirectoryToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem previousToolStripMenuItem;
        private ToolStripMenuItem nextToolStripMenuItem;
        private ToolStripMenuItem leftPreviousToolStripMenuItem;
        private ToolStripMenuItem rightNextToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem nextFolder;
        private ToolStripMenuItem previousFolder;
        private ToolStripMenuItem leftNextToolStripMenuItem;
        private ToolStripMenuItem rightPreviousToolStripMenuItem;
        private ToolStripMenuItem archiveToolStripMenuItem;
        private ToolStripMenuItem leftToolStripMenuItem;
        private ToolStripMenuItem rightToolStripMenuItem;
        private ToolStripMenuItem rotateToolStripMenuItem;
        private ToolStripMenuItem RotateLeftClock;
        private ToolStripMenuItem RotateLeftAnti;
        private ToolStripMenuItem RotateRightClock;
        private ToolStripMenuItem RotateRightAnti;
        private ToolStripMenuItem openInWindowsExplorerToolStripMenuItem;
    }
}

