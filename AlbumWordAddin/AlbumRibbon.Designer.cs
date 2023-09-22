using Microsoft.Office.Tools.Ribbon;

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
            IniDropDownItems(dropDownMargin, 0, 10, 5);
            IniDropDownItems(dropDownSpacing, -100, 100, 0);
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
            Microsoft.Office.Tools.Ribbon.RibbonToggleButton buttonArrangeV;
            Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl1 = this.Factory.CreateRibbonDialogLauncher();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumRibbon));
            this.TabAddIns = this.Factory.CreateRibbonTab();
            this.groupFile = this.Factory.CreateRibbonGroup();
            this.buttonPictureSorter = this.Factory.CreateRibbonButton();
            this.ButtonImport = this.Factory.CreateRibbonButton();
            this.buttonShowShapeProperties = this.Factory.CreateRibbonButton();
            this.groupPage = this.Factory.CreateRibbonGroup();
            this.ButtonSelectShapesOnPage = this.Factory.CreateRibbonButton();
            this.ButtonRemoveEmptyPages = this.Factory.CreateRibbonButton();
            this.ButtonFixAnchors = this.Factory.CreateRibbonButton();
            this.ButtonSetRelativePosition = this.Factory.CreateRibbonButton();
            this.buttonGroup1 = this.Factory.CreateRibbonButtonGroup();
            this.buttonUndo = this.Factory.CreateRibbonButton();
            this.buttonRedo = this.Factory.CreateRibbonButton();
            this.groupArrange = this.Factory.CreateRibbonGroup();
            this.box4 = this.Factory.CreateRibbonBox();
            this.toggleFlowGrid = this.Factory.CreateRibbonToggleButton();
            this.buttonArrangeRV = this.Factory.CreateRibbonToggleButton();
            this.buttonArrangeSq = this.Factory.CreateRibbonToggleButton();
            this.buttonArrangeRH = this.Factory.CreateRibbonToggleButton();
            this.buttonArrangeH = this.Factory.CreateRibbonToggleButton();
            this.box2 = this.Factory.CreateRibbonBox();
            this.hAlignLeft = this.Factory.CreateRibbonToggleButton();
            this.hAlignBendLeft = this.Factory.CreateRibbonToggleButton();
            this.hAlignFlat = this.Factory.CreateRibbonToggleButton();
            this.hAlignBendRight = this.Factory.CreateRibbonToggleButton();
            this.hAlignRight = this.Factory.CreateRibbonToggleButton();
            this.hAlignRightUp = this.Factory.CreateRibbonToggleButton();
            this.hAlignRightDown = this.Factory.CreateRibbonToggleButton();
            this.box6 = this.Factory.CreateRibbonBox();
            this.vAlignTop = this.Factory.CreateRibbonToggleButton();
            this.vAlignBendDown = this.Factory.CreateRibbonToggleButton();
            this.vAlignFlat = this.Factory.CreateRibbonToggleButton();
            this.vAlignBendUp = this.Factory.CreateRibbonToggleButton();
            this.vAlignBottom = this.Factory.CreateRibbonToggleButton();
            this.vAlignRightUp = this.Factory.CreateRibbonToggleButton();
            this.vAlignRightDown = this.Factory.CreateRibbonToggleButton();
            this.ButtonGroupRotateAndSwap = this.Factory.CreateRibbonBox();
            this.buttonSwapPositions = this.Factory.CreateRibbonButton();
            this.ButtonRotatePositionsClockwise = this.Factory.CreateRibbonButton();
            this.ButtonRotatePositionsAnticlockwise = this.Factory.CreateRibbonButton();
            this.groupAlign = this.Factory.CreateRibbonGroup();
            this.buttonGroupAlignVertical = this.Factory.CreateRibbonBox();
            this.buttonAlignTop = this.Factory.CreateRibbonButton();
            this.buttonAlignMiddle = this.Factory.CreateRibbonButton();
            this.buttonAlignBottom = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.buttonGroupAlignHorizontal = this.Factory.CreateRibbonBox();
            this.buttonAlignLeft = this.Factory.CreateRibbonButton();
            this.buttonAlignCenter = this.Factory.CreateRibbonButton();
            this.buttonAlignRight = this.Factory.CreateRibbonButton();
            this.box3 = this.Factory.CreateRibbonBox();
            this.buttonMarginLess = this.Factory.CreateRibbonButton();
            this.dropDownMargin = this.Factory.CreateRibbonDropDown();
            this.buttonMarginMore = this.Factory.CreateRibbonButton();
            this.box5 = this.Factory.CreateRibbonBox();
            this.buttonSpacingLess = this.Factory.CreateRibbonButton();
            this.dropDownSpacing = this.Factory.CreateRibbonDropDown();
            this.buttonSpacingMore = this.Factory.CreateRibbonButton();
            this.buttonGroup2 = this.Factory.CreateRibbonButtonGroup();
            this.buttonSizeToWidest = this.Factory.CreateRibbonButton();
            this.buttonSizeToNarrowest = this.Factory.CreateRibbonButton();
            this.buttonSizeToShortest = this.Factory.CreateRibbonButton();
            this.buttonSizeToTallest = this.Factory.CreateRibbonButton();
            this.groupSpacing = this.Factory.CreateRibbonGroup();
            this.box9 = this.Factory.CreateRibbonBox();
            this.buttonSpacing = this.Factory.CreateRibbonButton();
            this.buttonSpacingDecreaseHorizontal = this.Factory.CreateRibbonButton();
            this.buttonSpacingIncreaseHorizontal = this.Factory.CreateRibbonButton();
            this.box7 = this.Factory.CreateRibbonBox();
            this.buttonSpacingEqualVertical = this.Factory.CreateRibbonButton();
            this.buttonSpacingDecreaseVertical = this.Factory.CreateRibbonButton();
            this.buttonSpacingIncreaseVertical = this.Factory.CreateRibbonButton();
            this.box8 = this.Factory.CreateRibbonBox();
            this.buttonSpacingInterpolate = this.Factory.CreateRibbonButton();
            this.buttonSpacingDecreaseBoth = this.Factory.CreateRibbonButton();
            this.buttonSpacingIncreaseBoth = this.Factory.CreateRibbonButton();
            this.groupWrapping = this.Factory.CreateRibbonGroup();
            this.buttonTextWrappingSquare = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingBehindTextv = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingLeftOnly = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingInFrontOfText = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingTopAndBottom = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingBothSides = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingThrough = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingTight = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingRightOnly = this.Factory.CreateRibbonButton();
            this.groupPrepare = this.Factory.CreateRibbonGroup();
            this.ButtonLowRes = this.Factory.CreateRibbonButton();
            this.ButtonRightRes = this.Factory.CreateRibbonButton();
            this.ButtonHiRes = this.Factory.CreateRibbonButton();
            buttonArrangeV = this.Factory.CreateRibbonToggleButton();
            this.TabAddIns.SuspendLayout();
            this.groupFile.SuspendLayout();
            this.groupPage.SuspendLayout();
            this.buttonGroup1.SuspendLayout();
            this.groupArrange.SuspendLayout();
            this.box4.SuspendLayout();
            this.box2.SuspendLayout();
            this.box6.SuspendLayout();
            this.ButtonGroupRotateAndSwap.SuspendLayout();
            this.groupAlign.SuspendLayout();
            this.buttonGroupAlignVertical.SuspendLayout();
            this.buttonGroupAlignHorizontal.SuspendLayout();
            this.box3.SuspendLayout();
            this.box5.SuspendLayout();
            this.buttonGroup2.SuspendLayout();
            this.groupSpacing.SuspendLayout();
            this.box9.SuspendLayout();
            this.box7.SuspendLayout();
            this.box8.SuspendLayout();
            this.groupWrapping.SuspendLayout();
            this.groupPrepare.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonArrangeV
            // 
            buttonArrangeV.Image = global::AlbumWordAddin.Properties.Resources.Pict_3x1;
            buttonArrangeV.Label = "";
            buttonArrangeV.Name = "buttonArrangeV";
            buttonArrangeV.ScreenTip = "Arrange Vertically";
            buttonArrangeV.ShowImage = true;
            buttonArrangeV.ShowLabel = false;
            buttonArrangeV.SuperTip = "Position the selected images in a single column";
            buttonArrangeV.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            buttonArrangeV.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonArrangeV_Click);
            // 
            // TabAddIns
            // 
            this.TabAddIns.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.TabAddIns.Groups.Add(this.groupFile);
            this.TabAddIns.Groups.Add(this.groupPage);
            this.TabAddIns.Groups.Add(this.groupArrange);
            this.TabAddIns.Groups.Add(this.groupAlign);
            this.TabAddIns.Groups.Add(this.groupSpacing);
            this.TabAddIns.Groups.Add(this.groupWrapping);
            this.TabAddIns.Groups.Add(this.groupPrepare);
            this.TabAddIns.Label = "Photo Album";
            this.TabAddIns.Name = "TabAddIns";
            // 
            // groupFile
            // 
            this.groupFile.Items.Add(this.buttonPictureSorter);
            this.groupFile.Items.Add(this.ButtonImport);
            this.groupFile.Items.Add(this.buttonShowShapeProperties);
            this.groupFile.Label = "File";
            this.groupFile.Name = "groupFile";
            // 
            // buttonPictureSorter
            // 
            this.buttonPictureSorter.Image = global::AlbumWordAddin.Properties.Resources.PicturesSorterIcon;
            this.buttonPictureSorter.Label = "Organize Photos";
            this.buttonPictureSorter.Name = "buttonPictureSorter";
            this.buttonPictureSorter.ScreenTip = "Organize Photos";
            this.buttonPictureSorter.ShowImage = true;
            this.buttonPictureSorter.ShowLabel = false;
            this.buttonPictureSorter.SuperTip = "Compare and Choose photos";
            this.buttonPictureSorter.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonPictureSorter_Click);
            // 
            // ButtonImport
            // 
            this.ButtonImport.Image = global::AlbumWordAddin.Properties.Resources.green_cloud_import;
            this.ButtonImport.Label = "Import pictures";
            this.ButtonImport.Name = "ButtonImport";
            this.ButtonImport.OfficeImageId = "PictureInsertFromFile";
            this.ButtonImport.ScreenTip = "Import pictures";
            this.ButtonImport.ShowImage = true;
            this.ButtonImport.ShowLabel = false;
            this.ButtonImport.SuperTip = "Import pictures and create one album document per folder";
            this.ButtonImport.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonImport_Click);
            // 
            // buttonShowShapeProperties
            // 
            this.buttonShowShapeProperties.Label = "buttonShowShapeProperties";
            this.buttonShowShapeProperties.Name = "buttonShowShapeProperties";
            this.buttonShowShapeProperties.OfficeImageId = "Insights";
            this.buttonShowShapeProperties.ScreenTip = "Show selected images properties";
            this.buttonShowShapeProperties.ShowImage = true;
            this.buttonShowShapeProperties.ShowLabel = false;
            this.buttonShowShapeProperties.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ShowShapeProperties);
            // 
            // groupPage
            // 
            this.groupPage.Items.Add(this.ButtonSelectShapesOnPage);
            this.groupPage.Items.Add(this.ButtonRemoveEmptyPages);
            this.groupPage.Items.Add(this.ButtonFixAnchors);
            this.groupPage.Items.Add(this.ButtonSetRelativePosition);
            this.groupPage.Items.Add(this.buttonGroup1);
            this.groupPage.Label = "Page tools";
            this.groupPage.Name = "groupPage";
            // 
            // ButtonSelectShapesOnPage
            // 
            this.ButtonSelectShapesOnPage.Image = global::AlbumWordAddin.Properties.Resources.SelectAllImagesOnPage;
            this.ButtonSelectShapesOnPage.Label = "";
            this.ButtonSelectShapesOnPage.Name = "ButtonSelectShapesOnPage";
            this.ButtonSelectShapesOnPage.ScreenTip = "Select All Images on Page";
            this.ButtonSelectShapesOnPage.ShowImage = true;
            this.ButtonSelectShapesOnPage.SuperTip = "Select All Images on Page";
            this.ButtonSelectShapesOnPage.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSelectShapesOnPage_Click);
            // 
            // ButtonRemoveEmptyPages
            // 
            this.ButtonRemoveEmptyPages.Image = global::AlbumWordAddin.Properties.Resources.RemoveEmptyPages;
            this.ButtonRemoveEmptyPages.Label = "";
            this.ButtonRemoveEmptyPages.Name = "ButtonRemoveEmptyPages";
            this.ButtonRemoveEmptyPages.ScreenTip = "Remove empty pages";
            this.ButtonRemoveEmptyPages.ShowImage = true;
            this.ButtonRemoveEmptyPages.SuperTip = "Remove empty pages";
            this.ButtonRemoveEmptyPages.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonRemoveEmptyPages_Click);
            // 
            // ButtonFixAnchors
            // 
            this.ButtonFixAnchors.Image = global::AlbumWordAddin.Properties.Resources.MoveAnchorsToTop;
            this.ButtonFixAnchors.Label = "";
            this.ButtonFixAnchors.Name = "ButtonFixAnchors";
            this.ButtonFixAnchors.ScreenTip = "Move anchors to top";
            this.ButtonFixAnchors.ShowImage = true;
            this.ButtonFixAnchors.SuperTip = "Move anchors to top";
            this.ButtonFixAnchors.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.ButtonFixAnchors.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonFixAnchors_Click);
            // 
            // ButtonSetRelativePosition
            // 
            this.ButtonSetRelativePosition.Image = global::AlbumWordAddin.Properties.Resources.PositionRelativeToPage;
            this.ButtonSetRelativePosition.Label = "";
            this.ButtonSetRelativePosition.Name = "ButtonSetRelativePosition";
            this.ButtonSetRelativePosition.ScreenTip = "Set position relative to page";
            this.ButtonSetRelativePosition.ShowImage = true;
            this.ButtonSetRelativePosition.SuperTip = "Set position relative to page";
            this.ButtonSetRelativePosition.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.ButtonSetRelativePosition.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSetRelativePosition_Click);
            // 
            // buttonGroup1
            // 
            this.buttonGroup1.Items.Add(this.buttonUndo);
            this.buttonGroup1.Items.Add(this.buttonRedo);
            this.buttonGroup1.Name = "buttonGroup1";
            // 
            // buttonUndo
            // 
            this.buttonUndo.Image = global::AlbumWordAddin.Properties.Resources.Undo;
            this.buttonUndo.Label = "button1";
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.ShowImage = true;
            this.buttonUndo.ShowLabel = false;
            this.buttonUndo.SuperTip = "Undo complex operation, such as Arrange pictures, or Adjust Pictures Alignment, a" +
    "t once.";
            this.buttonUndo.Tag = "Undo complex operation";
            this.buttonUndo.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonUndo_Click);
            // 
            // buttonRedo
            // 
            this.buttonRedo.Image = global::AlbumWordAddin.Properties.Resources.Redo;
            this.buttonRedo.Label = "button2";
            this.buttonRedo.Name = "buttonRedo";
            this.buttonRedo.ShowImage = true;
            this.buttonRedo.ShowLabel = false;
            this.buttonRedo.SuperTip = "Redo complex operation, such as Arrange pictures, or Adjust Pictures Alignment, a" +
    "t once.";
            this.buttonRedo.Tag = "Redo complex operation";
            this.buttonRedo.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonRedo_Click);
            // 
            // groupArrange
            // 
            this.groupArrange.DialogLauncher = ribbonDialogLauncherImpl1;
            this.groupArrange.Items.Add(this.box4);
            this.groupArrange.Items.Add(this.box2);
            this.groupArrange.Items.Add(this.box6);
            this.groupArrange.Items.Add(this.ButtonGroupRotateAndSwap);
            this.groupArrange.Label = "Arrange";
            this.groupArrange.Name = "groupArrange";
            // 
            // box4
            // 
            this.box4.Items.Add(this.toggleFlowGrid);
            this.box4.Items.Add(buttonArrangeV);
            this.box4.Items.Add(this.buttonArrangeRV);
            this.box4.Items.Add(this.buttonArrangeSq);
            this.box4.Items.Add(this.buttonArrangeRH);
            this.box4.Items.Add(this.buttonArrangeH);
            this.box4.Name = "box4";
            // 
            // toggleFlowGrid
            // 
            this.toggleFlowGrid.Image = global::AlbumWordAddin.Properties.Resources.ToggleFlowGridGrid;
            this.toggleFlowGrid.Label = "toggleButton1";
            this.toggleFlowGrid.Name = "toggleFlowGrid";
            this.toggleFlowGrid.ScreenTip = "Toggle flow or grid layout ";
            this.toggleFlowGrid.ShowImage = true;
            this.toggleFlowGrid.ShowLabel = false;
            this.toggleFlowGrid.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ToggleFlowGrid_Click);
            // 
            // buttonArrangeRV
            // 
            this.buttonArrangeRV.Image = global::AlbumWordAddin.Properties.Resources.Pict_3x2;
            this.buttonArrangeRV.Label = "";
            this.buttonArrangeRV.Name = "buttonArrangeRV";
            this.buttonArrangeRV.ScreenTip = "Arrange landscape";
            this.buttonArrangeRV.ShowImage = true;
            this.buttonArrangeRV.ShowLabel = false;
            this.buttonArrangeRV.SuperTip = "Position the selected images a rectangular manner, fit to many lanscape pictures";
            this.buttonArrangeRV.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonArrangeRV.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonArrangeRV_Click);
            // 
            // buttonArrangeSq
            // 
            this.buttonArrangeSq.Image = global::AlbumWordAddin.Properties.Resources.Pict_2x2;
            this.buttonArrangeSq.Label = "";
            this.buttonArrangeSq.Name = "buttonArrangeSq";
            this.buttonArrangeSq.ScreenTip = "Arrange Square";
            this.buttonArrangeSq.ShowImage = true;
            this.buttonArrangeSq.ShowLabel = false;
            this.buttonArrangeSq.SuperTip = "Position the selected images a square manner.";
            this.buttonArrangeSq.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonArrangeSq.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonArrangeSq_Click);
            // 
            // buttonArrangeRH
            // 
            this.buttonArrangeRH.Image = global::AlbumWordAddin.Properties.Resources.Pict_2x3;
            this.buttonArrangeRH.Label = "";
            this.buttonArrangeRH.Name = "buttonArrangeRH";
            this.buttonArrangeRH.ScreenTip = "Arrange Portrait";
            this.buttonArrangeRH.ShowImage = true;
            this.buttonArrangeRH.ShowLabel = false;
            this.buttonArrangeRH.SuperTip = "Position the selected images a rectangular manner, fit to many portrait pictures." +
    "";
            this.buttonArrangeRH.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonArrangeRH.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonArrangeRH_Click);
            // 
            // buttonArrangeH
            // 
            this.buttonArrangeH.Image = global::AlbumWordAddin.Properties.Resources.Pict_1x3;
            this.buttonArrangeH.Label = "";
            this.buttonArrangeH.Name = "buttonArrangeH";
            this.buttonArrangeH.ScreenTip = "Arrange Horizontally";
            this.buttonArrangeH.ShowImage = true;
            this.buttonArrangeH.ShowLabel = false;
            this.buttonArrangeH.SuperTip = "Position the selected images in a single row";
            this.buttonArrangeH.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonArrangeH.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonArrangeH_Click);
            // 
            // box2
            // 
            this.box2.Items.Add(this.hAlignLeft);
            this.box2.Items.Add(this.hAlignBendLeft);
            this.box2.Items.Add(this.hAlignFlat);
            this.box2.Items.Add(this.hAlignBendRight);
            this.box2.Items.Add(this.hAlignRight);
            this.box2.Items.Add(this.hAlignRightUp);
            this.box2.Items.Add(this.hAlignRightDown);
            this.box2.Name = "box2";
            // 
            // hAlignLeft
            // 
            this.hAlignLeft.Enabled = false;
            this.hAlignLeft.Image = global::AlbumWordAddin.Properties.Resources.HAlignLeft;
            this.hAlignLeft.Label = "";
            this.hAlignLeft.Name = "hAlignLeft";
            this.hAlignLeft.ScreenTip = "Left align";
            this.hAlignLeft.ShowImage = true;
            this.hAlignLeft.SuperTip = "Align the images to the left of the column, when arranged vertically";
            this.hAlignLeft.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.hAlignLeft.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignBendLeft
            // 
            this.hAlignBendLeft.Enabled = false;
            this.hAlignBendLeft.Image = global::AlbumWordAddin.Properties.Resources.HAlignBendLeft;
            this.hAlignBendLeft.Label = "";
            this.hAlignBendLeft.Name = "hAlignBendLeft";
            this.hAlignBendLeft.ScreenTip = "Bend column to the right";
            this.hAlignBendLeft.ShowImage = true;
            this.hAlignBendLeft.SuperTip = "Left align the top and bottom images, and indent the middle images to the right o" +
    "f the column, when arranged vertically";
            this.hAlignBendLeft.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.hAlignBendLeft.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignFlat
            // 
            this.hAlignFlat.Enabled = false;
            this.hAlignFlat.Image = global::AlbumWordAddin.Properties.Resources.HAlignFlat;
            this.hAlignFlat.Label = "";
            this.hAlignFlat.Name = "hAlignFlat";
            this.hAlignFlat.ScreenTip = "Center";
            this.hAlignFlat.ShowImage = true;
            this.hAlignFlat.SuperTip = "Align the images to the center of each column, when arranged vertically";
            this.hAlignFlat.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.hAlignFlat.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignBendRight
            // 
            this.hAlignBendRight.Enabled = false;
            this.hAlignBendRight.Image = global::AlbumWordAddin.Properties.Resources.HAlignBendRight;
            this.hAlignBendRight.Label = "";
            this.hAlignBendRight.Name = "hAlignBendRight";
            this.hAlignBendRight.ScreenTip = "Bend column to the left";
            this.hAlignBendRight.ShowImage = true;
            this.hAlignBendRight.SuperTip = "Right align the top and bottom images, and indent the middle images to the left o" +
    "f the column, when arranged vertically";
            this.hAlignBendRight.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.hAlignBendRight.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignRight
            // 
            this.hAlignRight.Enabled = false;
            this.hAlignRight.Image = global::AlbumWordAddin.Properties.Resources.HAlignRight;
            this.hAlignRight.Label = "";
            this.hAlignRight.Name = "hAlignRight";
            this.hAlignRight.ScreenTip = "Right align";
            this.hAlignRight.ShowImage = true;
            this.hAlignRight.SuperTip = "Align the images to the right of the column, when arranged vertically";
            this.hAlignRight.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.hAlignRight.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignRightUp
            // 
            this.hAlignRightUp.Enabled = false;
            this.hAlignRightUp.Image = global::AlbumWordAddin.Properties.Resources.HAlignRightUp;
            this.hAlignRightUp.Label = "";
            this.hAlignRightUp.Name = "hAlignRightUp";
            this.hAlignRightUp.ScreenTip = "Align Ascending";
            this.hAlignRightUp.ShowImage = true;
            this.hAlignRightUp.SuperTip = "Align the images from the left to the right of the column, in an ascending manner" +
    ", when arranged vertically";
            this.hAlignRightUp.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.hAlignRightUp.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignRightDown
            // 
            this.hAlignRightDown.Enabled = false;
            this.hAlignRightDown.Image = global::AlbumWordAddin.Properties.Resources.HAlignRightDown;
            this.hAlignRightDown.Label = "";
            this.hAlignRightDown.Name = "hAlignRightDown";
            this.hAlignRightDown.ScreenTip = "Align Descending";
            this.hAlignRightDown.ShowImage = true;
            this.hAlignRightDown.SuperTip = "Align the images from the left to the right of the column, in an descending manne" +
    "r, when arranged vertically";
            this.hAlignRightDown.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.hAlignRightDown.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // box6
            // 
            this.box6.Items.Add(this.vAlignTop);
            this.box6.Items.Add(this.vAlignBendDown);
            this.box6.Items.Add(this.vAlignFlat);
            this.box6.Items.Add(this.vAlignBendUp);
            this.box6.Items.Add(this.vAlignBottom);
            this.box6.Items.Add(this.vAlignRightUp);
            this.box6.Items.Add(this.vAlignRightDown);
            this.box6.Name = "box6";
            // 
            // vAlignTop
            // 
            this.vAlignTop.Enabled = false;
            this.vAlignTop.Image = global::AlbumWordAddin.Properties.Resources.VAlignTop;
            this.vAlignTop.Label = "";
            this.vAlignTop.Name = "vAlignTop";
            this.vAlignTop.ScreenTip = "Top Align";
            this.vAlignTop.ShowImage = true;
            this.vAlignTop.SuperTip = "Align the images to the top of the row, when arranged horizontally";
            this.vAlignTop.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.vAlignTop.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignBendDown
            // 
            this.vAlignBendDown.Enabled = false;
            this.vAlignBendDown.Image = global::AlbumWordAddin.Properties.Resources.VAlignBendDown;
            this.vAlignBendDown.Label = "";
            this.vAlignBendDown.Name = "vAlignBendDown";
            this.vAlignBendDown.ScreenTip = "Bend row to the bottom";
            this.vAlignBendDown.ShowImage = true;
            this.vAlignBendDown.SuperTip = "Top align the left and right images, and indent the middle images to the bottom o" +
    "f the row, when arranged horizontally";
            this.vAlignBendDown.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.vAlignBendDown.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignFlat
            // 
            this.vAlignFlat.Enabled = false;
            this.vAlignFlat.Image = global::AlbumWordAddin.Properties.Resources.VAlignFlat;
            this.vAlignFlat.Label = "";
            this.vAlignFlat.Name = "vAlignFlat";
            this.vAlignFlat.ScreenTip = "Middle";
            this.vAlignFlat.ShowImage = true;
            this.vAlignFlat.SuperTip = "Align the images to the middle of each row, when arranged horizontally";
            this.vAlignFlat.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.vAlignFlat.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignBendUp
            // 
            this.vAlignBendUp.Enabled = false;
            this.vAlignBendUp.Image = global::AlbumWordAddin.Properties.Resources.VAlignBendUp;
            this.vAlignBendUp.Label = "";
            this.vAlignBendUp.Name = "vAlignBendUp";
            this.vAlignBendUp.ScreenTip = "Bend row to the top";
            this.vAlignBendUp.ShowImage = true;
            this.vAlignBendUp.SuperTip = "Bottom align the left and right images, and indent the middle images to the top o" +
    "f the row, when arranged horizontally";
            this.vAlignBendUp.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.vAlignBendUp.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignBottom
            // 
            this.vAlignBottom.Enabled = false;
            this.vAlignBottom.Image = global::AlbumWordAddin.Properties.Resources.VAlignBottom;
            this.vAlignBottom.Label = "";
            this.vAlignBottom.Name = "vAlignBottom";
            this.vAlignBottom.ScreenTip = "Bottom align";
            this.vAlignBottom.ShowImage = true;
            this.vAlignBottom.SuperTip = "Align the images to the bottom of the row, when arranged horizontally";
            this.vAlignBottom.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.vAlignBottom.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignRightUp
            // 
            this.vAlignRightUp.Enabled = false;
            this.vAlignRightUp.Image = global::AlbumWordAddin.Properties.Resources.VAlignRightUp;
            this.vAlignRightUp.Label = "";
            this.vAlignRightUp.Name = "vAlignRightUp";
            this.vAlignRightUp.ScreenTip = "Align Ascending";
            this.vAlignRightUp.ShowImage = true;
            this.vAlignRightUp.SuperTip = "Align the images from the left to the right of the column, in an ascending manner" +
    ", when arranged horizontally";
            this.vAlignRightUp.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.vAlignRightUp.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignRightDown
            // 
            this.vAlignRightDown.Enabled = false;
            this.vAlignRightDown.Image = global::AlbumWordAddin.Properties.Resources.VAlignRightDown;
            this.vAlignRightDown.Label = "";
            this.vAlignRightDown.Name = "vAlignRightDown";
            this.vAlignRightDown.ScreenTip = "Align Descending";
            this.vAlignRightDown.ShowImage = true;
            this.vAlignRightDown.SuperTip = "Align the images from the left to the right of the column, in an descending manne" +
    "r, when arranged horizontally";
            this.vAlignRightDown.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.vAlignRightDown.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // ButtonGroupRotateAndSwap
            // 
            this.ButtonGroupRotateAndSwap.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.ButtonGroupRotateAndSwap.Items.Add(this.buttonSwapPositions);
            this.ButtonGroupRotateAndSwap.Items.Add(this.ButtonRotatePositionsClockwise);
            this.ButtonGroupRotateAndSwap.Items.Add(this.ButtonRotatePositionsAnticlockwise);
            this.ButtonGroupRotateAndSwap.Name = "ButtonGroupRotateAndSwap";
            // 
            // buttonSwapPositions
            // 
            this.buttonSwapPositions.Image = global::AlbumWordAddin.Properties.Resources.SwapPositions;
            this.buttonSwapPositions.Label = "button1";
            this.buttonSwapPositions.Name = "buttonSwapPositions";
            this.buttonSwapPositions.ScreenTip = "Swap images";
            this.buttonSwapPositions.ShowImage = true;
            this.buttonSwapPositions.ShowLabel = false;
            this.buttonSwapPositions.SuperTip = "Exchange the positions of two selected images";
            this.buttonSwapPositions.Tag = AlbumWordAddin.ShapeToolRequiredCount.Two;
            this.buttonSwapPositions.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSwapPositions_Click);
            // 
            // ButtonRotatePositionsClockwise
            // 
            this.ButtonRotatePositionsClockwise.Image = global::AlbumWordAddin.Properties.Resources.RotateClockwise;
            this.ButtonRotatePositionsClockwise.Label = "butto";
            this.ButtonRotatePositionsClockwise.Name = "ButtonRotatePositionsClockwise";
            this.ButtonRotatePositionsClockwise.ScreenTip = "Cascade swap right";
            this.ButtonRotatePositionsClockwise.ShowImage = true;
            this.ButtonRotatePositionsClockwise.ShowLabel = false;
            this.ButtonRotatePositionsClockwise.SuperTip = "Exchange the positions of the selected images, moving them clockwise";
            this.ButtonRotatePositionsClockwise.Tag = AlbumWordAddin.ShapeToolRequiredCount.ThreeOrMore;
            this.ButtonRotatePositionsClockwise.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonRotatePositionsClockwise_Click);
            // 
            // ButtonRotatePositionsAnticlockwise
            // 
            this.ButtonRotatePositionsAnticlockwise.Image = global::AlbumWordAddin.Properties.Resources.RotateAntiClockwise;
            this.ButtonRotatePositionsAnticlockwise.Label = "button1";
            this.ButtonRotatePositionsAnticlockwise.Name = "ButtonRotatePositionsAnticlockwise";
            this.ButtonRotatePositionsAnticlockwise.ScreenTip = "Cascade swap left";
            this.ButtonRotatePositionsAnticlockwise.ShowImage = true;
            this.ButtonRotatePositionsAnticlockwise.ShowLabel = false;
            this.ButtonRotatePositionsAnticlockwise.SuperTip = "Exchange the positions of the selected images, moving them anticlockwise";
            this.ButtonRotatePositionsAnticlockwise.Tag = AlbumWordAddin.ShapeToolRequiredCount.ThreeOrMore;
            this.ButtonRotatePositionsAnticlockwise.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonRotatePositionsAnticlockwise_Click);
            // 
            // groupAlign
            // 
            this.groupAlign.Items.Add(this.buttonGroupAlignVertical);
            this.groupAlign.Items.Add(this.separator1);
            this.groupAlign.Items.Add(this.buttonGroupAlignHorizontal);
            this.groupAlign.Items.Add(this.box3);
            this.groupAlign.Items.Add(this.box5);
            this.groupAlign.Items.Add(this.buttonGroup2);
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
            this.buttonAlignTop.ScreenTip = "Top align";
            this.buttonAlignTop.ShowImage = true;
            this.buttonAlignTop.SuperTip = "Align selected images to the top-most";
            this.buttonAlignTop.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonAlignTop.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonAlignTop_Click);
            // 
            // buttonAlignMiddle
            // 
            this.buttonAlignMiddle.Label = "";
            this.buttonAlignMiddle.Name = "buttonAlignMiddle";
            this.buttonAlignMiddle.OfficeImageId = "ObjectsAlignMiddleVertical";
            this.buttonAlignMiddle.ScreenTip = "Middle align";
            this.buttonAlignMiddle.ShowImage = true;
            this.buttonAlignMiddle.SuperTip = "Align selected images to the middle vertical position";
            this.buttonAlignMiddle.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonAlignMiddle.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonAlignMiddle_Click);
            // 
            // buttonAlignBottom
            // 
            this.buttonAlignBottom.Label = "";
            this.buttonAlignBottom.Name = "buttonAlignBottom";
            this.buttonAlignBottom.OfficeImageId = "ObjectsAlignBottom";
            this.buttonAlignBottom.ShowImage = true;
            this.buttonAlignBottom.SuperTip = "Align selected images to the bottom-most";
            this.buttonAlignBottom.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonAlignBottom.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonAlignBottom_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
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
            this.buttonAlignLeft.ScreenTip = "Left align";
            this.buttonAlignLeft.ShowImage = true;
            this.buttonAlignLeft.SuperTip = "Align selected images to the left-most";
            this.buttonAlignLeft.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonAlignLeft.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonAlignLeft_Click);
            // 
            // buttonAlignCenter
            // 
            this.buttonAlignCenter.Label = "";
            this.buttonAlignCenter.Name = "buttonAlignCenter";
            this.buttonAlignCenter.OfficeImageId = "ObjectsAlignCenterHorizontal";
            this.buttonAlignCenter.ShowImage = true;
            this.buttonAlignCenter.SuperTip = "Align selected images to the center horizontal position";
            this.buttonAlignCenter.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonAlignCenter.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonAlignCenter_Click);
            // 
            // buttonAlignRight
            // 
            this.buttonAlignRight.Label = "";
            this.buttonAlignRight.Name = "buttonAlignRight";
            this.buttonAlignRight.OfficeImageId = "ObjectsAlignRight";
            this.buttonAlignRight.ScreenTip = "Right align";
            this.buttonAlignRight.ShowImage = true;
            this.buttonAlignRight.SuperTip = "Align selected images to the right-most";
            this.buttonAlignRight.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonAlignRight.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonAlignRight_Click);
            // 
            // box3
            // 
            this.box3.Items.Add(this.buttonMarginLess);
            this.box3.Items.Add(this.dropDownMargin);
            this.box3.Items.Add(this.buttonMarginMore);
            this.box3.Name = "box3";
            // 
            // buttonMarginLess
            // 
            this.buttonMarginLess.Image = global::AlbumWordAddin.Properties.Resources.MarginsShrink;
            this.buttonMarginLess.Label = "<";
            this.buttonMarginLess.Name = "buttonMarginLess";
            this.buttonMarginLess.ScreenTip = "Shrink Page Margins";
            this.buttonMarginLess.ShowImage = true;
            this.buttonMarginLess.ShowLabel = false;
            this.buttonMarginLess.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonMarginLess.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonMarginLess_Click);
            // 
            // dropDownMargin
            // 
            this.dropDownMargin.Label = "";
            this.dropDownMargin.Name = "dropDownMargin";
            this.dropDownMargin.ScreenTip = "Margins";
            this.dropDownMargin.ShowItemImage = false;
            this.dropDownMargin.ShowLabel = false;
            this.dropDownMargin.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.dropDownMargin.ButtonClick += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DropDownMargin_ButtonClick);
            this.dropDownMargin.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DropDownMargin_SelectionChanged);
            // 
            // buttonMarginMore
            // 
            this.buttonMarginMore.Image = global::AlbumWordAddin.Properties.Resources.MarginsGrow;
            this.buttonMarginMore.Label = ">";
            this.buttonMarginMore.Name = "buttonMarginMore";
            this.buttonMarginMore.ScreenTip = "Grow Page Margins";
            this.buttonMarginMore.ShowImage = true;
            this.buttonMarginMore.ShowLabel = false;
            this.buttonMarginMore.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonMarginMore.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonMarginMore_Click);
            // 
            // box5
            // 
            this.box5.Items.Add(this.buttonSpacingLess);
            this.box5.Items.Add(this.dropDownSpacing);
            this.box5.Items.Add(this.buttonSpacingMore);
            this.box5.Name = "box5";
            // 
            // buttonSpacingLess
            // 
            this.buttonSpacingLess.Image = global::AlbumWordAddin.Properties.Resources.spacingShrink;
            this.buttonSpacingLess.Label = "<";
            this.buttonSpacingLess.Name = "buttonSpacingLess";
            this.buttonSpacingLess.ScreenTip = "Shrink Spacing between images";
            this.buttonSpacingLess.ShowImage = true;
            this.buttonSpacingLess.ShowLabel = false;
            this.buttonSpacingLess.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonSpacingLess.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSpacingLess_Click);
            // 
            // dropDownSpacing
            // 
            this.dropDownSpacing.Label = "";
            this.dropDownSpacing.Name = "dropDownSpacing";
            this.dropDownSpacing.ScreenTip = "Spacing";
            this.dropDownSpacing.ShowItemImage = false;
            this.dropDownSpacing.ShowLabel = false;
            this.dropDownSpacing.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.dropDownSpacing.ButtonClick += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DropDownSpacing_ButtonClick);
            this.dropDownSpacing.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DropDownSpacing_SelectionChanged);
            // 
            // buttonSpacingMore
            // 
            this.buttonSpacingMore.Image = global::AlbumWordAddin.Properties.Resources.spacingGrow;
            this.buttonSpacingMore.Label = ">";
            this.buttonSpacingMore.Name = "buttonSpacingMore";
            this.buttonSpacingMore.ScreenTip = "Grow Spacing between images";
            this.buttonSpacingMore.ShowImage = true;
            this.buttonSpacingMore.ShowLabel = false;
            this.buttonSpacingMore.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonSpacingMore.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSpacingMore_Click);
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
            this.buttonSizeToWidest.ScreenTip = "Size images to Widest";
            this.buttonSizeToWidest.ShowImage = true;
            this.buttonSizeToWidest.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonSizeToWidest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSizeToWidest_Click);
            // 
            // buttonSizeToNarrowest
            // 
            this.buttonSizeToNarrowest.Label = "";
            this.buttonSizeToNarrowest.Name = "buttonSizeToNarrowest";
            this.buttonSizeToNarrowest.OfficeImageId = "SizeToNarrowest";
            this.buttonSizeToNarrowest.ScreenTip = "Size images to most Narrow";
            this.buttonSizeToNarrowest.ShowImage = true;
            this.buttonSizeToNarrowest.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonSizeToNarrowest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSizeToNarrowest_Click);
            // 
            // buttonSizeToShortest
            // 
            this.buttonSizeToShortest.Label = "";
            this.buttonSizeToShortest.Name = "buttonSizeToShortest";
            this.buttonSizeToShortest.OfficeImageId = "SizeToShortest";
            this.buttonSizeToShortest.ScreenTip = "Size images to Shortest";
            this.buttonSizeToShortest.ShowImage = true;
            this.buttonSizeToShortest.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonSizeToShortest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSizeToShortest_Click);
            // 
            // buttonSizeToTallest
            // 
            this.buttonSizeToTallest.Label = "";
            this.buttonSizeToTallest.Name = "buttonSizeToTallest";
            this.buttonSizeToTallest.OfficeImageId = "SizeToTallest";
            this.buttonSizeToTallest.ScreenTip = "Size images to Tallest";
            this.buttonSizeToTallest.ShowImage = true;
            this.buttonSizeToTallest.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonSizeToTallest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSizeToTallest_Click);
            // 
            // groupSpacing
            // 
            this.groupSpacing.Items.Add(this.box9);
            this.groupSpacing.Items.Add(this.box7);
            this.groupSpacing.Items.Add(this.box8);
            this.groupSpacing.Label = "Spacing";
            this.groupSpacing.Name = "groupSpacing";
            // 
            // box9
            // 
            this.box9.Items.Add(this.buttonSpacing);
            this.box9.Items.Add(this.buttonSpacingDecreaseHorizontal);
            this.box9.Items.Add(this.buttonSpacingIncreaseHorizontal);
            this.box9.Name = "box9";
            // 
            // buttonSpacing
            // 
            this.buttonSpacing.Image = global::AlbumWordAddin.Properties.Resources.SpacingEqualHorizontal;
            this.buttonSpacing.Label = "button1";
            this.buttonSpacing.Name = "buttonSpacing";
            this.buttonSpacing.ScreenTip = "Equal Horizontal Spacing";
            this.buttonSpacing.ShowImage = true;
            this.buttonSpacing.ShowLabel = false;
            this.buttonSpacing.SuperTip = "Dispose the selected images with equal horizontal spacing. The firstly and lastly" +
    " selected images are left unmoved.";
            this.buttonSpacing.Tag = AlbumWordAddin.ShapeToolRequiredCount.ThreeOrMore;
            this.buttonSpacing.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSpacingEqualHorizontal_Click);
            // 
            // buttonSpacingDecreaseHorizontal
            // 
            this.buttonSpacingDecreaseHorizontal.Image = global::AlbumWordAddin.Properties.Resources.SpacingDecreaseHorizontal;
            this.buttonSpacingDecreaseHorizontal.Label = "button1";
            this.buttonSpacingDecreaseHorizontal.Name = "buttonSpacingDecreaseHorizontal";
            this.buttonSpacingDecreaseHorizontal.ScreenTip = "Decrease Horizontal Spacing";
            this.buttonSpacingDecreaseHorizontal.ShowImage = true;
            this.buttonSpacingDecreaseHorizontal.ShowLabel = false;
            this.buttonSpacingDecreaseHorizontal.SuperTip = "Decrease Horizontal Spacing, The firstly selected image is left unmoved.";
            this.buttonSpacingDecreaseHorizontal.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonSpacingDecreaseHorizontal.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSpacingDecreaseHorizontal_Click);
            // 
            // buttonSpacingIncreaseHorizontal
            // 
            this.buttonSpacingIncreaseHorizontal.Image = global::AlbumWordAddin.Properties.Resources.SpacingIncreaseHorizontal;
            this.buttonSpacingIncreaseHorizontal.Label = "button1";
            this.buttonSpacingIncreaseHorizontal.Name = "buttonSpacingIncreaseHorizontal";
            this.buttonSpacingIncreaseHorizontal.ScreenTip = "Increase Horizontal Spacing";
            this.buttonSpacingIncreaseHorizontal.ShowImage = true;
            this.buttonSpacingIncreaseHorizontal.ShowLabel = false;
            this.buttonSpacingIncreaseHorizontal.SuperTip = "Increase Horizontal Spacing. The firstly selected image is left unmoved.";
            this.buttonSpacingIncreaseHorizontal.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonSpacingIncreaseHorizontal.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSpacingIncreaseHorizontal_Click);
            // 
            // box7
            // 
            this.box7.Items.Add(this.buttonSpacingEqualVertical);
            this.box7.Items.Add(this.buttonSpacingDecreaseVertical);
            this.box7.Items.Add(this.buttonSpacingIncreaseVertical);
            this.box7.Name = "box7";
            // 
            // buttonSpacingEqualVertical
            // 
            this.buttonSpacingEqualVertical.Image = global::AlbumWordAddin.Properties.Resources.SpacingEqualVertical;
            this.buttonSpacingEqualVertical.Label = "button1";
            this.buttonSpacingEqualVertical.Name = "buttonSpacingEqualVertical";
            this.buttonSpacingEqualVertical.ScreenTip = "Equal Vertical Spacing";
            this.buttonSpacingEqualVertical.ShowImage = true;
            this.buttonSpacingEqualVertical.ShowLabel = false;
            this.buttonSpacingEqualVertical.SuperTip = "Dispose the selected images with equal vertical spacing. The firstly and lastly s" +
    "elected images are left unmoved.";
            this.buttonSpacingEqualVertical.Tag = AlbumWordAddin.ShapeToolRequiredCount.ThreeOrMore;
            this.buttonSpacingEqualVertical.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSpacingEqualVertical_Click);
            // 
            // buttonSpacingDecreaseVertical
            // 
            this.buttonSpacingDecreaseVertical.Image = global::AlbumWordAddin.Properties.Resources.SpacingDecreaseVertical;
            this.buttonSpacingDecreaseVertical.Label = "button1";
            this.buttonSpacingDecreaseVertical.Name = "buttonSpacingDecreaseVertical";
            this.buttonSpacingDecreaseVertical.ScreenTip = "Decrease Vertical Spacing";
            this.buttonSpacingDecreaseVertical.ShowImage = true;
            this.buttonSpacingDecreaseVertical.ShowLabel = false;
            this.buttonSpacingDecreaseVertical.SuperTip = "Decrease Vertical Spacing. The firstly selected image is left unmoved.";
            this.buttonSpacingDecreaseVertical.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonSpacingDecreaseVertical.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSpacingDecreaseVertical_Click);
            // 
            // buttonSpacingIncreaseVertical
            // 
            this.buttonSpacingIncreaseVertical.Image = global::AlbumWordAddin.Properties.Resources.SpacingIncreaseVertical;
            this.buttonSpacingIncreaseVertical.Label = "button1";
            this.buttonSpacingIncreaseVertical.Name = "buttonSpacingIncreaseVertical";
            this.buttonSpacingIncreaseVertical.ScreenTip = "Increase Vertical Spacing";
            this.buttonSpacingIncreaseVertical.ShowImage = true;
            this.buttonSpacingIncreaseVertical.ShowLabel = false;
            this.buttonSpacingIncreaseVertical.SuperTip = "Increase Vertical Spacing. The firstly selected image is left unmoved.";
            this.buttonSpacingIncreaseVertical.Tag = AlbumWordAddin.ShapeToolRequiredCount.TwoOrMore;
            this.buttonSpacingIncreaseVertical.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSpacingIncreaseVertical_Click);
            // 
            // box8
            // 
            this.box8.Items.Add(this.buttonSpacingInterpolate);
            this.box8.Items.Add(this.buttonSpacingDecreaseBoth);
            this.box8.Items.Add(this.buttonSpacingIncreaseBoth);
            this.box8.Name = "box8";
            // 
            // buttonSpacingInterpolate
            // 
            this.buttonSpacingInterpolate.Image = global::AlbumWordAddin.Properties.Resources.SpacingInterpolate;
            this.buttonSpacingInterpolate.Label = "button1";
            this.buttonSpacingInterpolate.Name = "buttonSpacingInterpolate";
            this.buttonSpacingInterpolate.ScreenTip = "Interpolate Spacing";
            this.buttonSpacingInterpolate.ShowImage = true;
            this.buttonSpacingInterpolate.ShowLabel = false;
            this.buttonSpacingInterpolate.SuperTip = "Dispose the selected images with equal horizontal and vertical spacing. The first" +
    "ly and lastly selected images are left unmoved.";
            this.buttonSpacingInterpolate.Tag = AlbumWordAddin.ShapeToolRequiredCount.ThreeOrMore;
            this.buttonSpacingInterpolate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSpacingInterpolate_Click);
            // 
            // buttonSpacingDecreaseBoth
            // 
            this.buttonSpacingDecreaseBoth.Image = global::AlbumWordAddin.Properties.Resources.SpacingDecreaseBoth;
            this.buttonSpacingDecreaseBoth.Label = "button1";
            this.buttonSpacingDecreaseBoth.Name = "buttonSpacingDecreaseBoth";
            this.buttonSpacingDecreaseBoth.ScreenTip = "Decrease Spacing";
            this.buttonSpacingDecreaseBoth.ShowImage = true;
            this.buttonSpacingDecreaseBoth.ShowLabel = false;
            this.buttonSpacingDecreaseBoth.SuperTip = "Decrease empty space between selected images";
            this.buttonSpacingDecreaseBoth.Tag = AlbumWordAddin.ShapeToolRequiredCount.ThreeOrMore;
            this.buttonSpacingDecreaseBoth.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSpacingDecreaseBoth_Click);
            // 
            // buttonSpacingIncreaseBoth
            // 
            this.buttonSpacingIncreaseBoth.Image = global::AlbumWordAddin.Properties.Resources.SpacingIncreaseBoth;
            this.buttonSpacingIncreaseBoth.Label = "button1";
            this.buttonSpacingIncreaseBoth.Name = "buttonSpacingIncreaseBoth";
            this.buttonSpacingIncreaseBoth.ScreenTip = "Decrease Spacing";
            this.buttonSpacingIncreaseBoth.ShowImage = true;
            this.buttonSpacingIncreaseBoth.ShowLabel = false;
            this.buttonSpacingIncreaseBoth.SuperTip = "Decrease empty space between selected images";
            this.buttonSpacingIncreaseBoth.Tag = AlbumWordAddin.ShapeToolRequiredCount.ThreeOrMore;
            this.buttonSpacingIncreaseBoth.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSpacingIncreaseBoth_Click);
            // 
            // groupWrapping
            // 
            this.groupWrapping.Items.Add(this.buttonTextWrappingSquare);
            this.groupWrapping.Items.Add(this.buttonTextWrappingBehindTextv);
            this.groupWrapping.Items.Add(this.buttonTextWrappingLeftOnly);
            this.groupWrapping.Items.Add(this.buttonTextWrappingInFrontOfText);
            this.groupWrapping.Items.Add(this.buttonTextWrappingTopAndBottom);
            this.groupWrapping.Items.Add(this.buttonTextWrappingBothSides);
            this.groupWrapping.Items.Add(this.buttonTextWrappingThrough);
            this.groupWrapping.Items.Add(this.buttonTextWrappingTight);
            this.groupWrapping.Items.Add(this.buttonTextWrappingRightOnly);
            this.groupWrapping.Label = "Text Wrapping";
            this.groupWrapping.Name = "groupWrapping";
            // 
            // buttonTextWrappingSquare
            // 
            this.buttonTextWrappingSquare.Label = " ";
            this.buttonTextWrappingSquare.Name = "buttonTextWrappingSquare";
            this.buttonTextWrappingSquare.OfficeImageId = "TextWrappingSquare";
            this.buttonTextWrappingSquare.ScreenTip = "Text Wrapping Square";
            this.buttonTextWrappingSquare.ShowImage = true;
            this.buttonTextWrappingSquare.ShowLabel = false;
            this.buttonTextWrappingSquare.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonTextWrappingSquare.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonTextWrappingSquare_Click);
            // 
            // buttonTextWrappingBehindTextv
            // 
            this.buttonTextWrappingBehindTextv.Label = " ";
            this.buttonTextWrappingBehindTextv.Name = "buttonTextWrappingBehindTextv";
            this.buttonTextWrappingBehindTextv.OfficeImageId = "TextWrappingBehindText";
            this.buttonTextWrappingBehindTextv.ScreenTip = "Text Wrapping Behind Text";
            this.buttonTextWrappingBehindTextv.ShowImage = true;
            this.buttonTextWrappingBehindTextv.ShowLabel = false;
            this.buttonTextWrappingBehindTextv.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonTextWrappingBehindTextv.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonTextWrappingBehindTextv_Click);
            // 
            // buttonTextWrappingLeftOnly
            // 
            this.buttonTextWrappingLeftOnly.Image = global::AlbumWordAddin.Properties.Resources.TextWrappingLeftOnly;
            this.buttonTextWrappingLeftOnly.Label = " ";
            this.buttonTextWrappingLeftOnly.Name = "buttonTextWrappingLeftOnly";
            this.buttonTextWrappingLeftOnly.ScreenTip = "Text Wrapping Left Only";
            this.buttonTextWrappingLeftOnly.ShowImage = true;
            this.buttonTextWrappingLeftOnly.ShowLabel = false;
            this.buttonTextWrappingLeftOnly.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonTextWrappingLeftOnly.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonTextWrappingLeftOnly_Click);
            // 
            // buttonTextWrappingInFrontOfText
            // 
            this.buttonTextWrappingInFrontOfText.Label = " ";
            this.buttonTextWrappingInFrontOfText.Name = "buttonTextWrappingInFrontOfText";
            this.buttonTextWrappingInFrontOfText.OfficeImageId = "TextWrappingInFrontOfText";
            this.buttonTextWrappingInFrontOfText.ScreenTip = "Text Wrapping In Front Of Text";
            this.buttonTextWrappingInFrontOfText.ShowImage = true;
            this.buttonTextWrappingInFrontOfText.ShowLabel = false;
            this.buttonTextWrappingInFrontOfText.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonTextWrappingInFrontOfText.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonTextWrappingInFrontOfText_Click);
            // 
            // buttonTextWrappingTopAndBottom
            // 
            this.buttonTextWrappingTopAndBottom.Label = " ";
            this.buttonTextWrappingTopAndBottom.Name = "buttonTextWrappingTopAndBottom";
            this.buttonTextWrappingTopAndBottom.OfficeImageId = "TextWrappingTopAndBottom";
            this.buttonTextWrappingTopAndBottom.ScreenTip = "Text Wrapping Top And Bottom";
            this.buttonTextWrappingTopAndBottom.ShowImage = true;
            this.buttonTextWrappingTopAndBottom.ShowLabel = false;
            this.buttonTextWrappingTopAndBottom.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonTextWrappingTopAndBottom.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonTextWrappingTopAndBottom_Click);
            // 
            // buttonTextWrappingBothSides
            // 
            this.buttonTextWrappingBothSides.Image = global::AlbumWordAddin.Properties.Resources.TextWrappingBothSides;
            this.buttonTextWrappingBothSides.Label = " ";
            this.buttonTextWrappingBothSides.Name = "buttonTextWrappingBothSides";
            this.buttonTextWrappingBothSides.ScreenTip = "Text Wrapping Both Sides";
            this.buttonTextWrappingBothSides.ShowImage = true;
            this.buttonTextWrappingBothSides.ShowLabel = false;
            this.buttonTextWrappingBothSides.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonTextWrappingBothSides.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonTextWrappingBothSides_Click);
            // 
            // buttonTextWrappingThrough
            // 
            this.buttonTextWrappingThrough.Label = " ";
            this.buttonTextWrappingThrough.Name = "buttonTextWrappingThrough";
            this.buttonTextWrappingThrough.OfficeImageId = "TextWrappingThrough";
            this.buttonTextWrappingThrough.ScreenTip = "Text Wrapping Through";
            this.buttonTextWrappingThrough.ShowImage = true;
            this.buttonTextWrappingThrough.ShowLabel = false;
            this.buttonTextWrappingThrough.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonTextWrappingThrough.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonTextWrappingThrough_Click);
            // 
            // buttonTextWrappingTight
            // 
            this.buttonTextWrappingTight.Label = " ";
            this.buttonTextWrappingTight.Name = "buttonTextWrappingTight";
            this.buttonTextWrappingTight.OfficeImageId = "TextWrappingTight";
            this.buttonTextWrappingTight.ScreenTip = "Text Wrapping Tight";
            this.buttonTextWrappingTight.ShowImage = true;
            this.buttonTextWrappingTight.ShowLabel = false;
            this.buttonTextWrappingTight.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonTextWrappingTight.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonTextWrappingTight_Click);
            // 
            // buttonTextWrappingRightOnly
            // 
            this.buttonTextWrappingRightOnly.Image = global::AlbumWordAddin.Properties.Resources.TextWrappingRightOnly;
            this.buttonTextWrappingRightOnly.Label = " ";
            this.buttonTextWrappingRightOnly.Name = "buttonTextWrappingRightOnly";
            this.buttonTextWrappingRightOnly.ScreenTip = "Text Wrapping RIght Only";
            this.buttonTextWrappingRightOnly.ShowImage = true;
            this.buttonTextWrappingRightOnly.ShowLabel = false;
            this.buttonTextWrappingRightOnly.Tag = AlbumWordAddin.ShapeToolRequiredCount.OneOrMore;
            this.buttonTextWrappingRightOnly.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonTextWrappingRightOnly_Click);
            // 
            // groupPrepare
            // 
            this.groupPrepare.Items.Add(this.ButtonLowRes);
            this.groupPrepare.Items.Add(this.ButtonRightRes);
            this.groupPrepare.Items.Add(this.ButtonHiRes);
            this.groupPrepare.Label = "Prepare document for print";
            this.groupPrepare.Name = "groupPrepare";
            // 
            // ButtonLowRes
            // 
            this.ButtonLowRes.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ButtonLowRes.Image = ((System.Drawing.Image)(resources.GetObject("ButtonLowRes.Image")));
            this.ButtonLowRes.Label = "Low Res Images";
            this.ButtonLowRes.Name = "ButtonLowRes";
            this.ButtonLowRes.ScreenTip = "Low Resolution Images";
            this.ButtonLowRes.ShowImage = true;
            this.ButtonLowRes.SuperTip = resources.GetString("ButtonLowRes.SuperTip");
            this.ButtonLowRes.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonLowRes_Click);
            // 
            // ButtonRightRes
            // 
            this.ButtonRightRes.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ButtonRightRes.Image = global::AlbumWordAddin.Properties.Resources.RightResPictures;
            this.ButtonRightRes.Label = "Right Res Images";
            this.ButtonRightRes.Name = "ButtonRightRes";
            this.ButtonRightRes.ScreenTip = "Right Resolution Images";
            this.ButtonRightRes.ShowImage = true;
            this.ButtonRightRes.SuperTip = resources.GetString("ButtonRightRes.SuperTip");
            this.ButtonRightRes.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonRightRes_Click);
            // 
            // ButtonHiRes
            // 
            this.ButtonHiRes.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ButtonHiRes.Image = global::AlbumWordAddin.Properties.Resources.HiResPictures;
            this.ButtonHiRes.Label = "Hi Res Images";
            this.ButtonHiRes.Name = "ButtonHiRes";
            this.ButtonHiRes.ScreenTip = "High Resolution Images";
            this.ButtonHiRes.ShowImage = true;
            this.ButtonHiRes.SuperTip = "Change all the pictures to higher resolution, to optimize the print quality when " +
    "the editing work is complete.";
            this.ButtonHiRes.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonHiRes_Click);
            // 
            // AlbumRibbon
            // 
            this.Name = "AlbumRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.TabAddIns);
            this.Close += new System.EventHandler(this.AlbumRibbon_Close);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.AlbumRibbon_Load);
            this.TabAddIns.ResumeLayout(false);
            this.TabAddIns.PerformLayout();
            this.groupFile.ResumeLayout(false);
            this.groupFile.PerformLayout();
            this.groupPage.ResumeLayout(false);
            this.groupPage.PerformLayout();
            this.buttonGroup1.ResumeLayout(false);
            this.buttonGroup1.PerformLayout();
            this.groupArrange.ResumeLayout(false);
            this.groupArrange.PerformLayout();
            this.box4.ResumeLayout(false);
            this.box4.PerformLayout();
            this.box2.ResumeLayout(false);
            this.box2.PerformLayout();
            this.box6.ResumeLayout(false);
            this.box6.PerformLayout();
            this.ButtonGroupRotateAndSwap.ResumeLayout(false);
            this.ButtonGroupRotateAndSwap.PerformLayout();
            this.groupAlign.ResumeLayout(false);
            this.groupAlign.PerformLayout();
            this.buttonGroupAlignVertical.ResumeLayout(false);
            this.buttonGroupAlignVertical.PerformLayout();
            this.buttonGroupAlignHorizontal.ResumeLayout(false);
            this.buttonGroupAlignHorizontal.PerformLayout();
            this.box3.ResumeLayout(false);
            this.box3.PerformLayout();
            this.box5.ResumeLayout(false);
            this.box5.PerformLayout();
            this.buttonGroup2.ResumeLayout(false);
            this.buttonGroup2.PerformLayout();
            this.groupSpacing.ResumeLayout(false);
            this.groupSpacing.PerformLayout();
            this.box9.ResumeLayout(false);
            this.box9.PerformLayout();
            this.box7.ResumeLayout(false);
            this.box7.PerformLayout();
            this.box8.ResumeLayout(false);
            this.box8.PerformLayout();
            this.groupWrapping.ResumeLayout(false);
            this.groupWrapping.PerformLayout();
            this.groupPrepare.ResumeLayout(false);
            this.groupPrepare.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab TabAddIns;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupPage;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupAlign;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAlignBottom;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAlignMiddle;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAlignTop;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAlignLeft;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAlignCenter;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAlignRight;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox buttonGroupAlignVertical;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox buttonGroupAlignHorizontal;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupArrange;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box4;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton buttonArrangeRV;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton buttonArrangeSq;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton buttonArrangeRH;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton buttonArrangeH;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton hAlignLeft;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton hAlignFlat;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton hAlignRight;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton hAlignRightDown;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton hAlignRightUp;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton hAlignBendRight;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton hAlignBendLeft;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton vAlignTop;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton vAlignFlat;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton vAlignBottom;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton vAlignRightDown;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton vAlignRightUp;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton vAlignBendDown;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton vAlignBendUp;
        internal RibbonGroup groupFile;
        internal RibbonButton ButtonImport;
        internal RibbonButton ButtonLowRes;
        internal RibbonButton ButtonHiRes;
        internal RibbonBox box2;
        internal RibbonBox box6;
        internal RibbonBox box3;
        internal RibbonButton buttonMarginLess;
        internal RibbonDropDown dropDownMargin;
        internal RibbonButton buttonMarginMore;
        internal RibbonBox box5;
        internal RibbonButton buttonSpacingLess;
        internal RibbonDropDown dropDownSpacing;
        internal RibbonButton buttonSpacingMore;
        internal RibbonButton buttonPictureSorter;
        internal RibbonButtonGroup buttonGroup2;
        internal RibbonButton buttonSizeToWidest;
        internal RibbonButton buttonSizeToNarrowest;
        internal RibbonButton buttonSizeToShortest;
        internal RibbonButton buttonSizeToTallest;
        internal RibbonButton ButtonSetRelativePosition;
        internal RibbonButton ButtonFixAnchors;
        internal RibbonButton ButtonRemoveEmptyPages;
        internal RibbonButton ButtonSelectShapesOnPage;
        internal RibbonSeparator separator1;
        internal RibbonGroup groupWrapping;
        internal RibbonButton buttonTextWrappingTight;
        internal RibbonButton buttonTextWrappingSquare;
        internal RibbonButton buttonTextWrappingBehindTextv;
        internal RibbonButton buttonTextWrappingLeftOnly;
        internal RibbonButton buttonTextWrappingInFrontOfText;
        internal RibbonButton buttonTextWrappingTopAndBottom;
        internal RibbonButton buttonTextWrappingBothSides;
        internal RibbonButton buttonTextWrappingThrough;
        internal RibbonButton buttonTextWrappingRightOnly;
        internal RibbonGroup groupSpacing;
        internal RibbonBox box9;
        internal RibbonButton buttonSpacing;
        internal RibbonButton buttonSpacingDecreaseHorizontal;
        internal RibbonButton buttonSpacingIncreaseHorizontal;
        internal RibbonBox box7;
        internal RibbonButton buttonSpacingEqualVertical;
        internal RibbonButton buttonSpacingDecreaseVertical;
        internal RibbonButton buttonSpacingIncreaseVertical;
        internal RibbonBox box8;
        internal RibbonButton buttonSpacingInterpolate;
        internal RibbonGroup groupPrepare;
        internal RibbonButton buttonSpacingDecreaseBoth;
        internal RibbonButton buttonSpacingIncreaseBoth;
        internal RibbonButtonGroup buttonGroup1;
        internal RibbonButton buttonUndo;
        internal RibbonButton buttonRedo;
        internal RibbonBox ButtonGroupRotateAndSwap;
        internal RibbonButton buttonSwapPositions;
        internal RibbonButton ButtonRotatePositionsClockwise;
        internal RibbonButton ButtonRotatePositionsAnticlockwise;
        internal RibbonButton buttonShowShapeProperties;
        internal RibbonButton ButtonRightRes;
        internal RibbonToggleButton toggleFlowGrid;
    }

    partial class ThisRibbonCollection
    {
        internal AlbumRibbon AlbumRibbon
        {
            get { return this.GetRibbon<AlbumRibbon>(); }
        }
    }
}
