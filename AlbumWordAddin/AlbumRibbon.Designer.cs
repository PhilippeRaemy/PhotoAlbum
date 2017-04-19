﻿using System.Collections.Generic;
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

        IEnumerable<RibbonDropDownItem> GenIntDropdownItems(int start, int count)
        {
             return Enumerable.Range(start, count)
                .Select(i =>
                {
                    var di = Factory.CreateRibbonDropDownItem();
                    di.Label = i.ToString();
                    di.Tag = i;
                    return di;
                });
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
            Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl1 = this.Factory.CreateRibbonDialogLauncher();
            this.TabAddIns = this.Factory.CreateRibbonTab();
            this.groupFile = this.Factory.CreateRibbonGroup();
            this.ButtonImport = this.Factory.CreateRibbonButton();
            this.buttonPictureSorter = this.Factory.CreateRibbonButton();
            this.ButtonLowRes = this.Factory.CreateRibbonButton();
            this.ButtonHiRes = this.Factory.CreateRibbonButton();
            this.groupPage = this.Factory.CreateRibbonGroup();
            this.ButtonSelectShapesOnPage = this.Factory.CreateRibbonButton();
            this.ButtonRemoveEmptyPages = this.Factory.CreateRibbonButton();
            this.ButtonFixAnchors = this.Factory.CreateRibbonButton();
            this.ButtonSetRelativePosition = this.Factory.CreateRibbonButton();
            this.groupAlign = this.Factory.CreateRibbonGroup();
            this.buttonGroupAlignVertical = this.Factory.CreateRibbonBox();
            this.buttonAlignTop = this.Factory.CreateRibbonButton();
            this.buttonAlignMiddle = this.Factory.CreateRibbonButton();
            this.buttonAlignBottom = this.Factory.CreateRibbonButton();
            this.buttonGroupAlignHorizontal = this.Factory.CreateRibbonBox();
            this.buttonAlignLeft = this.Factory.CreateRibbonButton();
            this.buttonAlignCenter = this.Factory.CreateRibbonButton();
            this.buttonAlignRight = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.box3 = this.Factory.CreateRibbonBox();
            this.buttonMarginLess = this.Factory.CreateRibbonButton();
            this.dropDownMargin = this.Factory.CreateRibbonDropDown();
            this.buttonMarginMore = this.Factory.CreateRibbonButton();
            this.box5 = this.Factory.CreateRibbonBox();
            this.buttonPaddingLess = this.Factory.CreateRibbonButton();
            this.dropDownPadding = this.Factory.CreateRibbonDropDown();
            this.buttonPaddingMore = this.Factory.CreateRibbonButton();
            this.buttonGroup2 = this.Factory.CreateRibbonButtonGroup();
            this.buttonSizeToWidest = this.Factory.CreateRibbonButton();
            this.buttonSizeToNarrowest = this.Factory.CreateRibbonButton();
            this.buttonSizeToShortest = this.Factory.CreateRibbonButton();
            this.buttonSizeToTallest = this.Factory.CreateRibbonButton();
            this.groupSize = this.Factory.CreateRibbonGroup();
            this.box1 = this.Factory.CreateRibbonBox();
            this.editBoxSizeWidth = this.Factory.CreateRibbonEditBox();
            this.editBoxSizeHeight = this.Factory.CreateRibbonEditBox();
            this.groupArrange = this.Factory.CreateRibbonGroup();
            this.box4 = this.Factory.CreateRibbonBox();
            this.buttonArrangeLV = this.Factory.CreateRibbonButton();
            this.buttonArrangeRV = this.Factory.CreateRibbonButton();
            this.buttonArrangeSq = this.Factory.CreateRibbonButton();
            this.buttonArrangeRH = this.Factory.CreateRibbonButton();
            this.buttonArrangeH = this.Factory.CreateRibbonButton();
            this.box2 = this.Factory.CreateRibbonBox();
            this.hAlignLeft = this.Factory.CreateRibbonButton();
            this.hAlignBendLeft = this.Factory.CreateRibbonButton();
            this.hAlignFlat = this.Factory.CreateRibbonButton();
            this.hAlignBendRight = this.Factory.CreateRibbonButton();
            this.hAlignRight = this.Factory.CreateRibbonButton();
            this.hAlignRightUp = this.Factory.CreateRibbonButton();
            this.hAlignRightDown = this.Factory.CreateRibbonButton();
            this.box6 = this.Factory.CreateRibbonBox();
            this.vAlignTop = this.Factory.CreateRibbonButton();
            this.vAlignBendDown = this.Factory.CreateRibbonButton();
            this.vAlignFlat = this.Factory.CreateRibbonButton();
            this.vAlignBendUp = this.Factory.CreateRibbonButton();
            this.vAlignBottom = this.Factory.CreateRibbonButton();
            this.vAlignRightUp = this.Factory.CreateRibbonButton();
            this.vAlignRightDown = this.Factory.CreateRibbonButton();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.buttonTextWrappingSquare = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingBehindTextv = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingLeftOnly = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingInFrontOfText = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingTopAndBottom = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingBothSides = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingThrough = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingTight = this.Factory.CreateRibbonButton();
            this.buttonTextWrappingRightOnly = this.Factory.CreateRibbonButton();
            this.groupSpacing = this.Factory.CreateRibbonGroup();
            this.box7 = this.Factory.CreateRibbonBox();
            this.box8 = this.Factory.CreateRibbonBox();
            this.buttonSpacingInterpolate = this.Factory.CreateRibbonButton();
            this.box9 = this.Factory.CreateRibbonBox();
            this.buttonSpacing = this.Factory.CreateRibbonButton();
            this.buttonSpacingDecreaseHorizontal = this.Factory.CreateRibbonButton();
            this.buttonSpacingIncreaseHorizontal = this.Factory.CreateRibbonButton();
            this.buttonSpacingEqualVertical = this.Factory.CreateRibbonButton();
            this.buttonSpacingDecreaseVertical = this.Factory.CreateRibbonButton();
            this.buttonSpacingIncreaseVertical = this.Factory.CreateRibbonButton();
            this.TabAddIns.SuspendLayout();
            this.groupFile.SuspendLayout();
            this.groupPage.SuspendLayout();
            this.groupAlign.SuspendLayout();
            this.buttonGroupAlignVertical.SuspendLayout();
            this.buttonGroupAlignHorizontal.SuspendLayout();
            this.box3.SuspendLayout();
            this.box5.SuspendLayout();
            this.buttonGroup2.SuspendLayout();
            this.groupSize.SuspendLayout();
            this.box1.SuspendLayout();
            this.groupArrange.SuspendLayout();
            this.box4.SuspendLayout();
            this.box2.SuspendLayout();
            this.box6.SuspendLayout();
            this.group1.SuspendLayout();
            this.groupSpacing.SuspendLayout();
            this.box7.SuspendLayout();
            this.box8.SuspendLayout();
            this.box9.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabAddIns
            // 
            this.TabAddIns.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.TabAddIns.Groups.Add(this.groupFile);
            this.TabAddIns.Groups.Add(this.groupPage);
            this.TabAddIns.Groups.Add(this.groupAlign);
            this.TabAddIns.Groups.Add(this.groupSpacing);
            this.TabAddIns.Groups.Add(this.groupSize);
            this.TabAddIns.Groups.Add(this.groupArrange);
            this.TabAddIns.Groups.Add(this.group1);
            this.TabAddIns.Label = "Photo Album";
            this.TabAddIns.Name = "TabAddIns";
            // 
            // groupFile
            // 
            this.groupFile.Items.Add(this.ButtonImport);
            this.groupFile.Items.Add(this.buttonPictureSorter);
            this.groupFile.Items.Add(this.ButtonLowRes);
            this.groupFile.Items.Add(this.ButtonHiRes);
            this.groupFile.Label = "File";
            this.groupFile.Name = "groupFile";
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
            // ButtonLowRes
            // 
            this.ButtonLowRes.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ButtonLowRes.Image = ((System.Drawing.Image)(resources.GetObject("ButtonLowRes.Image")));
            this.ButtonLowRes.Label = "Low Res Images";
            this.ButtonLowRes.Name = "ButtonLowRes";
            this.ButtonLowRes.ScreenTip = "Low Resolution Images";
            this.ButtonLowRes.ShowImage = true;
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
            this.ButtonHiRes.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonHiRes_Click);
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
            this.ButtonSetRelativePosition.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ButtonSetRelativePosition_Click);
            // 
            // groupAlign
            // 
            this.groupAlign.Items.Add(this.buttonGroupAlignVertical);
            this.groupAlign.Items.Add(this.buttonGroupAlignHorizontal);
            this.groupAlign.Items.Add(this.separator1);
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
            // separator1
            // 
            this.separator1.Name = "separator1";
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
            this.buttonMarginLess.Label = "<";
            this.buttonMarginLess.Name = "buttonMarginLess";
            this.buttonMarginLess.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonMarginLess_Click);
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
            // buttonMarginMore
            // 
            this.buttonMarginMore.Label = ">";
            this.buttonMarginMore.Name = "buttonMarginMore";
            this.buttonMarginMore.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonMarginMore_Click);
            // 
            // box5
            // 
            this.box5.Items.Add(this.buttonPaddingLess);
            this.box5.Items.Add(this.dropDownPadding);
            this.box5.Items.Add(this.buttonPaddingMore);
            this.box5.Name = "box5";
            // 
            // buttonPaddingLess
            // 
            this.buttonPaddingLess.Label = "<";
            this.buttonPaddingLess.Name = "buttonPaddingLess";
            this.buttonPaddingLess.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonPaddingLess_Click);
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
            // buttonPaddingMore
            // 
            this.buttonPaddingMore.Label = ">";
            this.buttonPaddingMore.Name = "buttonPaddingMore";
            this.buttonPaddingMore.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonPaddingMore_Click);
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
            this.buttonSizeToWidest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSizeToWidest_Click);
            // 
            // buttonSizeToNarrowest
            // 
            this.buttonSizeToNarrowest.Label = "";
            this.buttonSizeToNarrowest.Name = "buttonSizeToNarrowest";
            this.buttonSizeToNarrowest.OfficeImageId = "SizeToNarrowest";
            this.buttonSizeToNarrowest.ScreenTip = "Size images to most Narrow";
            this.buttonSizeToNarrowest.ShowImage = true;
            this.buttonSizeToNarrowest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSizeToNarrowest_Click);
            // 
            // buttonSizeToShortest
            // 
            this.buttonSizeToShortest.Label = "";
            this.buttonSizeToShortest.Name = "buttonSizeToShortest";
            this.buttonSizeToShortest.OfficeImageId = "SizeToShortest";
            this.buttonSizeToShortest.ScreenTip = "Size images to Shortest";
            this.buttonSizeToShortest.ShowImage = true;
            this.buttonSizeToShortest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSizeToShortest_Click);
            // 
            // buttonSizeToTallest
            // 
            this.buttonSizeToTallest.Label = "";
            this.buttonSizeToTallest.Name = "buttonSizeToTallest";
            this.buttonSizeToTallest.OfficeImageId = "SizeToTallest";
            this.buttonSizeToTallest.ScreenTip = "Size images to tallest";
            this.buttonSizeToTallest.ShowImage = true;
            this.buttonSizeToTallest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSizeToTallest_Click);
            // 
            // groupSize
            // 
            this.groupSize.Items.Add(this.box1);
            this.groupSize.Label = "Size";
            this.groupSize.Name = "groupSize";
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
            this.hAlignLeft.Image = global::AlbumWordAddin.Properties.Resources.HAlignLeft;
            this.hAlignLeft.Label = "";
            this.hAlignLeft.Name = "hAlignLeft";
            this.hAlignLeft.ShowImage = true;
            this.hAlignLeft.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignBendLeft
            // 
            this.hAlignBendLeft.Image = global::AlbumWordAddin.Properties.Resources.HAlignBendLeft;
            this.hAlignBendLeft.Label = "";
            this.hAlignBendLeft.Name = "hAlignBendLeft";
            this.hAlignBendLeft.ShowImage = true;
            this.hAlignBendLeft.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignFlat
            // 
            this.hAlignFlat.Image = global::AlbumWordAddin.Properties.Resources.HAlignFlat;
            this.hAlignFlat.Label = "";
            this.hAlignFlat.Name = "hAlignFlat";
            this.hAlignFlat.ShowImage = true;
            this.hAlignFlat.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignBendRight
            // 
            this.hAlignBendRight.Image = global::AlbumWordAddin.Properties.Resources.HAlignBendRight;
            this.hAlignBendRight.Label = "";
            this.hAlignBendRight.Name = "hAlignBendRight";
            this.hAlignBendRight.ShowImage = true;
            this.hAlignBendRight.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignRight
            // 
            this.hAlignRight.Image = global::AlbumWordAddin.Properties.Resources.HAlignRight;
            this.hAlignRight.Label = "";
            this.hAlignRight.Name = "hAlignRight";
            this.hAlignRight.ShowImage = true;
            this.hAlignRight.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignRightUp
            // 
            this.hAlignRightUp.Image = global::AlbumWordAddin.Properties.Resources.HAlignRightUp;
            this.hAlignRightUp.Label = "";
            this.hAlignRightUp.Name = "hAlignRightUp";
            this.hAlignRightUp.ShowImage = true;
            this.hAlignRightUp.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemHAlign_Click);
            // 
            // hAlignRightDown
            // 
            this.hAlignRightDown.Image = global::AlbumWordAddin.Properties.Resources.HAlignRightDown;
            this.hAlignRightDown.Label = "";
            this.hAlignRightDown.Name = "hAlignRightDown";
            this.hAlignRightDown.ShowImage = true;
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
            this.vAlignTop.Image = global::AlbumWordAddin.Properties.Resources.VAlignTop;
            this.vAlignTop.Label = "";
            this.vAlignTop.Name = "vAlignTop";
            this.vAlignTop.ShowImage = true;
            this.vAlignTop.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignBendDown
            // 
            this.vAlignBendDown.Image = global::AlbumWordAddin.Properties.Resources.VAlignBendDown;
            this.vAlignBendDown.Label = "";
            this.vAlignBendDown.Name = "vAlignBendDown";
            this.vAlignBendDown.ShowImage = true;
            this.vAlignBendDown.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignFlat
            // 
            this.vAlignFlat.Image = global::AlbumWordAddin.Properties.Resources.VAlignFlat;
            this.vAlignFlat.Label = "";
            this.vAlignFlat.Name = "vAlignFlat";
            this.vAlignFlat.ShowImage = true;
            this.vAlignFlat.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignBendUp
            // 
            this.vAlignBendUp.Image = global::AlbumWordAddin.Properties.Resources.VAlignBendUp;
            this.vAlignBendUp.Label = "";
            this.vAlignBendUp.Name = "vAlignBendUp";
            this.vAlignBendUp.ShowImage = true;
            this.vAlignBendUp.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignBottom
            // 
            this.vAlignBottom.Image = global::AlbumWordAddin.Properties.Resources.VAlignBottom;
            this.vAlignBottom.Label = "";
            this.vAlignBottom.Name = "vAlignBottom";
            this.vAlignBottom.ShowImage = true;
            this.vAlignBottom.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignRightUp
            // 
            this.vAlignRightUp.Image = global::AlbumWordAddin.Properties.Resources.VAlignRightUp;
            this.vAlignRightUp.Label = "";
            this.vAlignRightUp.Name = "vAlignRightUp";
            this.vAlignRightUp.ShowImage = true;
            this.vAlignRightUp.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // vAlignRightDown
            // 
            this.vAlignRightDown.Image = global::AlbumWordAddin.Properties.Resources.VAlignRightDown;
            this.vAlignRightDown.Label = "";
            this.vAlignRightDown.Name = "vAlignRightDown";
            this.vAlignRightDown.ShowImage = true;
            this.vAlignRightDown.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.MenuItemVAlign_Click);
            // 
            // group1
            // 
            this.group1.Items.Add(this.buttonTextWrappingSquare);
            this.group1.Items.Add(this.buttonTextWrappingBehindTextv);
            this.group1.Items.Add(this.buttonTextWrappingLeftOnly);
            this.group1.Items.Add(this.buttonTextWrappingInFrontOfText);
            this.group1.Items.Add(this.buttonTextWrappingTopAndBottom);
            this.group1.Items.Add(this.buttonTextWrappingBothSides);
            this.group1.Items.Add(this.buttonTextWrappingThrough);
            this.group1.Items.Add(this.buttonTextWrappingTight);
            this.group1.Items.Add(this.buttonTextWrappingRightOnly);
            this.group1.Label = "Wrapping";
            this.group1.Name = "group1";
            // 
            // buttonTextWrappingSquare
            // 
            this.buttonTextWrappingSquare.Label = " ";
            this.buttonTextWrappingSquare.Name = "buttonTextWrappingSquare";
            this.buttonTextWrappingSquare.OfficeImageId = "TextWrappingSquare";
            this.buttonTextWrappingSquare.ScreenTip = "Text Wrapping Square";
            this.buttonTextWrappingSquare.ShowImage = true;
            this.buttonTextWrappingSquare.ShowLabel = false;
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
            this.buttonTextWrappingRightOnly.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonTextWrappingRightOnly_Click);
            // 
            // groupSpacing
            // 
            this.groupSpacing.Items.Add(this.box9);
            this.groupSpacing.Items.Add(this.box7);
            this.groupSpacing.Items.Add(this.box8);
            this.groupSpacing.Label = "Spacing";
            this.groupSpacing.Name = "groupSpacing";
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
            // buttonSpacingInterpolate
            // 
            this.buttonSpacingInterpolate.Image = global::AlbumWordAddin.Properties.Resources.SpacingInterpolate;
            this.buttonSpacingInterpolate.Label = "button1";
            this.buttonSpacingInterpolate.Name = "buttonSpacingInterpolate";
            this.buttonSpacingInterpolate.ScreenTip = "Interpolate Spacing";
            this.buttonSpacingInterpolate.ShowImage = true;
            this.buttonSpacingInterpolate.ShowLabel = false;
            this.buttonSpacingInterpolate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSpacingInterpolate_Click);
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
            this.buttonSpacingIncreaseVertical.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSpacingIncreaseVertical_Click);
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
            this.groupSize.ResumeLayout(false);
            this.groupSize.PerformLayout();
            this.box1.ResumeLayout(false);
            this.box1.PerformLayout();
            this.groupArrange.ResumeLayout(false);
            this.groupArrange.PerformLayout();
            this.box4.ResumeLayout(false);
            this.box4.PerformLayout();
            this.box2.ResumeLayout(false);
            this.box2.PerformLayout();
            this.box6.ResumeLayout(false);
            this.box6.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.groupSpacing.ResumeLayout(false);
            this.groupSpacing.PerformLayout();
            this.box7.ResumeLayout(false);
            this.box7.PerformLayout();
            this.box8.ResumeLayout(false);
            this.box8.PerformLayout();
            this.box9.ResumeLayout(false);
            this.box9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab TabAddIns;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupPage;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupAlign;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupSize;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
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
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupArrange;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box4;
        private Microsoft.Office.Tools.Ribbon.RibbonButton buttonArrangeLV;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonArrangeRV;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonArrangeSq;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonArrangeRH;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonArrangeH;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton hAlignLeft;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton hAlignFlat;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton hAlignRight;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton hAlignRightDown;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton hAlignRightUp;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton hAlignBendRight;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton hAlignBendLeft;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton vAlignTop;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton vAlignFlat;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton vAlignBottom;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton vAlignRightDown;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton vAlignRightUp;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton vAlignBendDown;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton vAlignBendUp;
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
        internal RibbonGroup group1;
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
    }

    partial class ThisRibbonCollection
    {
        internal AlbumRibbon AlbumRibbon
        {
            get { return this.GetRibbon<AlbumRibbon>(); }
        }
    }
}
