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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PictureSorterForm));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pickDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.previousFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInWindowsExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortFilesBySignatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RotateLeftClock = new System.Windows.Forms.ToolStripMenuItem();
            this.RotateLeftAnti = new System.Windows.Forms.ToolStripMenuItem();
            this.RotateRightClock = new System.Windows.Forms.ToolStripMenuItem();
            this.RotateRightAnti = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveLeftToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moreRightToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftPreviousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightPreviousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.buttonNavigateBothRight = new System.Windows.Forms.Button();
            this.buttonNavigateBothLeft = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonShelfRight = new System.Windows.Forms.Button();
            this.buttonNavigateRightLeft = new System.Windows.Forms.Button();
            this.buttonRotateRightClock = new System.Windows.Forms.Button();
            this.buttonRotateRightAnti = new System.Windows.Forms.Button();
            this.buttonNavigateRightRight = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonShelfLeft = new System.Windows.Forms.Button();
            this.buttonNavigateLeftLeft = new System.Windows.Forms.Button();
            this.buttonRotateLeftClock = new System.Windows.Forms.Button();
            this.buttonRotateLeftAnti = new System.Windows.Forms.Button();
            this.buttonNavigateRight = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelLeft = new System.Windows.Forms.Label();
            this.labelRight = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.contextMenuStripPicture = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1119, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pickDirectoryToolStripMenuItem,
            this.nextFolder,
            this.previousFolder,
            this.renameToolStripMenuItem,
            this.openInWindowsExplorerToolStripMenuItem,
            this.sortFilesBySignatureToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.fileToolStripMenuItem.Text = "F&older";
            // 
            // pickDirectoryToolStripMenuItem
            // 
            this.pickDirectoryToolStripMenuItem.Name = "pickDirectoryToolStripMenuItem";
            this.pickDirectoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.pickDirectoryToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.pickDirectoryToolStripMenuItem.Text = "&Open";
            this.pickDirectoryToolStripMenuItem.Click += new System.EventHandler(this.pickDirectoryToolStripMenuItem_Click);
            // 
            // nextFolder
            // 
            this.nextFolder.Name = "nextFolder";
            this.nextFolder.ShortcutKeyDisplayString = "PgDn";
            this.nextFolder.Size = new System.Drawing.Size(254, 22);
            this.nextFolder.Text = "Ne&xt";
            this.nextFolder.Click += new System.EventHandler(this.nextFolder_Click);
            // 
            // previousFolder
            // 
            this.previousFolder.Name = "previousFolder";
            this.previousFolder.ShortcutKeyDisplayString = "PgUp";
            this.previousFolder.Size = new System.Drawing.Size(254, 22);
            this.previousFolder.Text = "&Previous";
            this.previousFolder.Click += new System.EventHandler(this.previousFolder_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.renameToolStripMenuItem.Text = "Rena&me";
            // 
            // openInWindowsExplorerToolStripMenuItem
            // 
            this.openInWindowsExplorerToolStripMenuItem.Name = "openInWindowsExplorerToolStripMenuItem";
            this.openInWindowsExplorerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.openInWindowsExplorerToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.openInWindowsExplorerToolStripMenuItem.Text = "Open in Windows &Explorer";
            this.openInWindowsExplorerToolStripMenuItem.Click += new System.EventHandler(this.openInWindowsExplorerToolStripMenuItem_Click);
            // 
            // sortFilesBySignatureToolStripMenuItem
            // 
            this.sortFilesBySignatureToolStripMenuItem.Name = "sortFilesBySignatureToolStripMenuItem";
            this.sortFilesBySignatureToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.sortFilesBySignatureToolStripMenuItem.Text = "Sort files by signature";
            this.sortFilesBySignatureToolStripMenuItem.Click += new System.EventHandler(this.SortFilesBySignatureToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archiveToolStripMenuItem,
            this.rotateToolStripMenuItem,
            this.moveToToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "&File";
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leftToolStripMenuItem,
            this.rightToolStripMenuItem});
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.archiveToolStripMenuItem.Text = "Archive";
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.ShortcutKeyDisplayString = "F1, 1";
            this.leftToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.leftToolStripMenuItem.Text = "Left";
            this.leftToolStripMenuItem.Click += new System.EventHandler(this.archiveLeftToolStripMenuItem_Click);
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.ShortcutKeyDisplayString = "F2, 2";
            this.rightToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rotateToolStripMenuItem.Text = "Rotate";
            // 
            // RotateLeftClock
            // 
            this.RotateLeftClock.Name = "RotateLeftClock";
            this.RotateLeftClock.ShortcutKeyDisplayString = "Q";
            this.RotateLeftClock.Size = new System.Drawing.Size(180, 22);
            this.RotateLeftClock.Text = "Left clock";
            this.RotateLeftClock.Click += new System.EventHandler(this.RotateLeftClock_Click);
            // 
            // RotateLeftAnti
            // 
            this.RotateLeftAnti.Name = "RotateLeftAnti";
            this.RotateLeftAnti.ShortcutKeyDisplayString = "A";
            this.RotateLeftAnti.Size = new System.Drawing.Size(180, 22);
            this.RotateLeftAnti.Text = "Left anti";
            this.RotateLeftAnti.Click += new System.EventHandler(this.RotateLeftAnti_Click);
            // 
            // RotateRightClock
            // 
            this.RotateRightClock.Name = "RotateRightClock";
            this.RotateRightClock.ShortcutKeyDisplayString = "W";
            this.RotateRightClock.Size = new System.Drawing.Size(180, 22);
            this.RotateRightClock.Text = "Right clock";
            this.RotateRightClock.Click += new System.EventHandler(this.RotateRightClock_Click);
            // 
            // RotateRightAnti
            // 
            this.RotateRightAnti.Name = "RotateRightAnti";
            this.RotateRightAnti.ShortcutKeyDisplayString = "S";
            this.RotateRightAnti.Size = new System.Drawing.Size(180, 22);
            this.RotateRightAnti.Text = "Right anti";
            this.RotateRightAnti.Click += new System.EventHandler(this.RotateRightAnti_Click);
            // 
            // moveToToolStripMenuItem
            // 
            this.moveToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveLeftToToolStripMenuItem,
            this.moreRightToToolStripMenuItem});
            this.moveToToolStripMenuItem.Name = "moveToToolStripMenuItem";
            this.moveToToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.moveToToolStripMenuItem.Text = "&Move to...";
            // 
            // moveLeftToToolStripMenuItem
            // 
            this.moveLeftToToolStripMenuItem.Name = "moveLeftToToolStripMenuItem";
            this.moveLeftToToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.moveLeftToToolStripMenuItem.Text = "Move Left to ...";
            // 
            // moreRightToToolStripMenuItem
            // 
            this.moreRightToToolStripMenuItem.Name = "moreRightToToolStripMenuItem";
            this.moreRightToToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.moreRightToToolStripMenuItem.Text = "Move Right to ...";
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
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1119, 54);
            this.panel1.TabIndex = 7;
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel5.Controls.Add(this.buttonUndo);
            this.panel5.Controls.Add(this.buttonNavigateBothRight);
            this.panel5.Controls.Add(this.buttonNavigateBothLeft);
            this.panel5.Location = new System.Drawing.Point(506, 20);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(96, 32);
            this.panel5.TabIndex = 29;
            // 
            // buttonUndo
            // 
            this.buttonUndo.Enabled = false;
            this.buttonUndo.Image = global::PicturesSorter.Properties.Resources.Undo;
            this.buttonUndo.Location = new System.Drawing.Point(32, 0);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(32, 32);
            this.buttonUndo.TabIndex = 27;
            this.buttonUndo.UseVisualStyleBackColor = true;
            this.buttonUndo.Click += new System.EventHandler(this.buttonUndo_Click);
            // 
            // buttonNavigateBothRight
            // 
            this.buttonNavigateBothRight.Image = global::PicturesSorter.Properties.Resources.SmallRightSync;
            this.buttonNavigateBothRight.Location = new System.Drawing.Point(64, 0);
            this.buttonNavigateBothRight.Name = "buttonNavigateBothRight";
            this.buttonNavigateBothRight.Size = new System.Drawing.Size(32, 32);
            this.buttonNavigateBothRight.TabIndex = 26;
            this.buttonNavigateBothRight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonNavigateBothRight.UseVisualStyleBackColor = true;
            this.buttonNavigateBothRight.Click += new System.EventHandler(this.buttonNavigateBothRight_Click);
            // 
            // buttonNavigateBothLeft
            // 
            this.buttonNavigateBothLeft.Image = global::PicturesSorter.Properties.Resources.SmallLeftSync;
            this.buttonNavigateBothLeft.Location = new System.Drawing.Point(0, 0);
            this.buttonNavigateBothLeft.Name = "buttonNavigateBothLeft";
            this.buttonNavigateBothLeft.Size = new System.Drawing.Size(32, 32);
            this.buttonNavigateBothLeft.TabIndex = 25;
            this.buttonNavigateBothLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonNavigateBothLeft.UseVisualStyleBackColor = true;
            this.buttonNavigateBothLeft.Click += new System.EventHandler(this.buttonNavigateBothLeft_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.buttonShelfRight);
            this.panel4.Controls.Add(this.buttonNavigateRightLeft);
            this.panel4.Controls.Add(this.buttonRotateRightClock);
            this.panel4.Controls.Add(this.buttonRotateRightAnti);
            this.panel4.Controls.Add(this.buttonNavigateRightRight);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(959, 20);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(160, 34);
            this.panel4.TabIndex = 28;
            // 
            // buttonShelfRight
            // 
            this.buttonShelfRight.Image = global::PicturesSorter.Properties.Resources.SmallShelve;
            this.buttonShelfRight.Location = new System.Drawing.Point(128, 0);
            this.buttonShelfRight.Name = "buttonShelfRight";
            this.buttonShelfRight.Size = new System.Drawing.Size(32, 32);
            this.buttonShelfRight.TabIndex = 24;
            this.buttonShelfRight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonShelfRight.UseVisualStyleBackColor = true;
            this.buttonShelfRight.Click += new System.EventHandler(this.buttonShelfRight_Click);
            // 
            // buttonNavigateRightLeft
            // 
            this.buttonNavigateRightLeft.Image = global::PicturesSorter.Properties.Resources.SmallLeft;
            this.buttonNavigateRightLeft.Location = new System.Drawing.Point(0, 0);
            this.buttonNavigateRightLeft.Name = "buttonNavigateRightLeft";
            this.buttonNavigateRightLeft.Size = new System.Drawing.Size(32, 32);
            this.buttonNavigateRightLeft.TabIndex = 23;
            this.buttonNavigateRightLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonNavigateRightLeft.UseVisualStyleBackColor = true;
            this.buttonNavigateRightLeft.Click += new System.EventHandler(this.buttonNavigateRightLeft_Click);
            // 
            // buttonRotateRightClock
            // 
            this.buttonRotateRightClock.Image = global::PicturesSorter.Properties.Resources.SmallRotateRight;
            this.buttonRotateRightClock.Location = new System.Drawing.Point(96, 0);
            this.buttonRotateRightClock.Name = "buttonRotateRightClock";
            this.buttonRotateRightClock.Size = new System.Drawing.Size(32, 32);
            this.buttonRotateRightClock.TabIndex = 22;
            this.buttonRotateRightClock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonRotateRightClock.UseVisualStyleBackColor = true;
            this.buttonRotateRightClock.Click += new System.EventHandler(this.RotateRightClock_Click);
            // 
            // buttonRotateRightAnti
            // 
            this.buttonRotateRightAnti.Image = ((System.Drawing.Image)(resources.GetObject("buttonRotateRightAnti.Image")));
            this.buttonRotateRightAnti.Location = new System.Drawing.Point(64, 0);
            this.buttonRotateRightAnti.Name = "buttonRotateRightAnti";
            this.buttonRotateRightAnti.Size = new System.Drawing.Size(32, 32);
            this.buttonRotateRightAnti.TabIndex = 21;
            this.buttonRotateRightAnti.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonRotateRightAnti.UseVisualStyleBackColor = true;
            this.buttonRotateRightAnti.Click += new System.EventHandler(this.RotateRightAnti_Click);
            // 
            // buttonNavigateRightRight
            // 
            this.buttonNavigateRightRight.Image = global::PicturesSorter.Properties.Resources.SmallRight;
            this.buttonNavigateRightRight.Location = new System.Drawing.Point(32, 0);
            this.buttonNavigateRightRight.Name = "buttonNavigateRightRight";
            this.buttonNavigateRightRight.Size = new System.Drawing.Size(32, 32);
            this.buttonNavigateRightRight.TabIndex = 20;
            this.buttonNavigateRightRight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonNavigateRightRight.UseVisualStyleBackColor = true;
            this.buttonNavigateRightRight.Click += new System.EventHandler(this.buttonNavigateRightRight_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonShelfLeft);
            this.panel2.Controls.Add(this.buttonNavigateLeftLeft);
            this.panel2.Controls.Add(this.buttonRotateLeftClock);
            this.panel2.Controls.Add(this.buttonRotateLeftAnti);
            this.panel2.Controls.Add(this.buttonNavigateRight);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(160, 34);
            this.panel2.TabIndex = 27;
            // 
            // buttonShelfLeft
            // 
            this.buttonShelfLeft.Image = global::PicturesSorter.Properties.Resources.SmallShelve;
            this.buttonShelfLeft.Location = new System.Drawing.Point(128, 0);
            this.buttonShelfLeft.Name = "buttonShelfLeft";
            this.buttonShelfLeft.Size = new System.Drawing.Size(32, 32);
            this.buttonShelfLeft.TabIndex = 24;
            this.buttonShelfLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonShelfLeft.UseVisualStyleBackColor = true;
            this.buttonShelfLeft.Click += new System.EventHandler(this.buttonShelfLeft_Click);
            // 
            // buttonNavigateLeftLeft
            // 
            this.buttonNavigateLeftLeft.Image = global::PicturesSorter.Properties.Resources.SmallLeft;
            this.buttonNavigateLeftLeft.Location = new System.Drawing.Point(0, 0);
            this.buttonNavigateLeftLeft.Name = "buttonNavigateLeftLeft";
            this.buttonNavigateLeftLeft.Size = new System.Drawing.Size(32, 32);
            this.buttonNavigateLeftLeft.TabIndex = 23;
            this.buttonNavigateLeftLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonNavigateLeftLeft.UseVisualStyleBackColor = true;
            this.buttonNavigateLeftLeft.Click += new System.EventHandler(this.buttonNavigateLeftLeft_Click);
            // 
            // buttonRotateLeftClock
            // 
            this.buttonRotateLeftClock.Image = global::PicturesSorter.Properties.Resources.SmallRotateRight;
            this.buttonRotateLeftClock.Location = new System.Drawing.Point(96, 0);
            this.buttonRotateLeftClock.Name = "buttonRotateLeftClock";
            this.buttonRotateLeftClock.Size = new System.Drawing.Size(32, 32);
            this.buttonRotateLeftClock.TabIndex = 22;
            this.buttonRotateLeftClock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonRotateLeftClock.UseVisualStyleBackColor = true;
            this.buttonRotateLeftClock.Click += new System.EventHandler(this.RotateLeftClock_Click);
            // 
            // buttonRotateLeftAnti
            // 
            this.buttonRotateLeftAnti.Image = ((System.Drawing.Image)(resources.GetObject("buttonRotateLeftAnti.Image")));
            this.buttonRotateLeftAnti.Location = new System.Drawing.Point(64, 0);
            this.buttonRotateLeftAnti.Name = "buttonRotateLeftAnti";
            this.buttonRotateLeftAnti.Size = new System.Drawing.Size(32, 32);
            this.buttonRotateLeftAnti.TabIndex = 21;
            this.buttonRotateLeftAnti.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonRotateLeftAnti.UseVisualStyleBackColor = true;
            this.buttonRotateLeftAnti.Click += new System.EventHandler(this.RotateLeftAnti_Click);
            // 
            // buttonNavigateRight
            // 
            this.buttonNavigateRight.Image = global::PicturesSorter.Properties.Resources.SmallRight;
            this.buttonNavigateRight.Location = new System.Drawing.Point(32, 0);
            this.buttonNavigateRight.Name = "buttonNavigateRight";
            this.buttonNavigateRight.Size = new System.Drawing.Size(32, 32);
            this.buttonNavigateRight.TabIndex = 20;
            this.buttonNavigateRight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonNavigateRight.UseVisualStyleBackColor = true;
            this.buttonNavigateRight.Click += new System.EventHandler(this.buttonNavigateRight_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labelLeft);
            this.panel3.Controls.Add(this.labelRight);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1119, 20);
            this.panel3.TabIndex = 26;
            // 
            // labelLeft
            // 
            this.labelLeft.AutoSize = true;
            this.labelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelLeft.Location = new System.Drawing.Point(0, 0);
            this.labelLeft.Name = "labelLeft";
            this.labelLeft.Size = new System.Drawing.Size(35, 13);
            this.labelLeft.TabIndex = 11;
            this.labelLeft.Text = "label1";
            // 
            // labelRight
            // 
            this.labelRight.AutoSize = true;
            this.labelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelRight.Location = new System.Drawing.Point(1084, 0);
            this.labelRight.Name = "labelRight";
            this.labelRight.Size = new System.Drawing.Size(35, 13);
            this.labelRight.TabIndex = 7;
            this.labelRight.Text = "label3";
            // 
            // pictureBox2
            // 
            this.pictureBox2.ContextMenuStrip = this.contextMenuStripPicture;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Location = new System.Drawing.Point(602, 78);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(517, 531);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseClick);
            this.pictureBox2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDoubleClick);
            // 
            // contextMenuStripPicture
            // 
            this.contextMenuStripPicture.Name = "contextMenuStripPicture";
            this.contextMenuStripPicture.Size = new System.Drawing.Size(61, 4);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.contextMenuStripPicture;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 78);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(505, 531);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            // 
            // PictureSorterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 609);
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
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private FolderBrowserDialog folderBrowserDialog;
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
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Panel panel1;
        private Panel panel5;
        private Button buttonNavigateBothRight;
        private Button buttonNavigateBothLeft;
        private Panel panel4;
        private Button buttonShelfRight;
        private Button buttonNavigateRightLeft;
        private Button buttonRotateRightClock;
        private Button buttonRotateRightAnti;
        private Button buttonNavigateRightRight;
        private Panel panel2;
        private Button buttonShelfLeft;
        private Button buttonNavigateLeftLeft;
        private Button buttonRotateLeftClock;
        private Button buttonRotateLeftAnti;
        private Button buttonNavigateRight;
        private Panel panel3;
        private Label labelLeft;
        private Label labelRight;
        private Button buttonUndo;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ToolStripMenuItem moveToToolStripMenuItem;
        private ToolStripMenuItem moveLeftToToolStripMenuItem;
        private ToolStripMenuItem moreRightToToolStripMenuItem;
        private ContextMenuStrip contextMenuStripPicture;
        private ToolStripMenuItem contextMenuStripPictureArchive;
        private ToolStripMenuItem contextMenuStripPictureRotateLeft;
        private ToolStripMenuItem contextMenuStripPictureRotateRight;
        private ToolStripMenuItem contextMenuStripPictureMoveTo;
        private ToolStripMenuItem sortFilesBySignatureToolStripMenuItem;
    }
}

