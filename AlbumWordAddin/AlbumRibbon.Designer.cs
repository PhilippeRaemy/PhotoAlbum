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
            ThisAddIn.ThisRibbon = this;
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
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl1 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl2 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl3 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl1 = this.Factory.CreateRibbonDialogLauncher();
            this.TabAddIns = this.Factory.CreateRibbonTab();
            this.groupPage = this.Factory.CreateRibbonGroup();
            this.ButtonRemoveEmptyPages = this.Factory.CreateRibbonButton();
            this.ButtonSelectShapesOnPage = this.Factory.CreateRibbonButton();
            this.ButtonFixAnchors = this.Factory.CreateRibbonButton();
            this.groupAlign = this.Factory.CreateRibbonGroup();
            this.buttonGroupAlignVertical = this.Factory.CreateRibbonBox();
            this.buttonAlignTop = this.Factory.CreateRibbonButton();
            this.buttonAlignMiddle = this.Factory.CreateRibbonButton();
            this.buttonAlignBottom = this.Factory.CreateRibbonButton();
            this.buttonGroupAlignHorizontal = this.Factory.CreateRibbonBox();
            this.buttonAlignLeft = this.Factory.CreateRibbonButton();
            this.buttonAlignCenter = this.Factory.CreateRibbonButton();
            this.buttonAlignRight = this.Factory.CreateRibbonButton();
            this.groupSize = this.Factory.CreateRibbonGroup();
            this.buttonGroup2 = this.Factory.CreateRibbonButtonGroup();
            this.buttonSizeToWidest = this.Factory.CreateRibbonButton();
            this.buttonSizeToNarrowest = this.Factory.CreateRibbonButton();
            this.buttonSizeToShortest = this.Factory.CreateRibbonButton();
            this.buttonSizeToTallest = this.Factory.CreateRibbonButton();
            this.box1 = this.Factory.CreateRibbonBox();
            this.editBoxSizeWidth = this.Factory.CreateRibbonEditBox();
            this.editBoxSizeHeight = this.Factory.CreateRibbonEditBox();
            this.box2 = this.Factory.CreateRibbonBox();
            this.buttonBestFit = this.Factory.CreateRibbonButton();
            this.dropDownPadding = this.Factory.CreateRibbonDropDown();
            this.groupArrange = this.Factory.CreateRibbonGroup();
            this.box4 = this.Factory.CreateRibbonBox();
            this.buttonArrangeLV = this.Factory.CreateRibbonButton();
            this.buttonArrangeRV = this.Factory.CreateRibbonButton();
            this.buttonArrangeSq = this.Factory.CreateRibbonButton();
            this.buttonArrangeRH = this.Factory.CreateRibbonButton();
            this.buttonArrangeH = this.Factory.CreateRibbonButton();
            this.box3 = this.Factory.CreateRibbonBox();
            this.toggleButton1 = this.Factory.CreateRibbonToggleButton();
            this.TabAddIns.SuspendLayout();
            this.groupPage.SuspendLayout();
            this.groupAlign.SuspendLayout();
            this.buttonGroupAlignVertical.SuspendLayout();
            this.buttonGroupAlignHorizontal.SuspendLayout();
            this.groupSize.SuspendLayout();
            this.buttonGroup2.SuspendLayout();
            this.box1.SuspendLayout();
            this.box2.SuspendLayout();
            this.groupArrange.SuspendLayout();
            this.box4.SuspendLayout();
            this.box3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabAddIns
            // 
            this.TabAddIns.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.TabAddIns.Groups.Add(this.groupPage);
            this.TabAddIns.Groups.Add(this.groupAlign);
            this.TabAddIns.Groups.Add(this.groupSize);
            this.TabAddIns.Groups.Add(this.groupArrange);
            this.TabAddIns.Label = "Photo Album";
            this.TabAddIns.Name = "TabAddIns";
            // 
            // groupPage
            // 
            this.groupPage.Items.Add(this.ButtonRemoveEmptyPages);
            this.groupPage.Items.Add(this.ButtonSelectShapesOnPage);
            this.groupPage.Items.Add(this.ButtonFixAnchors);
            this.groupPage.Label = "Page tools";
            this.groupPage.Name = "groupPage";
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
            // ButtonFixAnchors
            // 
            this.ButtonFixAnchors.Label = "Move anchors to top";
            this.ButtonFixAnchors.Name = "ButtonFixAnchors";
            this.ButtonFixAnchors.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonFixAnchors_Click);
            // 
            // groupAlign
            // 
            this.groupAlign.Items.Add(this.buttonGroupAlignVertical);
            this.groupAlign.Items.Add(this.buttonGroupAlignHorizontal);
            this.groupAlign.Label = "Alignment";
            this.groupAlign.Name = "groupAlign";
            // 
            // buttonGroupAlignVertical
            // 
            this.buttonGroupAlignVertical.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.buttonGroupAlignVertical.Items.Add(this.buttonAlignTop);
            this.buttonGroupAlignVertical.Items.Add(this.buttonAlignMiddle);
            this.buttonGroupAlignVertical.Items.Add(this.buttonAlignBottom);
            this.buttonGroupAlignVertical.Name = "buttonGroupAlignVertical";
            // 
            // buttonAlignTop
            // 
            this.buttonAlignTop.Label = "";
            this.buttonAlignTop.Name = "buttonAlignTop";
            this.buttonAlignTop.OfficeImageId = "ObjectsAlignTop";
            this.buttonAlignTop.ShowImage = true;
            this.buttonAlignTop.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAlignTop_Click);
            // 
            // buttonAlignMiddle
            // 
            this.buttonAlignMiddle.Label = "";
            this.buttonAlignMiddle.Name = "buttonAlignMiddle";
            this.buttonAlignMiddle.OfficeImageId = "ObjectsAlignMiddleVertical";
            this.buttonAlignMiddle.ShowImage = true;
            this.buttonAlignMiddle.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAlignMiddle_Click);
            // 
            // buttonAlignBottom
            // 
            this.buttonAlignBottom.Label = "";
            this.buttonAlignBottom.Name = "buttonAlignBottom";
            this.buttonAlignBottom.OfficeImageId = "ObjectsAlignBottom";
            this.buttonAlignBottom.ShowImage = true;
            this.buttonAlignBottom.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAlignBottom_Click);
            // 
            // buttonGroupAlignHorizontal
            // 
            this.buttonGroupAlignHorizontal.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.buttonGroupAlignHorizontal.Items.Add(this.buttonAlignLeft);
            this.buttonGroupAlignHorizontal.Items.Add(this.buttonAlignCenter);
            this.buttonGroupAlignHorizontal.Items.Add(this.buttonAlignRight);
            this.buttonGroupAlignHorizontal.Name = "buttonGroupAlignHorizontal";
            // 
            // buttonAlignLeft
            // 
            this.buttonAlignLeft.Label = "";
            this.buttonAlignLeft.Name = "buttonAlignLeft";
            this.buttonAlignLeft.OfficeImageId = "ObjectsAlignLeft";
            this.buttonAlignLeft.ShowImage = true;
            this.buttonAlignLeft.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAlignLeft_Click);
            // 
            // buttonAlignCenter
            // 
            this.buttonAlignCenter.Label = "";
            this.buttonAlignCenter.Name = "buttonAlignCenter";
            this.buttonAlignCenter.OfficeImageId = "ObjectsAlignCenterHorizontal";
            this.buttonAlignCenter.ShowImage = true;
            this.buttonAlignCenter.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAlignCenter_Click);
            // 
            // buttonAlignRight
            // 
            this.buttonAlignRight.Label = "";
            this.buttonAlignRight.Name = "buttonAlignRight";
            this.buttonAlignRight.OfficeImageId = "ObjectsAlignRight";
            this.buttonAlignRight.ShowImage = true;
            this.buttonAlignRight.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAlignRight_Click);
            // 
            // groupSize
            // 
            this.groupSize.Items.Add(this.buttonGroup2);
            this.groupSize.Items.Add(this.box1);
            this.groupSize.Items.Add(this.box2);
            this.groupSize.Label = "Size";
            this.groupSize.Name = "groupSize";
            // 
            // buttonGroup2
            // 
            this.buttonGroup2.Items.Add(this.buttonSizeToWidest);
            this.buttonGroup2.Items.Add(this.buttonSizeToNarrowest);
            this.buttonGroup2.Items.Add(this.buttonSizeToShortest);
            this.buttonGroup2.Items.Add(this.buttonSizeToTallest);
            this.buttonGroup2.Name = "buttonGroup2";
            // 
            // buttonSizeToWidest
            // 
            this.buttonSizeToWidest.Label = "";
            this.buttonSizeToWidest.Name = "buttonSizeToWidest";
            this.buttonSizeToWidest.OfficeImageId = "SizeToWidest";
            this.buttonSizeToWidest.ShowImage = true;
            this.buttonSizeToWidest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSizeToWidest_Click);
            // 
            // buttonSizeToNarrowest
            // 
            this.buttonSizeToNarrowest.Label = "";
            this.buttonSizeToNarrowest.Name = "buttonSizeToNarrowest";
            this.buttonSizeToNarrowest.OfficeImageId = "SizeToNarrowest";
            this.buttonSizeToNarrowest.ShowImage = true;
            this.buttonSizeToNarrowest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSizeToNarrowest_Click);
            // 
            // buttonSizeToShortest
            // 
            this.buttonSizeToShortest.Label = "";
            this.buttonSizeToShortest.Name = "buttonSizeToShortest";
            this.buttonSizeToShortest.OfficeImageId = "SizeToShortest";
            this.buttonSizeToShortest.ShowImage = true;
            this.buttonSizeToShortest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSizeToShortest_Click);
            // 
            // buttonSizeToTallest
            // 
            this.buttonSizeToTallest.Label = "";
            this.buttonSizeToTallest.Name = "buttonSizeToTallest";
            this.buttonSizeToTallest.OfficeImageId = "SizeToTallest";
            this.buttonSizeToTallest.ShowImage = true;
            this.buttonSizeToTallest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSizeToTallest_Click);
            // 
            // box1
            // 
            this.box1.Items.Add(this.editBoxSizeWidth);
            this.box1.Items.Add(this.editBoxSizeHeight);
            this.box1.Name = "box1";
            // 
            // editBoxSizeWidth
            // 
            this.editBoxSizeWidth.Label = "";
            this.editBoxSizeWidth.Name = "editBoxSizeWidth";
            this.editBoxSizeWidth.Text = null;
            this.editBoxSizeWidth.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.editBoxSizeWidth_TextChanged);
            // 
            // editBoxSizeHeight
            // 
            this.editBoxSizeHeight.Label = "x";
            this.editBoxSizeHeight.Name = "editBoxSizeHeight";
            this.editBoxSizeHeight.Text = null;
            this.editBoxSizeHeight.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.editBoxSizedHeight_TextChanged);
            // 
            // box2
            // 
            this.box2.Items.Add(this.buttonBestFit);
            this.box2.Items.Add(this.dropDownPadding);
            this.box2.Name = "box2";
            // 
            // buttonBestFit
            // 
            this.buttonBestFit.Label = "Best fit";
            this.buttonBestFit.Name = "buttonBestFit";
            this.buttonBestFit.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonBestFit_Click);
            // 
            // dropDownPadding
            // 
            ribbonDropDownItemImpl1.Label = "1";
            ribbonDropDownItemImpl2.Label = "2";
            ribbonDropDownItemImpl3.Label = "3";
            this.dropDownPadding.Items.Add(ribbonDropDownItemImpl1);
            this.dropDownPadding.Items.Add(ribbonDropDownItemImpl2);
            this.dropDownPadding.Items.Add(ribbonDropDownItemImpl3);
            this.dropDownPadding.Label = "Padding";
            this.dropDownPadding.Name = "dropDownPadding";
            // 
            // groupArrange
            // 
            this.groupArrange.DialogLauncher = ribbonDialogLauncherImpl1;
            this.groupArrange.Items.Add(this.box4);
            this.groupArrange.Items.Add(this.box3);
            this.groupArrange.Label = "Arrange";
            this.groupArrange.Name = "groupArrange";
            // 
            // box4
            // 
            this.box4.Items.Add(this.buttonArrangeLV);
            this.box4.Items.Add(this.buttonArrangeRV);
            this.box4.Items.Add(this.buttonArrangeSq);
            this.box4.Items.Add(this.buttonArrangeRH);
            this.box4.Items.Add(this.buttonArrangeH);
            this.box4.Name = "box4";
            // 
            // buttonArrangeLV
            // 
            this.buttonArrangeLV.Image = global::AlbumWordAddin.Properties.Resources.Pict_3x1;
            this.buttonArrangeLV.Label = "";
            this.buttonArrangeLV.Name = "buttonArrangeLV";
            this.buttonArrangeLV.ScreenTip = "Position selected images in a single column";
            this.buttonArrangeLV.ShowImage = true;
            this.buttonArrangeLV.ShowLabel = false;
            this.buttonArrangeLV.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonArrangeLV_Click);
            // 
            // buttonArrangeRV
            // 
            this.buttonArrangeRV.Image = global::AlbumWordAddin.Properties.Resources.Pict_3x2;
            this.buttonArrangeRV.Label = "";
            this.buttonArrangeRV.Name = "buttonArrangeRV";
            this.buttonArrangeRV.ScreenTip = "Position selected images a rectangular manner, fit to many lanscape picturess";
            this.buttonArrangeRV.ShowImage = true;
            this.buttonArrangeRV.ShowLabel = false;
            this.buttonArrangeRV.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonArrangeRV_Click);
            // 
            // buttonArrangeSq
            // 
            this.buttonArrangeSq.Image = global::AlbumWordAddin.Properties.Resources.Pict_2x2;
            this.buttonArrangeSq.Label = "";
            this.buttonArrangeSq.Name = "buttonArrangeSq";
            this.buttonArrangeSq.ScreenTip = "Position selected images a square manner.";
            this.buttonArrangeSq.ShowImage = true;
            this.buttonArrangeSq.ShowLabel = false;
            this.buttonArrangeSq.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonArrangeSq_Click);
            // 
            // buttonArrangeRH
            // 
            this.buttonArrangeRH.Image = global::AlbumWordAddin.Properties.Resources.Pict_2x3;
            this.buttonArrangeRH.Label = "";
            this.buttonArrangeRH.Name = "buttonArrangeRH";
            this.buttonArrangeRH.ScreenTip = "Position selected images a rectangular manner, fit to many portrait pictures.";
            this.buttonArrangeRH.ShowImage = true;
            this.buttonArrangeRH.ShowLabel = false;
            this.buttonArrangeRH.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonArrangeRH_Click);
            // 
            // buttonArrangeH
            // 
            this.buttonArrangeH.Image = global::AlbumWordAddin.Properties.Resources.Pict_1x3;
            this.buttonArrangeH.Label = "";
            this.buttonArrangeH.Name = "buttonArrangeH";
            this.buttonArrangeH.ScreenTip = "Position selected images in a single row";
            this.buttonArrangeH.ShowImage = true;
            this.buttonArrangeH.ShowLabel = false;
            this.buttonArrangeH.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonArrangeH_Click);
            // 
            // box3
            // 
            this.box3.Items.Add(this.toggleButton1);
            this.box3.Name = "box3";
            // 
            // toggleButton1
            // 
            this.toggleButton1.Label = "o";
            this.toggleButton1.Name = "toggleButton1";
            this.toggleButton1.ShowImage = true;
            // 
            // AlbumRibbon
            // 
            this.Name = "AlbumRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.TabAddIns);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.AlbumRibbon_Load);
            this.TabAddIns.ResumeLayout(false);
            this.TabAddIns.PerformLayout();
            this.groupPage.ResumeLayout(false);
            this.groupPage.PerformLayout();
            this.groupAlign.ResumeLayout(false);
            this.groupAlign.PerformLayout();
            this.buttonGroupAlignVertical.ResumeLayout(false);
            this.buttonGroupAlignVertical.PerformLayout();
            this.buttonGroupAlignHorizontal.ResumeLayout(false);
            this.buttonGroupAlignHorizontal.PerformLayout();
            this.groupSize.ResumeLayout(false);
            this.groupSize.PerformLayout();
            this.buttonGroup2.ResumeLayout(false);
            this.buttonGroup2.PerformLayout();
            this.box1.ResumeLayout(false);
            this.box1.PerformLayout();
            this.box2.ResumeLayout(false);
            this.box2.PerformLayout();
            this.groupArrange.ResumeLayout(false);
            this.groupArrange.PerformLayout();
            this.box4.ResumeLayout(false);
            this.box4.PerformLayout();
            this.box3.ResumeLayout(false);
            this.box3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab TabAddIns;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupPage;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ButtonRemoveEmptyPages;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ButtonSelectShapesOnPage;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ButtonFixAnchors;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupAlign;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupSize;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonBestFit;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dropDownPadding;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox editBoxSizeWidth;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox editBoxSizeHeight;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAlignBottom;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAlignMiddle;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAlignTop;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAlignLeft;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAlignCenter;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAlignRight;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox buttonGroupAlignVertical;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox buttonGroupAlignHorizontal;
        internal Microsoft.Office.Tools.Ribbon.RibbonButtonGroup buttonGroup2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSizeToTallest;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSizeToShortest;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSizeToNarrowest;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSizeToWidest;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupArrange;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box3;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButton1;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box4;
        private Microsoft.Office.Tools.Ribbon.RibbonButton buttonArrangeLV;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonArrangeRV;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonArrangeSq;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonArrangeRH;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonArrangeH;
    }

    partial class ThisRibbonCollection
    {
        internal AlbumRibbon AlbumRibbon
        {
            get { return this.GetRibbon<AlbumRibbon>(); }
        }
    }
}
