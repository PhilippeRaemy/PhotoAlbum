using System;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

using Microsoft.Office.Tools.Word;

namespace AlbumWordAddin
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Microsoft.Office.Core;
    using MoreLinq;
    using UserPreferences;
    using VstoEx.Extensions;
    using VstoEx.Geometry;
    using VstoEx.Progress;

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
            var s = SelectedShapes();
            ThisRibbon.EnablePictureTools(s.Length);
        }

        internal void AlignSelectedImages(Alignment alignment, float forced = Single.NaN)
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
            SpacingImpl(Spacer.HorizontalEqualSpacing);
        }

        public void SpacingDecreaseHorizontal()
        {
            SpacingImpl(Spacer.DecreaseHorizontal    );
        }

        public void SpacingIncreaseHorizontal()
        {
            SpacingImpl(Spacer.IncreaseHorizontal    );
        }

        public void SpacingEqualVertical()
        {
            SpacingImpl(Spacer.VerticalEqualSpacing  );
        }

        public void SpacingDecreaseVertical()
        {
            SpacingImpl(Spacer.DecreaseVertical      );
        }

        public void SpacingIncreaseVertical()
        {
            SpacingImpl(Spacer.IncreaseVertical      );
        }

        public void SpacingInterpolate()
        {
            SpacingImpl(Spacer.SpacingInterpolate    );
        }

        void SpacingImpl(Func<IEnumerable<Rectangle>, IEnumerable<Rectangle>> spacerFunc)
        {
            var shapes = MoveAllToSamePage(SelectedShapes()).ReplaceSelection();
            if (shapes.Length == 0) throw new InvalidOperationException("Please select one or more images.");
            var rectangles = shapes.Select(s => new Rectangle(s));
            var positions = spacerFunc(rectangles);
            _utilities.ApplyPositions(shapes, positions);
        }


        public void MarginAdjust(int marginDelta)
        {
            _utilities.MarginAdjust(marginDelta);
        }

        public IEnumerable<Word.Shape> MoveAllToSamePage(Word.Shape[] selectedShapes)
        {
            if (selectedShapes
                    .Select(s => s.GetPageNumber())
                    .Distinct()
                    .Count() <= 1
            )
            {
                return selectedShapes;
            }
            Word.Range anchor = null;
            foreach (var shape in selectedShapes)
            {
                if (anchor == null)
                {
                    anchor = shape.Anchor;
                    shape.Select(Replace: true);
                }
                else
                {
                    shape.Select(Replace: false);
                }
            }
            if (anchor == null) return Enumerable.Empty<Word.Shape>();
            Selection.Cut();
            anchor.Select();
            Selection.Paste();
            return selectedShapes;
        }

        public Word.Shape[] SelectedShapes()
        {
            // ToArray() required to freeze the pointers
            var selectedShapes = Selection.ShapeRange.Cast<Word.Shape>().ToArray();
            Debug.Assert(selectedShapes.All(s => s != null));
            return selectedShapes;
        }

        public void SelectedShapeIterator(Action<Word.Shape> shapeAction)
        {
            using (Application.StatePreserver().FreezeScreenUpdating())
            {
                Globals.ThisAddIn.SelectedShapes().ToArray().ForEach(shapeAction);
            }
        }
    }
}
