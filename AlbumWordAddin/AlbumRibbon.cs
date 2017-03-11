namespace AlbumWordAddin
{
    using System;
    using System.Globalization;
    using Microsoft.Office.Tools.Ribbon;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using UserPreferences;

    public partial class AlbumRibbon
    {
        void AlbumRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            PerformLayout();
            var userPrefs = new PersistedUserPreferences();
            DropDownIntSetter(dropDownMargin, userPrefs.Margin);
            DropDownIntSetter(dropDownPadding, userPrefs.Padding);
        }

        void AlbumRibbon_Close(object sender, EventArgs e)
        {
            using (var userPrefs = new PersistedUserPreferences())
            {
                userPrefs.Margin  = (int) dropDownMargin .SelectedItem.Tag;
                userPrefs.Padding = (int) dropDownPadding.SelectedItem.Tag;
            }
        }

        static void DropDownIntSetter(RibbonDropDown ribbonDropDown, int value)
        {
            ribbonDropDown.SelectedItem 
                =  ribbonDropDown.Items.FirstOrDefault(i => (int) i.Tag < value)
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

        void editBoxSizeWidth_TextChanged(object sender, RibbonControlEventArgs e)
        {
            float f;
            if (float.TryParse(editBoxSizeWidth.Text, NumberStyles.Float, CultureInfo.CurrentUICulture, out f))
            {
                Globals.ThisAddIn.AlignSelectedImages(Alignment.ForceWidth, f);
            }
        }

        void editBoxSizedHeight_TextChanged(object sender, RibbonControlEventArgs e)
        {
            float f;
            if (float.TryParse(editBoxSizeWidth.Text, NumberStyles.Float, CultureInfo.CurrentUICulture, out f))
            {
                Globals.ThisAddIn.AlignSelectedImages(Alignment.ForceHeight, f);
            }
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

        void buttonArrangeLV_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.ArrangeSelectedImages(Arrangement.LineVertical);
        }

        void buttonArrangeRV_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.ArrangeSelectedImages(Arrangement.RectangleVertical);
        }

        void buttonArrangeSq_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.ArrangeSelectedImages(Arrangement.Square);
        }

        void buttonArrangeRH_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.ArrangeSelectedImages(Arrangement.RectangleHorizontal);
        }

        void buttonArrangeH_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.ArrangeSelectedImages(Arrangement.LineHorizonal);
        }

        void MenuItemHAlign_Click(object sender, RibbonControlEventArgs e)
        {
            mnuHAlign_Click(sender, e);
        }

        void MenuItemVAlign_Click(object sender, RibbonControlEventArgs e)
        {
            mnuVAlign_Click(sender, e);
        }

        static void mnuHAlign_Click(object sender, RibbonControlEventArgs e)
        {
            var ribbonButton = sender as RibbonButton;
            if (ribbonButton == null) throw new InvalidOperationException();
            Globals.ThisAddIn.DoPositionSelectedImages(hAlign: ribbonButton.Id);
        }

        static void mnuVAlign_Click(object sender, RibbonControlEventArgs e)
        {
            var ribbonButton = sender as RibbonButton;
            if (ribbonButton == null) throw new InvalidOperationException();
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

        void DoPositionSelectedImages()
        {
            Globals.ThisAddIn.DoPositionSelectedImages(padding: 5 * (int) dropDownPadding.SelectedItem.Tag, margin: 5 * (int) dropDownMargin.SelectedItem.Tag);
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
            var frm = new FormImportPictures();
            if (frm.ShowDialog() != DialogResult.OK) return;
            var userPrefs = new PersistedUserPreferences();
            var walker = new FolderWalker(
                userPrefs.FolderImportStart, 
                userPrefs.FolderImportEnd,
                FileNameHandlerFromUserPrefs(userPrefs),
                new FormProgress()
                );
            walker.StartingFolder += Walker_StartingFolder;
            walker.EndingFolder += Walker_EndingFolder;
            walker.FoundAFile += Walker_FoundAFile;
            walker.Run();
        }

        static FileNameHandler FileNameHandlerFromUserPrefs(UserPreferences.UserPreferences userPrefs)
        {
            var smallFileNameMakerRe = new Regex(@"\.(jpg|jpeg)$", RegexOptions.IgnoreCase);
            var fileNameMaker = new FileNameHandler(
                userPrefs.IncludeFiles,
                userPrefs.ExcludeFiles,
                @"\.small\.((jpeg)|(jpg))$",
                s => smallFileNameMakerRe.Replace(s, ".small.$1"),
                s => new Regex(@"(.*\.)small\.(jpg|jpeg)$", RegexOptions.IgnoreCase).Replace(s, "$1.$2")
            );
            return fileNameMaker;
        }

        static void Walker_FoundAFile(object sender, FileEventArgs e)
        {
            Globals.ThisAddIn.AddPictureToCurrentDocument(e.FileInfo);
        }

        static void Walker_EndingFolder(object sender, FolderEventArgs e)
        {
            Globals.ThisAddIn.CloseCurrentAlbumDocument(e.DirectoryInfo);
        }

        static void Walker_StartingFolder(object sender, FolderEventArgs e)
        {
            Globals.ThisAddIn.CreateNewAlbumDocument(e.DirectoryInfo);
        }

        void ButtonSetRelativePosition_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.DoRelativePositionSelectedImages();
        }

        void ButtonLowRes_Click(object sender, RibbonControlEventArgs e)
        {
            var fileNameHandler = FileNameHandlerFromUserPrefs(new PersistedUserPreferences()); 
            Globals.ThisAddIn.ChangePicturesResolution(fileNameHandler.FilePatternIsMatch, fileNameHandler.SmallFileNameMaker, fileNameHandler.SmallPatternIsMatch);
        }

        void ButtonHiRes_Click(object sender, RibbonControlEventArgs e)
        {
            var fileNameHandler = FileNameHandlerFromUserPrefs(new PersistedUserPreferences());
            Globals.ThisAddIn.ChangePicturesResolution(fileNameHandler.SmallPatternIsMatch, fileNameHandler.LargeFileNameMaker, fileNameHandler.FilePatternIsMatch);
        }

    }
}
