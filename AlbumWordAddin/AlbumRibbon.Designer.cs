namespace AlbumWordAddin
{
    partial class AlbumRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public AlbumRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumRibbon));
            this.TabAddIns = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.ButtonRemoveEmptyPages = this.Factory.CreateRibbonButton();
            this.ButtonSelectShapesOnPage = this.Factory.CreateRibbonButton();
            this.TabAddIns.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabAddIns
            // 
            this.TabAddIns.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.TabAddIns.Groups.Add(this.group1);
            this.TabAddIns.Label = "Photo Album";
            this.TabAddIns.Name = "TabAddIns";
            // 
            // group1
            // 
            this.group1.Items.Add(this.ButtonRemoveEmptyPages);
            this.group1.Items.Add(this.ButtonSelectShapesOnPage);
            this.group1.Label = "group1";
            this.group1.Name = "group1";
            // 
            // ButtonRemoveEmptyPages
            // 
            this.ButtonRemoveEmptyPages.Image = ((System.Drawing.Image)(resources.GetObject("ButtonRemoveEmptyPages.Image")));
            this.ButtonRemoveEmptyPages.Label = "Remove empty pages";
            this.ButtonRemoveEmptyPages.Name = "ButtonRemoveEmptyPages";
            this.ButtonRemoveEmptyPages.ShowImage = true;
            this.ButtonRemoveEmptyPages.SuperTip = "Tip to button 1";
            this.ButtonRemoveEmptyPages.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonRemoveEmptyPages_Click);
            // 
            // ButtonSelectShapesOnPage
            // 
            this.ButtonSelectShapesOnPage.Label = "Select Images on Page";
            this.ButtonSelectShapesOnPage.Name = "ButtonSelectShapesOnPage";
            this.ButtonSelectShapesOnPage.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSelectShapesOnPage_Click);
            // 
            // AlbumRibbon
            // 
            this.Name = "AlbumRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.TabAddIns);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.AlbumRibbon_Load);
            this.TabAddIns.ResumeLayout(false);
            this.TabAddIns.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab TabAddIns;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ButtonRemoveEmptyPages;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ButtonSelectShapesOnPage;
    }

    partial class ThisRibbonCollection
    {
        internal AlbumRibbon AlbumRibbon
        {
            get { return this.GetRibbon<AlbumRibbon>(); }
        }
    }
}
