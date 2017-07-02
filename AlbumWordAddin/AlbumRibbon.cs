using System;

namespace AlbumWordAddin
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using Microsoft.Office.Tools.Ribbon;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using Mannex.Text.RegularExpressions;
    using Microsoft.Office.Core;
    using Microsoft.Office.Interop.Word;
    using MoreLinq;
    using UserPreferences;

    public partial class AlbumRibbon
    {
        RibbonToggleButtonSet _arrangeButtonSet;
        RibbonToggleButtonSet _hAlignButtonSet;
        RibbonToggleButtonSet _vAlignButtonSet;
        RibbonControlSet _buttonsActingOnOneOrMoreShapes;
        RibbonControlSet _buttonsActingOnTwoOrMoreShapes;
        RibbonControlSet _buttonsActingOnThreeOrMoreShapes;

        void AlbumRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            PerformLayout();
            var userPrefs = new PersistedUserPreferences();
            DropDownIntSetter(dropDownMargin, userPrefs.Margin);
            DropDownIntSetter(dropDownPadding, userPrefs.Padding);

            _arrangeButtonSet = new RibbonToggleButtonSet(EnumerateControls<RibbonToggleButton>(ctrl => ctrl.Name.IsMatch("buttonArrange")));
            _hAlignButtonSet  = new RibbonToggleButtonSet(EnumerateControls<RibbonToggleButton>(ctrl => ctrl.Name.IsMatch("hAlign"       )));
            _vAlignButtonSet  = new RibbonToggleButtonSet(EnumerateControls<RibbonToggleButton>(ctrl => ctrl.Name.IsMatch("vAlign"       )));
            _buttonsActingOnOneOrMoreShapes   = new RibbonControlSet(EnumerateControls(FilterOnTag(ShapeToolRequiredCount.OneOrMore  )));
            _buttonsActingOnTwoOrMoreShapes   = new RibbonControlSet(EnumerateControls(FilterOnTag(ShapeToolRequiredCount.TwoOrMore  )));
            _buttonsActingOnThreeOrMoreShapes = new RibbonControlSet(EnumerateControls(FilterOnTag(ShapeToolRequiredCount.ThreeOrMore)));
        }

        static Func<RibbonControl, bool> FilterOnTag(ShapeToolRequiredCount shapeToolRequiredCount) 
            => ctrl => ctrl.Tag is ShapeToolRequiredCount 
                       && ((ShapeToolRequiredCount)ctrl.Tag & shapeToolRequiredCount) != ShapeToolRequiredCount.None;

        IEnumerable<T> EnumerateControls<T>(Func<T, bool> filterFunc) where T: RibbonControl
         => from  gr   in TabAddIns.Groups
            from  item in gr.Items
            where item is T && filterFunc((T)item)
            select (T)item;

        void AlbumRibbon_Close(object sender, EventArgs e)
        {
            new PersistedUserPreferences {
                Margin = (int) dropDownMargin.SelectedItem.Tag,
                Padding = (int) dropDownPadding.SelectedItem.Tag
            }.Save();
        }

        static void DropDownIntSetter(RibbonDropDown ribbonDropDown, int value)
        {
            ribbonDropDown.SelectedItem 
                =  ribbonDropDown.Items.FirstOrDefault(i => (int) i.Tag == value)
                ?? ribbonDropDown.SelectedItem;
        }

        void ButtonRemoveEmptyPages_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.RemoveEmptyPages();
        }

        void ButtonSelectShapesOnPage_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SelectShapesOnPage();
        }

        void ButtonFixAnchors_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.FixAnchorOfSelectedImages();
        }

        void buttonSizeToWidest_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Widest);
        }

        void buttonSizeToTallest_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Tallest);
        }

        void buttonAlignTop_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Top);
        }

        void buttonAlignMiddle_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Middle);
        }

        void buttonAlignBottom_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Bottom);
        }

        void buttonAlignLeft_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Left);
        }

        void buttonAlignCenter_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Center);
        }

        void buttonAlignRight_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Right);
        }

        void buttonSizeToNarrowest_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Narrowest);
        }

        void buttonSizeToShortest_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Shortest);
        }

        void buttonArrangeV_Click(object sender, RibbonControlEventArgs e)
        {
            _arrangeButtonSet.SelectedButton = (RibbonToggleButton)sender;
            _hAlignButtonSet.SetEnabled(RibbonControlEnablereasonEnum.Functional, true );
            _vAlignButtonSet.SetEnabled(RibbonControlEnablereasonEnum.Functional, false);
            Globals.ThisAddIn.ArrangeSelectedImages(Arrangement.LineVertical, Padding(), Margin());
        }

        void buttonArrangeRV_Click(object sender, RibbonControlEventArgs e)
        {
            _hAlignButtonSet.SetEnabled(RibbonControlEnablereasonEnum.Functional, true);
            _vAlignButtonSet.SetEnabled(RibbonControlEnablereasonEnum.Functional, true);
            _arrangeButtonSet.SelectedButton = (RibbonToggleButton)sender;
            Globals.ThisAddIn.ArrangeSelectedImages(Arrangement.RectangleVertical, Padding(), Margin());
        }

        void buttonArrangeSq_Click(object sender, RibbonControlEventArgs e)
        {
            _hAlignButtonSet.SetEnabled(RibbonControlEnablereasonEnum.Functional, true);
            _vAlignButtonSet.SetEnabled(RibbonControlEnablereasonEnum.Functional, true);
            _arrangeButtonSet.SelectedButton = (RibbonToggleButton)sender;
            Globals.ThisAddIn.ArrangeSelectedImages(Arrangement.Square, Padding(), Margin());
        }

        void buttonArrangeRH_Click(object sender, RibbonControlEventArgs e)
        {
            _hAlignButtonSet.SetEnabled(RibbonControlEnablereasonEnum.Functional, true);
            _vAlignButtonSet.SetEnabled(RibbonControlEnablereasonEnum.Functional, true);
            _arrangeButtonSet.SelectedButton = (RibbonToggleButton)sender;
            Globals.ThisAddIn.ArrangeSelectedImages(Arrangement.RectangleHorizontal, Padding(), Margin());
        }

        void buttonArrangeH_Click(object sender, RibbonControlEventArgs e)
        {
            _hAlignButtonSet.SetEnabled(RibbonControlEnablereasonEnum.Functional, false);
            _vAlignButtonSet.SetEnabled(RibbonControlEnablereasonEnum.Functional, true);
            _arrangeButtonSet.SelectedButton = (RibbonToggleButton) sender;
            Globals.ThisAddIn.ArrangeSelectedImages(Arrangement.LineHorizonal, Padding(), Margin());
        }

        void MenuItemHAlign_Click(object sender, RibbonControlEventArgs e)
        {
            mnuHAlign_Click(sender, e);
        }

        void MenuItemVAlign_Click(object sender, RibbonControlEventArgs e)
        {
            mnuVAlign_Click(sender, e);
        }

        void mnuHAlign_Click(object sender, RibbonControlEventArgs e)
        {
            var ribbonButton = sender as RibbonToggleButton;
            if (ribbonButton == null) throw new InvalidOperationException();
            _hAlignButtonSet.SelectedButton = ribbonButton;
            Globals.ThisAddIn.DoPositionSelectedImages(hAlign: ribbonButton.Id);
        }

        void mnuVAlign_Click(object sender, RibbonControlEventArgs e)
        {
            var ribbonButton = sender as RibbonToggleButton;
            if (ribbonButton == null) throw new InvalidOperationException();
            _vAlignButtonSet.SelectedButton = ribbonButton;
            Globals.ThisAddIn.DoPositionSelectedImages(vAlign: ribbonButton.Id);
        }

        void buttonPaddingLess_Click(object sender, RibbonControlEventArgs e)
        {
            dropDownPadding_Change(sender, e, -1);
        }

        void buttonPaddingMore_Click(object sender, RibbonControlEventArgs e)
        {
            dropDownPadding_Change(sender, e, +1);
        }

        void dropDownPadding_Change(object sender, RibbonControlEventArgs e, int i)
        {
            try
            {
                dropDownPadding.SelectedItemIndex += i;
                dropDownPadding_ButtonClick(sender, e);
            }
            catch
            {
                // ignored : we're top or bottom of possible solutions and i would lead out of range...
            }
        }

        void dropDownPadding_ButtonClick(object sender, RibbonControlEventArgs e)
        {
            DoPositionSelectedImages();
        }

        int Padding() => 5 * (int)dropDownPadding.SelectedItem.Tag;
        int Margin () => 5 * (int)dropDownMargin .SelectedItem.Tag;

        void DoPositionSelectedImages()
        {
            Globals.ThisAddIn.DoPositionSelectedImages(Padding(), Margin());
        }

        void dropDownPadding_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            dropDownPadding_ButtonClick(sender, e);
        }

        void dropDownMargin_ButtonClick(object sender, RibbonControlEventArgs e)
        {
            DoPositionSelectedImages();
        }

        void dropDownMargin_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            dropDownMargin_ButtonClick(sender, e);
        }

        void buttonMarginLess_Click(object sender, RibbonControlEventArgs e)
        {
            dropDownMargin_Change(sender, e, -1);
        }

        void buttonMarginMore_Click(object sender, RibbonControlEventArgs e)
        {
            dropDownMargin_Change(sender, e, +1);
        }
        void dropDownMargin_Change(object sender, RibbonControlEventArgs e, int i)
        {
            try
            {
                dropDownMargin.SelectedItemIndex += i;
                dropDownMargin_ButtonClick(sender, e);
            }
            catch
            {
                // ignored : we're top or bottom of possible solutions and i would lead out of range...
            }
        }

        void ButtonImport_Click(object sender, RibbonControlEventArgs e)
        {
            var results = new FormImportPictures().ShowDialog();
            if (results == DialogResult.OK) Globals.ThisAddIn.ImportPictures();
        }

        void ButtonSetRelativePosition_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.DoRelativePositionSelectedImages();
        }

        void ButtonLowRes_Click(object sender, RibbonControlEventArgs e)
        {
            var fileNameHandler = new FileNameHandler(new PersistedUserPreferences()); 
            Globals.ThisAddIn.ChangePicturesResolution(fileNameHandler.FilePatternIsMatch, fileNameHandler.SmallFileNameMaker, fileNameHandler.SmallPatternIsMatch);
        }

        void ButtonHiRes_Click(object sender, RibbonControlEventArgs e)
        {
            var fileNameHandler = new FileNameHandler(new PersistedUserPreferences());
            Globals.ThisAddIn.ChangePicturesResolution(fileNameHandler.SmallPatternIsMatch, fileNameHandler.LargeFileNameMaker, fileNameHandler.FilePatternIsMatch);
        }

        void buttonPictureSorter_Click(object sender, RibbonControlEventArgs e)
        {
            new PicturesSorter.PictureSorterForm().Show();
        }

        void buttonTextWrappingSquare_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.TextWrapping(WdWrapType.wdWrapSquare);
        }

        void buttonTextWrappingInFrontOfText_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.TextWrapping(WdWrapType.wdWrapFront);
        }

        void buttonTextWrappingThrough_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.TextWrapping(WdWrapType.wdWrapThrough);
        }

        void buttonTextWrappingEditWrapPoints_Click(object sender, RibbonControlEventArgs e)
        {
        }

        void buttonTextWrappingBehindTextv_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.TextWrapping(WdWrapType.wdWrapBehind);
        }

        void buttonTextWrappingTopAndBottom_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.TextWrapping(WdWrapType.wdWrapTopBottom);
        }

        void buttonTextWrappingTight_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.TextWrapping(WdWrapType.wdWrapTight);
        }

        void buttonTextWrappingLeftOnly_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.TextWrapping(WdWrapSideType.wdWrapLeft);
        }

        void buttonTextWrappingBothSides_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.TextWrapping(WdWrapSideType.wdWrapBoth);
        }

        void buttonTextWrappingRightOnly_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.TextWrapping(WdWrapSideType.wdWrapRight);
        }

        void IniDropDownItems(RibbonDropDown dropdown, int min, int max, int selectedValue)
        {
            var items = GenIntDropdownItems(min, max - min + 1).ToArray();
            items.ForEach(dropdown.Items.Add);
            dropdown.SelectedItem = items.FirstOrDefault(i => (int)i.Tag == selectedValue);
            if (dropdown.SelectedItem == null) dropdown.SelectedItemIndex = (max - min + 1) / 2;
        }

        void buttonSpacingEqualHorizontal_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SpacingEqualHorizontal();
        }

        void buttonSpacingDecreaseHorizontal_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SpacingDecreaseHorizontal();
        }

        void buttonSpacingIncreaseHorizontal_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SpacingIncreaseHorizontal();
        }

        void buttonSpacingEqualVertical_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SpacingEqualVertical();
        }

        void buttonSpacingDecreaseVertical_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SpacingDecreaseVertical();
        }

        void buttonSpacingIncreaseVertical_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SpacingIncreaseVertical();
        }

        void buttonSpacingInterpolate_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SpacingInterpolate();
        }

        public void EnablePictureTools(int countOfSelectedShapes)
        {
            var shapeToolsRequiredCount = GetShapeToolsRequiredCount(countOfSelectedShapes);
            _buttonsActingOnOneOrMoreShapes  .SetEnabled(RibbonControlEnablereasonEnum.Selection, shapeToolsRequiredCount == ShapeToolRequiredCount.OneOrMore  );
            _buttonsActingOnTwoOrMoreShapes  .SetEnabled(RibbonControlEnablereasonEnum.Selection, shapeToolsRequiredCount == ShapeToolRequiredCount.TwoOrMore  );
            _buttonsActingOnThreeOrMoreShapes.SetEnabled(RibbonControlEnablereasonEnum.Selection, shapeToolsRequiredCount == ShapeToolRequiredCount.ThreeOrMore);
        }

        static ShapeToolRequiredCount GetShapeToolsRequiredCount(int countOfSelectedShapes) 
            => countOfSelectedShapes == 0 ? ShapeToolRequiredCount.None
             : countOfSelectedShapes == 1 ? ShapeToolRequiredCount.OneOrMore
             : countOfSelectedShapes == 2 ? ShapeToolRequiredCount.TwoOrMore
             : ShapeToolRequiredCount.ThreeOrMore;

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

    }
}
