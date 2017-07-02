using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Tools.Ribbon;
using MoreLinq;

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
            IniDropDownItems(dropDownPadding, -10, 10, 0);
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
            Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl1 = this.Factory.CreateRibbonDialogLauncher();
            Microsoft.Office.Tools.Ribbon.RibbonToggleButton buttonArrangeV;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumRibbon));
            this.TabAddIns = this.Factory.CreateRibbonTab();
            this.groupFile = this.Factory.CreateRibbonGroup();
            this.groupPage = this.Factory.CreateRibbonGroup();
            this.groupArrange = this.Factory.CreateRibbonGroup();
            this.box4 = this.Factory.CreateRibbonBox();
            this.box2 = this.Factory.CreateRibbonBox();
            this.box6 = this.Factory.CreateRibbonBox();
            this.groupAlign = this.Factory.CreateRibbonGroup();
            this.buttonGroupAlignVertical = this.Factory.CreateRibbonBox();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.buttonGroupAlignHorizontal = this.Factory.CreateRibbonBox();
            this.box3 = this.Factory.CreateRibbonBox();
            this.dropDownMargin = this.Factory.CreateRibbonDropDown();
            this.box5 = this.Factory.CreateRibbonBox();
            this.dropDownPadding = this.Factory.CreateRibbonDropDown();
            this.buttonGroup2 = this.Factory.CreateRibbonButtonGroup();
            this.groupSpacing = this.Factory.CreateRibbonGroup();
            this.box9 = this.Factory.CreateRibbonBox();
            this.box7 = this.Factory.CreateRibbonBox();
            this.box8 = this.Factory.CreateRibbonBox();
            this.groupWrapping = this.Factory.CreateRibbonGroup();
            this.groupPrepare = this.Factory.CreateRibbonGroup();
            this.ButtonImport = this.Factory.CreateRibbonButton();
            this.buttonPictureSorter = this.Factory.CreateRibbonButton();
            this.ButtonSelectShapesOnPage = this.Factory.CreateRibbonButton();
            this.ButtonRemoveEmptyPages = this.Factory.CreateRibbonButton();
            this.ButtonFixAnchors = this.Factory.CreateRibbonButton();
            this.ButtonSetRelativePosition = this.Factory.CreateRibbonButton();
            this.buttonArrangeRV = this.Factory.CreateRibbonToggleButton();
            this.buttonArrangeSq = this.Factory.CreateRibbonToggleButton();
            this.buttonArrangeRH = this.Factory.CreateRibbonToggleButton();
            this.buttonArrangeH = this.Factory.CreateRibbonToggleButton();
            this.hAlignLeft = this.Factory.CreateRibbonToggleButton();
            this.hAlignBendLeft = this.Factory.CreateRibbonToggleButton();
            this.hAlignFlat = this.Factory.CreateRibbonToggleButton();
            this.hAlignBendRight = this.Factory.CreateRibbonToggleButton();
            this.hAlignRight = this.Factory.CreateRibbonToggleButton();
            this.hAlignRightUp = this.Factory.CreateRibbonToggleButton();
            this.hAlignRightDown = this.Factory.CreateRibbonToggleButton();
            this.vAlignTop = this.Factory.CreateRibbonToggleButton();
            this.vAlignBendDown = this.Factory.CreateRibbonToggleButton();
            this.vAlignFlat = this.Factory.CreateRibbonToggleButton();
            this.vAlignBendUp = this.Factory.CreateRibbonToggleButton();
            this.vAlignBottom = this.Factory.CreateRibbonToggleButton();
            this.vAlignRightUp = this.Factory.CreateRibbonToggleButton();
            this.vAlignRightDown = this.Factory.CreateRibbonToggleButton();
            this.buttonAlignTop = this.Factory.CreateRibbonButton();
            this.buttonAlignMiddle = this.Factory.CreateRibbonButton();
            this.buttonAlignBottom = this.Factory.CreateRibbonButton();
            this.buttonAlignLeft = this.Factory.CreateRibbonButton();
            this.buttonAlignCenter = this.Factory.CreateRibbonButton();
            this.buttonAlignRight = this.Factory.CreateRibbonButton();
            this.buttonMarginLess = this.Factory.CreateRibbonButton();
            this.buttonMarginMore = this.Factory.CreateRibbonButton();
            this.buttonPaddingLess = this.Factory.CreateRibbonButton();
            this.buttonPaddingMore = this.Factory.CreateRibbonButton();
            this.buttonSizeToWidest = this.Factory.CreateRibbonButton();
            this.buttonSizeToNarrowest = this.Factory.CreateRibbonButton();
            this.buttonSizeToShortest = this.Factory.CreateRibbonButton();
            this.buttonSizeToTallest = this.Factory.CreateRibbonButton();
            this.buttonSpacing = this.Factory.CreateRibbonButton();
            this.buttonSpacingDecreaseHorizontal = this.Factory.CreateRibbonButton();
            this.buttonSpacingIncreaseHorizontal = this.Factory.CreateRibbonButton();
            this.buttonSpacingEqualVertical = this.Factory.CreateRibbonButton();
            this.buttonSpacingDecreaseVertical = this.Factory.CreateRibbonButton();
            this.buttonSpacingIncreaseVertical = this.Factory.CreateRibbonButton();
            this.buttonSpacingInterpolate = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingSquare = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingBehindTextv = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingLeftOnly = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingInFrontOfText = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingTopAndBottom = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingBothSides = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingThrough = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingTight = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingRightOnly = this.Factory.CreateRibbonButton();
            this.ButtonLowRes = this.Factory.CreateRibbonButton();
            this.ButtonHiRes = this.Factory.CreateRibbonButton();
            buttonArrangeV = this.Factory.CreateRibbonToggleButton();
            this.TabAddIns.SuspendLayout();
            this.groupFile.SuspendLayout();
            this.groupPage.SuspendLayout();
            this.groupArrange.SuspendLayout();
            this.box4.SuspendLayout();
            this.box2.SuspendLayout();
            this.box6.SuspendLayout();
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
            this.groupFile.Label = "File";
            this.groupFile.Name = "groupFile";
            // 
            // groupPage
            // 
            this.groupPage.Items.Add(this.ButtonSelectShapesOnPage);
            this.groupPage.Items.Add(this.ButtonRemoveEmptyPages);
            this.groupPage.Items.Add(this.ButtonFixAnchors);
            this.groupPage.Items.Add(this.ButtonSetRelativePosition);
            this.groupPage.Label = "Page tools";
            this.groupPage.Name = "groupPage";
            // 
            // groupArrange
            // 
            this.groupArrange.DialogLauncher = ribbonDialogLauncherImpl1;
            this.groupArrange.Items.Add(this.box4);
            this.groupArrange.Items.Add(this.box2);
            this.groupArrange.Items.Add(this.box6);
            this.groupArrange.Label = "Arrange";
            this.groupArrange.Name = "groupArrange";
            // 
            // box4
            // 
            this.box4.Items.Add(buttonArrangeV);
            this.box4.Items.Add(this.buttonArrangeRV);
            this.box4.Items.Add(this.buttonArrangeSq);
            this.box4.Items.Add(this.buttonArrangeRH);
            this.box4.Items.Add(this.buttonArrangeH);
            this.box4.Name = "box4";
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
            // box3
            // 
            this.box3.Items.Add(this.buttonMarginLess);
            this.box3.Items.Add(this.dropDownMargin);
            this.box3.Items.Add(this.buttonMarginMore);
            this.box3.Name = "box3";
            // 
            // dropDownMargin
            // 
            this.dropDownMargin.Image = global::AlbumWordAddin.Properties.Resources.Margins;
            this.dropDownMargin.Label = "";
            this.dropDownMargin.Name = "dropDownMargin";
            this.dropDownMargin.ScreenTip = "Margins";
            this.dropDownMargin.ShowImage = true;
            this.dropDownMargin.ButtonClick += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.dropDownMargin_ButtonClick);
            this.dropDownMargin.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.dropDownMargin_SelectionChanged);
            // 
            // box5
            // 
            this.box5.Items.Add(this.buttonPaddingLess);
            this.box5.Items.Add(this.dropDownPadding);
            this.box5.Items.Add(this.buttonPaddingMore);
            this.box5.Name = "box5";
            // 
            // dropDownPadding
            // 
            this.dropDownPadding.Image = global::AlbumWordAddin.Properties.Resources.padding;
            this.dropDownPadding.Label = "";
            this.dropDownPadding.Name = "dropDownPadding";
            this.dropDownPadding.ScreenTip = "Padding";
            this.dropDownPadding.ShowImage = true;
            this.dropDownPadding.ButtonClick += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.dropDownPadding_ButtonClick);
            this.dropDownPadding.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.dropDownPadding_SelectionChanged);
            // 
            // buttonGroup2
            // 
            this.buttonGroup2.Items.Add(this.buttonSizeToWidest);
            this.buttonGroup2.Items.Add(this.buttonSizeToNarrowest);
            this.buttonGroup2.Items.Add(this.buttonSizeToShortest);
            this.buttonGroup2.Items.Add(this.buttonSizeToTallest);
            this.buttonGroup2.Name = "buttonGroup2";
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
            // box7
            // 
            this.box7.Items.Add(this.buttonSpacingEqualVertical);
            this.box7.Items.Add(this.buttonSpacingDecreaseVertical);
            this.box7.Items.Add(this.buttonSpacingIncreaseVertical);
            this.box7.Name = "box7";
            // 
            // box8
            // 
            this.box8.Items.Add(this.buttonSpacingInterpolate);
            this.box8.Name = "box8";
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
            // groupPrepare
            // 
            this.groupPrepare.Items.Add(this.ButtonLowRes);
            this.groupPrepare.Items.Add(this.ButtonHiRes);
            this.groupPrepare.Label = "Prepare document for print";
            this.groupPrepare.Name = "groupPrepare";
            // 
            // ButtonImport
            // 
            this.ButtonImport.Label = "Import pictures";
            this.ButtonImport.Name = "ButtonImport";
            this.ButtonImport.OfficeImageId = "PictureInsertFromFile";
            this.ButtonImport.ScreenTip = "Import pictures";
            this.ButtonImport.ShowImage = true;
            this.ButtonImport.ShowLabel = false;
            this.ButtonImport.SuperTip = "Import pictures and create one album document per folder";
            this.ButtonImport.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonImport_Click);
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
            this.buttonPictureSorter.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonPictureSorter_Click);
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
            this.ButtonFixAnchors.Tag = ShapeToolRequiredCount.One;
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
            this.ButtonSetRelativePosition.Tag = ShapeToolRequiredCount.One;
            this.ButtonSetRelativePosition.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSetRelativePosition_Click);
            // 
            // buttonArrangeV
            // 
            buttonArrangeV.Image = global::AlbumWordAddin.Properties.Resources.Pict_3x1;
            buttonArrangeV.Label = "";
            buttonArrangeV.Name = "buttonArrangeV";
            buttonArrangeV.ScreenTip = "Position the selected images in a single column";
            buttonArrangeV.ShowImage = true;
            buttonArrangeV.ShowLabel = false;
            buttonArrangeV.Tag = ShapeToolRequiredCount.One;
            buttonArrangeV.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonArrangeV_Click);
            // 
            // buttonArrangeRV
            // 
            this.buttonArrangeRV.Image = global::AlbumWordAddin.Properties.Resources.Pict_3x2;
            this.buttonArrangeRV.Label = "";
            this.buttonArrangeRV.Name = "buttonArrangeRV";
            this.buttonArrangeRV.ScreenTip = "Position the selected images a rectangular manner, fit to many lanscape picturess" +
    "";
            this.buttonArrangeRV.ShowImage = true;
            this.buttonArrangeRV.ShowLabel = false;
            this.buttonArrangeRV.Tag = ShapeToolRequiredCount.Two;
            this.buttonArrangeRV.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonArrangeRV_Click);
            // 
            // buttonArrangeSq
            // 
            this.buttonArrangeSq.Image = global::AlbumWordAddin.Properties.Resources.Pict_2x2;
            this.buttonArrangeSq.Label = "";
            this.buttonArrangeSq.Name = "buttonArrangeSq";
            this.buttonArrangeSq.ScreenTip = "Position the selected images a square manner.";
            this.buttonArrangeSq.ShowImage = true;
            this.buttonArrangeSq.ShowLabel = false;
            this.buttonArrangeSq.Tag = ShapeToolRequiredCount.Two;
            this.buttonArrangeSq.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonArrangeSq_Click);
            // 
            // buttonArrangeRH
            // 
            this.buttonArrangeRH.Image = global::AlbumWordAddin.Properties.Resources.Pict_2x3;
            this.buttonArrangeRH.Label = "";
            this.buttonArrangeRH.Name = "buttonArrangeRH";
            this.buttonArrangeRH.ScreenTip = "Position the selected images a rectangular manner, fit to many portrait pictures." +
    "";
            this.buttonArrangeRH.ShowImage = true;
            this.buttonArrangeRH.ShowLabel = false;
            this.buttonArrangeRH.Tag = ShapeToolRequiredCount.Two;
            this.buttonArrangeRH.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonArrangeRH_Click);
            // 
            // buttonArrangeH
            // 
            this.buttonArrangeH.Image = global::AlbumWordAddin.Properties.Resources.Pict_1x3;
            this.buttonArrangeH.Label = "";
            this.buttonArrangeH.Name = "buttonArrangeH";
            this.buttonArrangeH.ScreenTip = "Position the selected images in a single row";
            this.buttonArrangeH.ShowImage = true;
            this.buttonArrangeH.ShowLabel = false;
            this.buttonArrangeH.Tag = ShapeToolRequiredCount.One;
            this.buttonArrangeH.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonArrangeH_Click);
            // 
            // hAlignLeft
            // 
            this.hAlignLeft.Enabled = false;
            this.hAlignLeft.Image = global::AlbumWordAddin.Properties.Resources.HAlignLeft;
            this.hAlignLeft.Label = "";
            this.hAlignLeft.Name = "hAlignLeft";
            this.hAlignLeft.ScreenTip = "Left align";
            this.hAlignLeft.ShowImage = true;
            this.hAlignLeft.SuperTip = "Align the images to the left of the column, when arranged by column";
            this.hAlignLeft.Tag =  ShapeToolRequiredCount.One;
            this.hAlignLeft.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignBendLeft
            // 
            this.hAlignBendLeft.Enabled = false;
            this.hAlignBendLeft.Image = global::AlbumWordAddin.Properties.Resources.HAlignBendLeft;
            this.hAlignBendLeft.Label = "";
            this.hAlignBendLeft.Name = "hAlignBendLeft";
            this.hAlignBendLeft.ScreenTip = "Indent middle images to the right";
            this.hAlignBendLeft.ShowImage = true;
            this.hAlignBendLeft.SuperTip = "Left align the top and bottom images, and indent the middle images to the right o" +
    "r the column, when arranged by column";
            this.hAlignBendLeft.Tag =  ShapeToolRequiredCount.Two;
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
            this.hAlignFlat.SuperTip = "Align the images to the center of each column, when arranged by column";
            this.hAlignFlat.Tag =  ShapeToolRequiredCount.One;
            this.hAlignFlat.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignBendRight
            // 
            this.hAlignBendRight.Enabled = false;
            this.hAlignBendRight.Image = global::AlbumWordAddin.Properties.Resources.HAlignBendRight;
            this.hAlignBendRight.Label = "";
            this.hAlignBendRight.Name = "hAlignBendRight";
            this.hAlignBendRight.ScreenTip = "Indent middle images to the left";
            this.hAlignBendRight.ShowImage = true;
            this.hAlignBendRight.SuperTip = "Right align the top and bottom images, and indent the middle images to the left o" +
    "r the column, when arranged by column";
            this.hAlignBendRight.Tag =  ShapeToolRequiredCount.Two;
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
            this.hAlignRight.SuperTip = "Align the images to the right of the column, when arranged by column";
            this.hAlignRight.Tag =  ShapeToolRequiredCount.One;
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
    ", when arranged by column";
            this.hAlignRightUp.Tag =  ShapeToolRequiredCount.Two;
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
    "r, when arranged by column";
            this.hAlignRightDown.Tag =  ShapeToolRequiredCount.Two;
            this.hAlignRightDown.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // vAlignTop
            // 
            this.vAlignTop.Enabled = false;
            this.vAlignTop.Image = global::AlbumWordAddin.Properties.Resources.VAlignTop;
            this.vAlignTop.Label = "";
            this.vAlignTop.Name = "vAlignTop";
            this.vAlignTop.ShowImage = true;
            this.vAlignTop.Tag =  ShapeToolRequiredCount.One;
            this.vAlignTop.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignBendDown
            // 
            this.vAlignBendDown.Enabled = false;
            this.vAlignBendDown.Image = global::AlbumWordAddin.Properties.Resources.VAlignBendDown;
            this.vAlignBendDown.Label = "";
            this.vAlignBendDown.Name = "vAlignBendDown";
            this.vAlignBendDown.ShowImage = true;
            this.vAlignBendDown.Tag =  ShapeToolRequiredCount.Two;
            this.vAlignBendDown.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignFlat
            // 
            this.vAlignFlat.Enabled = false;
            this.vAlignFlat.Image = global::AlbumWordAddin.Properties.Resources.VAlignFlat;
            this.vAlignFlat.Label = "";
            this.vAlignFlat.Name = "vAlignFlat";
            this.vAlignFlat.ShowImage = true;
            this.vAlignFlat.Tag = ShapeToolRequiredCount.One;
            this.vAlignFlat.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignBendUp
            // 
            this.vAlignBendUp.Enabled = false;
            this.vAlignBendUp.Image = global::AlbumWordAddin.Properties.Resources.VAlignBendUp;
            this.vAlignBendUp.Label = "";
            this.vAlignBendUp.Name = "vAlignBendUp";
            this.vAlignBendUp.ShowImage = true;
            this.vAlignBendUp.Tag = ShapeToolRequiredCount.Two;
            this.vAlignBendUp.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignBottom
            // 
            this.vAlignBottom.Enabled = false;
            this.vAlignBottom.Image = global::AlbumWordAddin.Properties.Resources.VAlignBottom;
            this.vAlignBottom.Label = "";
            this.vAlignBottom.Name = "vAlignBottom";
            this.vAlignBottom.ShowImage = true;
            this.vAlignBottom.Tag = ShapeToolRequiredCount.One;
            this.vAlignBottom.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignRightUp
            // 
            this.vAlignRightUp.Enabled = false;
            this.vAlignRightUp.Image = global::AlbumWordAddin.Properties.Resources.VAlignRightUp;
            this.vAlignRightUp.Label = "";
            this.vAlignRightUp.Name = "vAlignRightUp";
            this.vAlignRightUp.ShowImage = true;
            this.vAlignRightUp.Tag = ShapeToolRequiredCount.Two;
            this.vAlignRightUp.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignRightDown
            // 
            this.vAlignRightDown.Enabled = false;
            this.vAlignRightDown.Image = global::AlbumWordAddin.Properties.Resources.VAlignRightDown;
            this.vAlignRightDown.Label = "";
            this.vAlignRightDown.Name = "vAlignRightDown";
            this.vAlignRightDown.ShowImage = true;
            this.vAlignRightDown.Tag = ShapeToolRequiredCount.Two;
            this.vAlignRightDown.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // buttonAlignTop
            // 
            this.buttonAlignTop.Label = "";
            this.buttonAlignTop.Name = "buttonAlignTop";
            this.buttonAlignTop.OfficeImageId = "ObjectsAlignTop";
            this.buttonAlignTop.ShowImage = true;
            this.buttonAlignTop.Tag = ShapeToolRequiredCount.Two;
            this.buttonAlignTop.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAlignTop_Click);
            // 
            // buttonAlignMiddle
            // 
            this.buttonAlignMiddle.Label = "";
            this.buttonAlignMiddle.Name = "buttonAlignMiddle";
            this.buttonAlignMiddle.OfficeImageId = "ObjectsAlignMiddleVertical";
            this.buttonAlignMiddle.ShowImage = true;
            this.buttonAlignMiddle.Tag = ShapeToolRequiredCount.Two;
            this.buttonAlignMiddle.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAlignMiddle_Click);
            // 
            // buttonAlignBottom
            // 
            this.buttonAlignBottom.Label = "";
            this.buttonAlignBottom.Name = "buttonAlignBottom";
            this.buttonAlignBottom.OfficeImageId = "ObjectsAlignBottom";
            this.buttonAlignBottom.ShowImage = true;
            this.buttonAlignBottom.Tag = ShapeToolRequiredCount.Two;
            this.buttonAlignBottom.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAlignBottom_Click);
            // 
            // buttonAlignLeft
            // 
            this.buttonAlignLeft.Label = "";
            this.buttonAlignLeft.Name = "buttonAlignLeft";
            this.buttonAlignLeft.OfficeImageId = "ObjectsAlignLeft";
            this.buttonAlignLeft.ShowImage = true;
            this.buttonAlignLeft.Tag = ShapeToolRequiredCount.Two;
            this.buttonAlignLeft.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAlignLeft_Click);
            // 
            // buttonAlignCenter
            // 
            this.buttonAlignCenter.Label = "";
            this.buttonAlignCenter.Name = "buttonAlignCenter";
            this.buttonAlignCenter.OfficeImageId = "ObjectsAlignCenterHorizontal";
            this.buttonAlignCenter.ShowImage = true;
            this.buttonAlignCenter.Tag = ShapeToolRequiredCount.Two;
            this.buttonAlignCenter.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAlignCenter_Click);
            // 
            // buttonAlignRight
            // 
            this.buttonAlignRight.Label = "";
            this.buttonAlignRight.Name = "buttonAlignRight";
            this.buttonAlignRight.OfficeImageId = "ObjectsAlignRight";
            this.buttonAlignRight.ShowImage = true;
            this.buttonAlignRight.Tag = ShapeToolRequiredCount.Two;
            this.buttonAlignRight.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAlignRight_Click);
            // 
            // buttonMarginLess
            // 
            this.buttonMarginLess.Label = "<";
            this.buttonMarginLess.Name = "buttonMarginLess";
            this.buttonMarginLess.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonMarginLess_Click);
            // 
            // buttonMarginMore
            // 
            this.buttonMarginMore.Label = ">";
            this.buttonMarginMore.Name = "buttonMarginMore";
            this.buttonMarginMore.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonMarginMore_Click);
            // 
            // buttonPaddingLess
            // 
            this.buttonPaddingLess.Label = "<";
            this.buttonPaddingLess.Name = "buttonPaddingLess";
            this.buttonPaddingLess.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonPaddingLess_Click);
            // 
            // buttonPaddingMore
            // 
            this.buttonPaddingMore.Label = ">";
            this.buttonPaddingMore.Name = "buttonPaddingMore";
            this.buttonPaddingMore.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonPaddingMore_Click);
            // 
            // buttonSizeToWidest
            // 
            this.buttonSizeToWidest.Label = "";
            this.buttonSizeToWidest.Name = "buttonSizeToWidest";
            this.buttonSizeToWidest.OfficeImageId = "SizeToWidest";
            this.buttonSizeToWidest.ScreenTip = "Size images to Widest";
            this.buttonSizeToWidest.ShowImage = true;
            this.buttonSizeToWidest.Tag = ShapeToolRequiredCount.Two;
            this.buttonSizeToWidest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSizeToWidest_Click);
            // 
            // buttonSizeToNarrowest
            // 
            this.buttonSizeToNarrowest.Label = "";
            this.buttonSizeToNarrowest.Name = "buttonSizeToNarrowest";
            this.buttonSizeToNarrowest.OfficeImageId = "SizeToNarrowest";
            this.buttonSizeToNarrowest.ScreenTip = "Size images to most Narrow";
            this.buttonSizeToNarrowest.ShowImage = true;
            this.buttonSizeToNarrowest.Tag = ShapeToolRequiredCount.Two;
            this.buttonSizeToNarrowest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSizeToNarrowest_Click);
            // 
            // buttonSizeToShortest
            // 
            this.buttonSizeToShortest.Label = "";
            this.buttonSizeToShortest.Name = "buttonSizeToShortest";
            this.buttonSizeToShortest.OfficeImageId = "SizeToShortest";
            this.buttonSizeToShortest.ScreenTip = "Size images to Shortest";
            this.buttonSizeToShortest.ShowImage = true;
            this.buttonSizeToShortest.Tag = ShapeToolRequiredCount.Two;
            this.buttonSizeToShortest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSizeToShortest_Click);
            // 
            // buttonSizeToTallest
            // 
            this.buttonSizeToTallest.Label = "";
            this.buttonSizeToTallest.Name = "buttonSizeToTallest";
            this.buttonSizeToTallest.OfficeImageId = "SizeToTallest";
            this.buttonSizeToTallest.ScreenTip = "Size images to tallest";
            this.buttonSizeToTallest.ShowImage = true;
            this.buttonSizeToTallest.Tag = ShapeToolRequiredCount.Two;
            this.buttonSizeToTallest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSizeToTallest_Click);
            // 
            // buttonSpacing
            // 
            this.buttonSpacing.Image = global::AlbumWordAddin.Properties.Resources.SpacingEqualHorizontal;
            this.buttonSpacing.Label = "button1";
            this.buttonSpacing.Name = "buttonSpacing";
            this.buttonSpacing.ScreenTip = "Equal Horizontal Spacing";
            this.buttonSpacing.ShowImage = true;
            this.buttonSpacing.ShowLabel = false;
            this.buttonSpacing.Tag = ShapeToolRequiredCount.Two;
            this.buttonSpacing.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSpacingEqualHorizontal_Click);
            // 
            // buttonSpacingDecreaseHorizontal
            // 
            this.buttonSpacingDecreaseHorizontal.Image = global::AlbumWordAddin.Properties.Resources.SpacingDecreaseHorizontal;
            this.buttonSpacingDecreaseHorizontal.Label = "button1";
            this.buttonSpacingDecreaseHorizontal.Name = "buttonSpacingDecreaseHorizontal";
            this.buttonSpacingDecreaseHorizontal.ScreenTip = "Decrease Horizontal Spacing";
            this.buttonSpacingDecreaseHorizontal.ShowImage = true;
            this.buttonSpacingDecreaseHorizontal.ShowLabel = false;
            this.buttonSpacingDecreaseHorizontal.Tag = ShapeToolRequiredCount.Two;
            this.buttonSpacingDecreaseHorizontal.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSpacingDecreaseHorizontal_Click);
            // 
            // buttonSpacingIncreaseHorizontal
            // 
            this.buttonSpacingIncreaseHorizontal.Image = global::AlbumWordAddin.Properties.Resources.SpacingIncreaseHorizontal;
            this.buttonSpacingIncreaseHorizontal.Label = "button1";
            this.buttonSpacingIncreaseHorizontal.Name = "buttonSpacingIncreaseHorizontal";
            this.buttonSpacingIncreaseHorizontal.ScreenTip = "Increase Horizontal Spacing";
            this.buttonSpacingIncreaseHorizontal.ShowImage = true;
            this.buttonSpacingIncreaseHorizontal.ShowLabel = false;
            this.buttonSpacingIncreaseHorizontal.Tag = ShapeToolRequiredCount.Two;
            this.buttonSpacingIncreaseHorizontal.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSpacingIncreaseHorizontal_Click);
            // 
            // buttonSpacingEqualVertical
            // 
            this.buttonSpacingEqualVertical.Image = global::AlbumWordAddin.Properties.Resources.SpacingEqualVertical;
            this.buttonSpacingEqualVertical.Label = "button1";
            this.buttonSpacingEqualVertical.Name = "buttonSpacingEqualVertical";
            this.buttonSpacingEqualVertical.ScreenTip = "Equal Vertical Spacing";
            this.buttonSpacingEqualVertical.ShowImage = true;
            this.buttonSpacingEqualVertical.ShowLabel = false;
            this.buttonSpacingEqualVertical.Tag = ShapeToolRequiredCount.Two;
            this.buttonSpacingEqualVertical.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSpacingEqualVertical_Click);
            // 
            // buttonSpacingDecreaseVertical
            // 
            this.buttonSpacingDecreaseVertical.Image = global::AlbumWordAddin.Properties.Resources.SpacingDecreaseVertical;
            this.buttonSpacingDecreaseVertical.Label = "button1";
            this.buttonSpacingDecreaseVertical.Name = "buttonSpacingDecreaseVertical";
            this.buttonSpacingDecreaseVertical.ScreenTip = "Decrease Vertical Spacing";
            this.buttonSpacingDecreaseVertical.ShowImage = true;
            this.buttonSpacingDecreaseVertical.ShowLabel = false;
            this.buttonSpacingDecreaseVertical.Tag = ShapeToolRequiredCount.Two;
            this.buttonSpacingDecreaseVertical.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSpacingDecreaseVertical_Click);
            // 
            // buttonSpacingIncreaseVertical
            // 
            this.buttonSpacingIncreaseVertical.Image = global::AlbumWordAddin.Properties.Resources.SpacingIncreaseVertical;
            this.buttonSpacingIncreaseVertical.Label = "button1";
            this.buttonSpacingIncreaseVertical.Name = "buttonSpacingIncreaseVertical";
            this.buttonSpacingIncreaseVertical.ScreenTip = "Increase Vertical Spacing";
            this.buttonSpacingIncreaseVertical.ShowImage = true;
            this.buttonSpacingIncreaseVertical.ShowLabel = false;
            this.buttonSpacingIncreaseVertical.Tag = ShapeToolRequiredCount.Two;
            this.buttonSpacingIncreaseVertical.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSpacingIncreaseVertical_Click);
            // 
            // buttonSpacingInterpolate
            // 
            this.buttonSpacingInterpolate.Image = global::AlbumWordAddin.Properties.Resources.SpacingInterpolate;
            this.buttonSpacingInterpolate.Label = "button1";
            this.buttonSpacingInterpolate.Name = "buttonSpacingInterpolate";
            this.buttonSpacingInterpolate.ScreenTip = "Interpolate Spacing";
            this.buttonSpacingInterpolate.ShowImage = true;
            this.buttonSpacingInterpolate.ShowLabel = false;
            this.buttonSpacingInterpolate.Tag = ShapeToolRequiredCount.Three;
            this.buttonSpacingInterpolate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSpacingInterpolate_Click);
            // 
            // buttonTextWrappingSquare
            // 
            this.buttonTextWrappingSquare.Label = " ";
            this.buttonTextWrappingSquare.Name = "buttonTextWrappingSquare";
            this.buttonTextWrappingSquare.OfficeImageId = "TextWrappingSquare";
            this.buttonTextWrappingSquare.ScreenTip = "Text Wrapping Square";
            this.buttonTextWrappingSquare.ShowImage = true;
            this.buttonTextWrappingSquare.ShowLabel = false;
            this.buttonTextWrappingSquare.Tag = ShapeToolRequiredCount.One;
            this.buttonTextWrappingSquare.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonTextWrappingSquare_Click);
            // 
            // buttonTextWrappingBehindTextv
            // 
            this.buttonTextWrappingBehindTextv.Label = " ";
            this.buttonTextWrappingBehindTextv.Name = "buttonTextWrappingBehindTextv";
            this.buttonTextWrappingBehindTextv.OfficeImageId = "TextWrappingBehindText";
            this.buttonTextWrappingBehindTextv.ScreenTip = "Text Wrapping Behind Text";
            this.buttonTextWrappingBehindTextv.ShowImage = true;
            this.buttonTextWrappingBehindTextv.ShowLabel = false;
            this.buttonTextWrappingBehindTextv.Tag = ShapeToolRequiredCount.One;
            this.buttonTextWrappingBehindTextv.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonTextWrappingBehindTextv_Click);
            // 
            // buttonTextWrappingLeftOnly
            // 
            this.buttonTextWrappingLeftOnly.Image = global::AlbumWordAddin.Properties.Resources.TextWrappingLeftOnly;
            this.buttonTextWrappingLeftOnly.Label = " ";
            this.buttonTextWrappingLeftOnly.Name = "buttonTextWrappingLeftOnly";
            this.buttonTextWrappingLeftOnly.ScreenTip = "Text Wrapping Left Only";
            this.buttonTextWrappingLeftOnly.ShowImage = true;
            this.buttonTextWrappingLeftOnly.ShowLabel = false;
            this.buttonTextWrappingLeftOnly.Tag = ShapeToolRequiredCount.One;
            this.buttonTextWrappingLeftOnly.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonTextWrappingLeftOnly_Click);
            // 
            // buttonTextWrappingInFrontOfText
            // 
            this.buttonTextWrappingInFrontOfText.Label = " ";
            this.buttonTextWrappingInFrontOfText.Name = "buttonTextWrappingInFrontOfText";
            this.buttonTextWrappingInFrontOfText.OfficeImageId = "TextWrappingInFrontOfText";
            this.buttonTextWrappingInFrontOfText.ScreenTip = "Text Wrapping In Front Of Text";
            this.buttonTextWrappingInFrontOfText.ShowImage = true;
            this.buttonTextWrappingInFrontOfText.ShowLabel = false;
            this.buttonTextWrappingInFrontOfText.Tag = ShapeToolRequiredCount.One;
            this.buttonTextWrappingInFrontOfText.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonTextWrappingInFrontOfText_Click);
            // 
            // buttonTextWrappingTopAndBottom
            // 
            this.buttonTextWrappingTopAndBottom.Label = " ";
            this.buttonTextWrappingTopAndBottom.Name = "buttonTextWrappingTopAndBottom";
            this.buttonTextWrappingTopAndBottom.OfficeImageId = "TextWrappingTopAndBottom";
            this.buttonTextWrappingTopAndBottom.ScreenTip = "Text Wrapping Top And Bottom";
            this.buttonTextWrappingTopAndBottom.ShowImage = true;
            this.buttonTextWrappingTopAndBottom.ShowLabel = false;
            this.buttonTextWrappingTopAndBottom.Tag = ShapeToolRequiredCount.One;
            this.buttonTextWrappingTopAndBottom.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonTextWrappingTopAndBottom_Click);
            // 
            // buttonTextWrappingBothSides
            // 
            this.buttonTextWrappingBothSides.Image = global::AlbumWordAddin.Properties.Resources.TextWrappingBothSides;
            this.buttonTextWrappingBothSides.Label = " ";
            this.buttonTextWrappingBothSides.Name = "buttonTextWrappingBothSides";
            this.buttonTextWrappingBothSides.ScreenTip = "Text Wrapping Both Sides";
            this.buttonTextWrappingBothSides.ShowImage = true;
            this.buttonTextWrappingBothSides.ShowLabel = false;
            this.buttonTextWrappingBothSides.Tag = ShapeToolRequiredCount.One;
            this.buttonTextWrappingBothSides.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonTextWrappingBothSides_Click);
            // 
            // buttonTextWrappingThrough
            // 
            this.buttonTextWrappingThrough.Label = " ";
            this.buttonTextWrappingThrough.Name = "buttonTextWrappingThrough";
            this.buttonTextWrappingThrough.OfficeImageId = "TextWrappingThrough";
            this.buttonTextWrappingThrough.ScreenTip = "Text Wrapping Through";
            this.buttonTextWrappingThrough.ShowImage = true;
            this.buttonTextWrappingThrough.ShowLabel = false;
            this.buttonTextWrappingThrough.Tag = ShapeToolRequiredCount.One;
            this.buttonTextWrappingThrough.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonTextWrappingThrough_Click);
            // 
            // buttonTextWrappingTight
            // 
            this.buttonTextWrappingTight.Label = " ";
            this.buttonTextWrappingTight.Name = "buttonTextWrappingTight";
            this.buttonTextWrappingTight.OfficeImageId = "TextWrappingTight";
            this.buttonTextWrappingTight.ScreenTip = "Text Wrapping Tight";
            this.buttonTextWrappingTight.ShowImage = true;
            this.buttonTextWrappingTight.ShowLabel = false;
            this.buttonTextWrappingTight.Tag = ShapeToolRequiredCount.One;
            this.buttonTextWrappingTight.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonTextWrappingTight_Click);
            // 
            // buttonTextWrappingRightOnly
            // 
            this.buttonTextWrappingRightOnly.Image = global::AlbumWordAddin.Properties.Resources.TextWrappingRightOnly;
            this.buttonTextWrappingRightOnly.Label = " ";
            this.buttonTextWrappingRightOnly.Name = "buttonTextWrappingRightOnly";
            this.buttonTextWrappingRightOnly.ScreenTip = "Text Wrapping RIght Only";
            this.buttonTextWrappingRightOnly.ShowImage = true;
            this.buttonTextWrappingRightOnly.ShowLabel = false;
            this.buttonTextWrappingRightOnly.Tag = ShapeToolRequiredCount.One;
            this.buttonTextWrappingRightOnly.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonTextWrappingRightOnly_Click);
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
            // ButtonHiRes
            // 
            this.ButtonHiRes.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ButtonHiRes.Image = ((System.Drawing.Image)(resources.GetObject("ButtonHiRes.Image")));
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
            this.groupArrange.ResumeLayout(false);
            this.groupArrange.PerformLayout();
            this.box4.ResumeLayout(false);
            this.box4.PerformLayout();
            this.box2.ResumeLayout(false);
            this.box2.PerformLayout();
            this.box6.ResumeLayout(false);
            this.box6.PerformLayout();
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
        internal RibbonButton buttonPaddingLess;
        internal RibbonDropDown dropDownPadding;
        internal RibbonButton buttonPaddingMore;
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
    }

    partial class ThisRibbonCollection
    {
        internal AlbumRibbon AlbumRibbon
        {
            get { return this.GetRibbon<AlbumRibbon>(); }
        }
    }
}
