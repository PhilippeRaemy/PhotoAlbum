using System;
using System.Linq;
using System.Windows.Forms;
using Mannex;
using Word = Microsoft.Office.Interop.Word;

using Microsoft.Office.Tools.Word;

namespace AlbumWordAddin
{
    using System.IO;
    using Microsoft.Office.Core;
    using UserPreferences;

    public partial class ThisAddIn
    {
        Document ActiveDocument => Globals.Factory.GetVstoObject(Application.ActiveDocument);
        Word.Selection Selection => ActiveDocument.Application.Selection;
        public static AlbumRibbon ThisRibbon { get; set; }

        AlbumWordAddinUtils _utilities;

        protected override object RequestComAddInAutomationService()
        {
            return _utilities ?? (_utilities = new AlbumWordAddinUtils());
        }

        void ThisAddIn_Startup(object sender, EventArgs e)
        {
            try
            {
                Application_DocumentOpen(Application.ActiveDocument);
            }
            catch
            {
                // ignored
            }
            Application.DocumentOpen += Application_DocumentOpen;
            ((Word.ApplicationEvents4_Event) Application).NewDocument += Application_DocumentOpen;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(((Exception) e.ExceptionObject).Message, "An error occured in AlbumWordAddin",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void Application_DocumentOpen(Word.Document doc)
        {
            Globals.Factory.GetVstoObject(doc).SelectionChange += ThisAddIn_SelectionChange;
        }

        void ThisAddIn_SelectionChange(object sender, SelectionEventArgs e)
        {
            var s = _utilities.SelectedShapes();
            ThisRibbon.EnablePictureTools(s.Length);
        }

        internal void AlignSelectedImages(Alignment alignment, float forced = float.NaN)
        {
            _utilities.AlignSelectedImages(alignment, forced);
        }

        void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
        }

        public void RemoveEmptyPages()
        {
            _utilities.RemoveEmptyPages();
        }

        public void SelectShapesOnPage()
        {
            _utilities.SelectShapesOnPage();
        }

        public void FixAnchorOfSelectedImages()
        {
            _utilities.FixAnchorOfSelectedImages();
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        void InternalStartup()
        {
            Startup += ThisAddIn_Startup;
            Shutdown += ThisAddIn_Shutdown;
        }

        #endregion

        public void DoPositionSelectedImages(string hAlign = null, string vAlign = null)
        {
            _utilities.DoPositionSelectedImages(hAlign, vAlign);
        }

        public void ArrangeSelectedImages(Arrangement arrangement, int padding, int margin)
        {
            _utilities.DoPositionSelectedImages(arrangement, padding, margin);
        }

        internal void DoPositionSelectedImages(int padding, int margin)
        {
            _utilities.DoPositionSelectedImages(padding, margin);
        }

        public void DoRelativePositionSelectedImages()
        {
            _utilities.DoRelativePositionSelectedImages();
        }

        public void CreateNewAlbumDocument(DirectoryInfo directoryInfo)
        {
            var userPrefs = new PersistedUserPreferences();
            var newDocFile = new FileInfo(Path.Combine(directoryInfo.FullName, $"{directoryInfo.Name}.docx"));
            if (newDocFile.Exists)
                newDocFile =
                    new FileInfo(Path.Combine(directoryInfo.FullName,
                        $"{directoryInfo.Name}.{DateTime.Now:yyyyMMdd.HHmmss}.docx"));
            var newdoc = Application.Documents.Add(Template: userPrefs.NewDocumentTemplate, NewTemplate: false,
                DocumentType: Word.WdNewDocumentType.wdNewBlankDocument);
            newdoc.SaveAs(newDocFile.FullName);
        }

        public void CloseCurrentAlbumDocument(DirectoryInfo directoryInfo)
        {
            Application.ActiveDocument.Close(Word.WdSaveOptions.wdSaveChanges);
        }

        public void AddPictureToCurrentDocument(FileInfo fileInfo)
        {
            var pageWidth = Selection.PageSetup.PageWidth;
            var pageHeight = Selection.PageSetup.PageHeight;
            var sel = Application.Selection;
            sel.EndKey(Word.WdUnits.wdStory, Word.WdMovementType.wdMove);
            var shp = sel.Document.Shapes.AddPicture(fileInfo.FullName, LinkToFile: true, SaveWithDocument: false,
                Left: pageWidth * .05f, Top: pageHeight * .05f);
            shp.RelativeHorizontalPosition = Word.WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
            shp.RelativeVerticalPosition = Word.WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
            shp.LockAspectRatio = MsoTriState.msoCTrue;
            var hRatio = pageWidth / shp.Width;
            var vRatio = pageHeight / shp.Height;
            var maxShpWidth = shp.Width * 0.75f * (vRatio < hRatio ? vRatio : hRatio);
            var maxShpHeight = shp.Height * 0.75f * (vRatio < hRatio ? vRatio : hRatio);
            if (shp.Width  > maxShpWidth ) shp.Width  = maxShpWidth;
            if (shp.Height > maxShpHeight) shp.Height = maxShpHeight;
            shp.WrapFormat.Type = Word.WdWrapType.wdWrapTight;
            sel.EndKey(Word.WdUnits.wdStory, Word.WdMovementType.wdMove);
            sel.InsertBreak(Type: Word.WdBreakType.wdPageBreak);
        }

        public void ChangePicturesResolution(Func<string, bool> fromPatternIsMatch, Func<string, string> fileNameMaker,
            Func<string, bool> toPatternIsMatch)
        {
            _utilities.ChangePicturesResolution(fromPatternIsMatch, fileNameMaker, toPatternIsMatch);
        }

        public void ImportPictures()
        {
            var userprefs = new PersistedUserPreferences();
            var fw = new FolderWalker(
                userprefs.FolderImportStart,
                userprefs.FolderImportEnd,
                new FileNameHandler(userprefs),
                new StatusBarProgressIndicator(Application)
                // new FormProgress()
            );
            fw.StartingFolder += Fw_StartingFolder;
            fw.FoundAFile += Fw_FoundAFile;
            fw.EndingFolder += Fw_EndingFolder;
            fw.Run();
        }

        void Fw_EndingFolder(object sender, FolderEventArgs e)
        {
            CloseCurrentAlbumDocument(e.DirectoryInfo);
        }

        void Fw_FoundAFile(object sender, FileEventArgs e)
        {
            AddPictureToCurrentDocument(e.FileInfo);
        }

        void Fw_StartingFolder(object sender, FolderEventArgs e)
        {
            CreateNewAlbumDocument(e.DirectoryInfo);
        }

        public void TextWrapping(Word.WdWrapType wdWrapType)
        {
            _utilities.TextWrapping(wdWrapType);
        }

        internal void TextWrapping(Word.WdWrapSideType wdWrapSide)
        {
            _utilities.TextWrapping(wdWrapSide);
        }

        public void SpacingEqualHorizontal()
        {
            _utilities.SpacingEqualHorizontal();
        }

        public void SpacingDecreaseHorizontal()
        {
            _utilities.SpacingDecreaseHorizontal();
        }

        public void SpacingIncreaseHorizontal()
        {
            _utilities.SpacingIncreaseHorizontal();
        }

        public void SpacingEqualVertical()
        {
            _utilities.SpacingEqualVertical();
        }

        public void SpacingDecreaseVertical()
        {
            _utilities.SpacingDecreaseVertical();
        }

        public void SpacingIncreaseVertical()
        {
            _utilities.SpacingIncreaseVertical();
        }

        public void SpacingInterpolate()
        {
            _utilities.SpacingInterpolate();
        }

        public void MarginAdjust(int marginDelta)
        {
            _utilities.MarginAdjust(marginDelta);
        }
    }
}
