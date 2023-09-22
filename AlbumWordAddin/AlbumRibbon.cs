using UserPreferences;

namespace AlbumWordAddin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Microsoft.Office.Tools.Ribbon;
    using Mannex.Text.RegularExpressions;
    using Microsoft.Office.Interop.Word;
    using Positioning;
    using VstoEx.Extensions;
    using static MoreLinq.Extensions.ForEachExtension;

    public partial class AlbumRibbon
    {
        const int SpacingFactor = 5;
        const int MarginFactor = 5;
        RibbonToggleButtonSet _arrangeButtonSet;
        RibbonToggleButtonSet _hAlignButtonSet;
        RibbonToggleButtonSet _vAlignButtonSet;
        RibbonControlSet      _buttonsActingOnOneOrMoreShapes;
        RibbonControlSet      _buttonsActingOnTwoOrMoreShapes;
        RibbonControlSet      _buttonsActingOnThreeOrMoreShapes;
        RibbonControlSet      _buttonsActingOnTwoShapes;

        void AlbumRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            PerformLayout();
            var userPrefs = new PersistedUserPreferences();
            DropDownIntSetter(dropDownMargin, userPrefs.Margin);
            DropDownIntSetter(dropDownSpacing, userPrefs.Spacing);

            _arrangeButtonSet = new RibbonToggleButtonSet(
                EnumerateControls<RibbonToggleButton>(ctrl => ctrl.Name.IsMatch("buttonArrange")))
            {
                Enabled = true
            };
            _hAlignButtonSet                          = new RibbonToggleButtonSet(EnumerateControls<RibbonToggleButton>(ctrl => ctrl.Name.IsMatch("hAlign")));
            _vAlignButtonSet                          = new RibbonToggleButtonSet(EnumerateControls<RibbonToggleButton>(ctrl => ctrl.Name.IsMatch("vAlign")));
            _buttonsActingOnOneOrMoreShapes           = new RibbonControlSet(EnumerateControls(FilterOnTag(ShapeToolRequiredCount.OneOrMore)));
            _buttonsActingOnTwoShapes                 = new RibbonControlSet(EnumerateControls(FilterOnTag(ShapeToolRequiredCount.Two)));
            _buttonsActingOnTwoOrMoreShapes           = new RibbonControlSet(EnumerateControls(FilterOnTag(ShapeToolRequiredCount.TwoOrMore)));
            _buttonsActingOnThreeOrMoreShapes         = new RibbonControlSet(EnumerateControls(FilterOnTag(ShapeToolRequiredCount.ThreeOrMore)));
            _buttonsActingOnOneOrMoreShapes.Enabled   = false;
            _buttonsActingOnTwoShapes.Enabled         = false;
            _buttonsActingOnTwoOrMoreShapes.Enabled   = false;
            _buttonsActingOnThreeOrMoreShapes.Enabled = false;
        }

        static Func<RibbonControl, bool> FilterOnTag(ShapeToolRequiredCount shapeToolRequiredCount)
            => ctrl => ctrl.Tag is ShapeToolRequiredCount tag
                       && tag == shapeToolRequiredCount;

        IEnumerable<T> EnumerateControls<T>(Func<T, bool> filterFunc) where T : RibbonControl
            => from gr in TabAddIns.Groups
                from item in gr.Items
                from subItem in ((item as RibbonBox)?.Items
                                 ?? (item as RibbonButtonGroup)?.Items
                                 ?? Enumerable.Empty<RibbonControl>()
                    ).Prepend(item)
                where subItem is T && filterFunc((T) subItem)
                select (T) subItem;

        void AlbumRibbon_Close(object sender, EventArgs e)
        {
            new PersistedUserPreferences
            {
                Margin = (int) dropDownMargin.SelectedItem.Tag,
                Spacing = (int) dropDownSpacing.SelectedItem.Tag
            }.Save();
        }

        static void DropDownIntSetter(RibbonDropDown ribbonDropDown, int value)
        {
            ribbonDropDown.SelectedItem
                = ribbonDropDown.Items.FirstOrDefault(i => (int) i.Tag >= value)
                  ?? ribbonDropDown.SelectedItem;
        }

        void ButtonRemoveEmptyPages_Click  (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.RemoveEmptyPages();
        void ButtonSelectShapesOnPage_Click(object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.SelectShapesOnPage();
        void ButtonFixAnchors_Click        (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.FixAnchorOfSelectedImages();
        void ButtonSizeToWidest_Click      (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.AlignSelectedImages(Alignment.Widest);
        void ButtonSizeToTallest_Click     (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.AlignSelectedImages(Alignment.Tallest);
        void ButtonAlignTop_Click          (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.AlignSelectedImages(Alignment.Top);
        void ButtonAlignMiddle_Click       (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.AlignSelectedImages(Alignment.Middle);
        void ButtonAlignBottom_Click       (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.AlignSelectedImages(Alignment.Bottom);
        void ButtonAlignLeft_Click         (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.AlignSelectedImages(Alignment.Left);
        void ButtonAlignCenter_Click       (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.AlignSelectedImages(Alignment.Center);
        void ButtonAlignRight_Click        (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.AlignSelectedImages(Alignment.Right);
        void ButtonSizeToNarrowest_Click   (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.AlignSelectedImages(Alignment.Narrowest);
        void ButtonSizeToShortest_Click    (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.AlignSelectedImages(Alignment.Shortest);

        void ButtonArrange_Click(object sender, bool enableHAlign, bool enableVAlign, Arrangement arrangement)
        {
            _arrangeButtonSet.SelectedButton = (RibbonToggleButton)sender;
            _hAlignButtonSet.Enabled = enableHAlign;
            _vAlignButtonSet.Enabled = enableVAlign;
            Globals.ThisAddIn.ArrangeSelectedImages(arrangement, Spacing(), Margin());
        }

        void ButtonArrangeV_Click (object sender, RibbonControlEventArgs e) => ButtonArrange_Click(sender, true , false, Arrangement.LineVertical);
        void ButtonArrangeRV_Click(object sender, RibbonControlEventArgs e) => ButtonArrange_Click(sender, true , true , Arrangement.RectangleVertical);
        void ButtonArrangeSq_Click(object sender, RibbonControlEventArgs e) => ButtonArrange_Click(sender, true , true , Arrangement.Square);
        void ButtonArrangeRH_Click(object sender, RibbonControlEventArgs e) => ButtonArrange_Click(sender, true , true , Arrangement.RectangleHorizontal);
        void ButtonArrangeH_Click (object sender, RibbonControlEventArgs e) => ButtonArrange_Click(sender, false, true , Arrangement.LineHorizonal);
        void MenuItemHAlign_Click (object sender, RibbonControlEventArgs e) => MnuHAlign_Click(sender, e);
        void MenuItemVAlign_Click (object sender, RibbonControlEventArgs e) => MnuVAlign_Click(sender, e);

        void MnuHAlign_Click(object sender, RibbonControlEventArgs e)
        {
            var ribbonButton = sender as RibbonToggleButton;
            _hAlignButtonSet.SelectedButton = ribbonButton ?? throw new InvalidOperationException();
            Globals.ThisAddIn.DoPositionSelectedImages(hAlign: ribbonButton.Id);
        }

        void MnuVAlign_Click(object sender, RibbonControlEventArgs e)
        {
            var ribbonButton = sender as RibbonToggleButton;
            _vAlignButtonSet.SelectedButton = ribbonButton ?? throw new InvalidOperationException();
            Globals.ThisAddIn.DoPositionSelectedImages(vAlign: ribbonButton.Id);
        }

        void ButtonSpacingLess_Click(object sender, RibbonControlEventArgs e) => DropDownSpacing_Change(sender, e, -1);
        void ButtonSpacingMore_Click(object sender, RibbonControlEventArgs e) => DropDownSpacing_Change(sender, e, +1);

        void DropDownSpacing_Change(object sender, RibbonControlEventArgs e, int i)
        {
            try
            {
                dropDownSpacing.SelectedItemIndex += i;
                DropDownSpacing_ButtonClick(sender, e);
            }
            catch
            {
                // ignored : we're top or bottom of possible solutions and it would lead out of range...
            }
        }

        void DropDownSpacing_ButtonClick(object sender, RibbonControlEventArgs e) => DoPositionSelectedImages();

        int Spacing() => SpacingFactor * (int) dropDownSpacing.SelectedItem.Tag;
        int Margin() => MarginFactor * (int) dropDownMargin.SelectedItem.Tag;

        void DoPositionSelectedImages()                                                => Globals.ThisAddIn.DoPositionSelectedImages(Spacing(), Margin());
        void DropDownSpacing_SelectionChanged(object sender, RibbonControlEventArgs e) => DropDownSpacing_ButtonClick(sender, e);
        void DropDownMargin_ButtonClick      (object sender, RibbonControlEventArgs e) => DoPositionSelectedImages();
        void DropDownMargin_SelectionChanged (object sender, RibbonControlEventArgs e) => DropDownMargin_ButtonClick(sender, e);

        void ButtonMargin_Click(object sender, RibbonControlEventArgs e, int direction)
        {
            DropDownMargin_Change(sender, e, direction);
            DropDownIntSetter(dropDownMargin,
                (int)Math.Round(Globals.ThisAddIn.MarginAdjust(direction * MarginFactor)));
        }

        void ButtonMarginLess_Click(object sender, RibbonControlEventArgs e) => ButtonMargin_Click(sender, e, -1);
        void ButtonMarginMore_Click(object sender, RibbonControlEventArgs e) => ButtonMargin_Click(sender, e, +1);

        void DropDownMargin_Change(object sender, RibbonControlEventArgs e, int i)
        {
            try
            {
                dropDownMargin.SelectedItemIndex += i;
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

        void ButtonSetRelativePosition_Click(object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.DoRelativePositionSelectedImages();

        void ButtonLowRes_Click(object sender, RibbonControlEventArgs e)
        {
            var fileNameHandler = new FileNameHandler(new PersistedUserPreferences());
            Globals.ThisAddIn.ChangePicturesResolution(fileNameHandler.SmallFileNameMaker, fileNameHandler.LargeFileNameMaker, true, false);
        }

        void ButtonHiRes_Click(object sender, RibbonControlEventArgs e)
        {
            var fileNameHandler = new FileNameHandler(new PersistedUserPreferences());
            Globals.ThisAddIn.ChangePicturesResolution(fileNameHandler.LargeFileNameMaker, fileNameHandler.LargeFileNameMaker, false, false);
        }

        void ButtonRightRes_Click(object sender, RibbonControlEventArgs e)
        {
            var fileNameHandler = new FileNameHandler(new PersistedUserPreferences());
            Globals.ThisAddIn.ChangePicturesResolution(fileNameHandler.RightFileNameMaker, fileNameHandler.LargeFileNameMaker, true, true);
        }

        void ButtonPictureSorter_Click            (object sender, RibbonControlEventArgs e) => new PicturesSorter.PictureSorterForm().Show();
        void ButtonTextWrappingSquare_Click       (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.TextWrapping(WdWrapType.wdWrapSquare);
        void ButtonTextWrappingInFrontOfText_Click(object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.TextWrapping(WdWrapType.wdWrapFront);
        void ButtonTextWrappingThrough_Click      (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.TextWrapping(WdWrapType.wdWrapThrough);
        void ButtonTextWrappingBehindTextv_Click  (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.TextWrapping(WdWrapType.wdWrapBehind);
        void ButtonTextWrappingTopAndBottom_Click (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.TextWrapping(WdWrapType.wdWrapTopBottom);
        void ButtonTextWrappingTight_Click        (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.TextWrapping(WdWrapType.wdWrapTight);
        void ButtonTextWrappingLeftOnly_Click     (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.TextWrapping(WdWrapSideType.wdWrapLeft);
        void ButtonTextWrappingBothSides_Click    (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.TextWrapping(WdWrapSideType.wdWrapBoth);
        void ButtonTextWrappingRightOnly_Click    (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.TextWrapping(WdWrapSideType.wdWrapRight);

        void IniDropDownItems(RibbonDropDown dropdown, int min, int max, int selectedValue)
        {
            var items = GenIntDropdownItems(min, max - min + 1).CheapToArray();
            items.ForEach(dropdown.Items.Add);
            dropdown.SelectedItem = items.FirstOrDefault(i => (int) i.Tag == selectedValue);
            if (dropdown.SelectedItem == null) dropdown.SelectedItemIndex = (max - min + 1) / 2;
        }

        void ButtonSpacingEqualHorizontal_Click   (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.SpacingEqualHorizontal();
        void ButtonSpacingDecreaseHorizontal_Click(object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.SpacingDecreaseHorizontal();
        void ButtonSpacingIncreaseHorizontal_Click(object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.SpacingIncreaseHorizontal();
        void ButtonSpacingEqualVertical_Click     (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.SpacingEqualVertical();
        void ButtonSpacingDecreaseVertical_Click  (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.SpacingDecreaseVertical();
        void ButtonSpacingIncreaseVertical_Click  (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.SpacingIncreaseVertical();
        void ButtonSpacingInterpolate_Click       (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.SpacingInterpolate();

        public void EnablePictureTools(int countOfSelectedShapes)
        {
            _buttonsActingOnOneOrMoreShapes.Enabled = countOfSelectedShapes >= 1;
            _buttonsActingOnTwoShapes.Enabled = countOfSelectedShapes == 2;
            _buttonsActingOnTwoOrMoreShapes.Enabled = countOfSelectedShapes >= 2;
            _buttonsActingOnThreeOrMoreShapes.Enabled = countOfSelectedShapes >= 3;
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

        void ButtonSpacingIncreaseBoth_Click         (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.SpacingIncreaseBoth();
        void ButtonSpacingDecreaseBoth_Click         (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.SpacingDecreaseBoth();
        void ButtonUndo_Click                        (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.Undo();
        void ButtonRedo_Click                        (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.Redo();
        void ButtonSwapPositions_Click               (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.RotateSelectedImages(1);
        void ButtonRotatePositionsClockwise_Click    (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.RotateSelectedImages(1);
        void ButtonRotatePositionsAnticlockwise_Click(object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.RotateSelectedImages(-1);
        void ShowShapeProperties                     (object sender, RibbonControlEventArgs e) => Globals.ThisAddIn.ShowSelectedImagesProperties();

        void ToggleFlowGrid_Click(object sender, RibbonControlEventArgs e)
        {
            var toggle = (RibbonToggleButton) sender;
            toggle.Image = toggle.Checked
                ? Properties.Resources.ToggleFlowGridFlow
                : Properties.Resources.ToggleFlowGridGrid;
            Globals.ThisAddIn.TogglePositioner(useFlow: toggle.Checked);
        }
    }
}
