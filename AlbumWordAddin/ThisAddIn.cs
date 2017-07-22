namespace AlbumWordAddin
{
    using System;
    using System.Windows.Forms;
    using Word = Microsoft.Office.Interop.Word;
    using Microsoft.Office.Tools.Word;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Mannex.Collections.Generic;
    using Microsoft.Office.Core;
    using MoreLinq;
    using Positioning;
    using UserPreferences;
    using VstoEx;
    using VstoEx.Extensions;
    using VstoEx.Geometry;
    using VstoEx.Progress;

    // ReSharper disable once ClassNeverInstantiated.Global
    [SuppressMessage("ReSharper", "LocalizableElement")]
    public partial class ThisAddIn
    {
        Document ActiveDocument => Globals.Factory.GetVstoObject(Application.ActiveDocument);
        Word.Selection Selection => ActiveDocument.Application.Selection;
        public static AlbumRibbon ThisRibbon { get; set; }

        readonly PositionManager _utilities = new PositionManager();

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
            Alignment alignment1 = alignment;
            using (Application.StatePreserver().FreezeScreenUpdating())
            {
                var shapes = Globals.ThisAddIn.SelectedShapes().ToArray();
                if (shapes.Length < 2) return;
                switch (alignment1)
                {
                    case Alignment.Top:
                    {
                        var pos = shapes.Min(shp => shp.Top);
                        shapes.ForEach(shp => shp.Top = pos);
                    }
                        break;
                    case Alignment.Middle:
                    {
                        var pos = shapes.Average(shp => shp.Top + shp.Height/2);
                        shapes.ForEach(shp => shp.Top = pos - shp.Height/2);
                    }
                        break;
                    case Alignment.Bottom:
                    {
                        var pos = shapes.Max(shp => shp.Top + shp.Height);
                        shapes.ForEach(shp => shp.Top = pos - shp.Height);
                    }
                        break;
                    case Alignment.Left:
                    {
                        var pos = shapes.Min(shp => shp.Left);
                        shapes.ForEach(shp => shp.Left = pos);
                    }
                        break;
                    case Alignment.Center:
                    {
                        var pos = shapes.Average(shp => shp.Left + shp.Width/2);
                        shapes.ForEach(shp => shp.Left = pos - shp.Width/2);
                    }
                        break;
                    case Alignment.Right:
                    {
                        var pos = shapes.Max(shp => shp.Left + shp.Width);
                        shapes.ForEach(shp => shp.Left = pos - shp.Width);
                    }
                        break;
                    case Alignment.Narrowest:
                    {
                        var pos = shapes.Min(shp => shp.Width);
                        shapes.ForEach(shp => shp.Width = pos);
                    }
                        break;
                    case Alignment.Shortest:
                    {
                        var pos = shapes.Min(shp => shp.Height);
                        shapes.ForEach(shp => shp.Height = pos);
                    }
                        break;
                    case Alignment.Tallest:
                    {
                        var pos = shapes.Max(shp => shp.Height);
                        shapes.ForEach(shp => shp.Height = pos);
                    }
                        break;
                    case Alignment.Widest:
                    {
                        var pos = shapes.Max(shp => shp.Width);
                        shapes.ForEach(shp => shp.Width = pos);
                    }
                        break;
                    case Alignment.ForceWidth:
                        if (!float.IsNaN(forced) && forced > 0)
                        {
                            shapes.ForEach(shp => shp.Width = forced);
                        }
                        break;
                    case Alignment.ForceHeight:
                        if (!float.IsNaN(forced) && forced > 0)
                        {
                            shapes.ForEach(shp => shp.Height = forced);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(alignment1), alignment1, null);
                }
            }
        }

        void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
        }

        public void RemoveEmptyPages()
        {
            using (Application.StatePreserver().FreezeScreenUpdating())
            {

                var paragraphsToBeDeleted = ActiveDocument.Paragraphs.Cast<Word.Paragraph>()
                        .Select(p => new
                            {
                                Paragraph = p,
                                PageNumber = p.Range.GetPageNumber(),
                                IsEmpty = p.Range.ShapeRange.Count == 0
                                          &&
                                          string.IsNullOrWhiteSpace(Regex.Replace(p.Range.Text, @"\x12\x09",
                                              string.Empty))
                            }
                        )
                        .GroupBy(p => p.PageNumber)
                        .Where(g => g.All(p => p.IsEmpty))
                        .OrderByDescending(g => g.Key)
                        .SelectMany(g => g.Select(gg => gg.Paragraph))
                        .ToList()
                    ;
                foreach (var paragraph in paragraphsToBeDeleted)
                {
                    paragraph.Range.Delete();
                }
            }
        }

        public void SelectShapesOnPage()
        {
            var pageNumber = Selection.GetPageNumber();
            var shapesOnPage = ActiveDocument.Shapes.Cast<Word.Shape>()
                .Where(s => (s.Type == MsoShapeType.msoLinkedPicture
                          || s.Type == MsoShapeType.msoPicture
                            ) && s.Anchor.GetPageNumber() == pageNumber
                );
            using (Application.StatePreserver().FreezeScreenUpdating())
                shapesOnPage.ForEach(s => s.Select(Replace: false));
        }

        public void FixAnchorOfSelectedImages()
        {
            using (Application.StatePreserver().FreezeScreenUpdating())
            {
                var doc = ActiveDocument;
                var shapes = Globals.ThisAddIn.SelectedShapes().ToArray();
                if (!shapes.Any()) return;
                var paragraphsByPage =
                    doc.Paragraphs.Cast<Word.Paragraph>()
                        .GroupBy(p => p.Range.GetPageNumber())
                        .ToArray();
                var bestAnchors =
                    shapes
                        .Select(sh => sh.AsKeyTo(paragraphsByPage.First(p => p.Key == sh.GetPageNumber()).First()));
                var newShapes = new List<Word.Shape>();
                foreach (var kvp in bestAnchors)
                {
                    if (kvp.Key.Anchor == kvp.Value.Range) newShapes.Add(kvp.Key);
                    else
                    {
                        kvp.Key.Select(Replace: true);
                        Selection.Cut();
                        kvp.Value.Range.Select();
                        Selection.Paste();
                        var sh = Globals.ThisAddIn.SelectedShapes().FirstOrDefault();
                        if (sh != null)
                        {
                            newShapes.Add(sh);
                        }
                    }
                }
                newShapes.FirstOrDefault()?.Select(Replace: true);
                newShapes.Skip(1).ForEach(sh => sh.Select(Replace: false));
            }
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

        public void ArrangeSelectedImages(Arrangement arrangement, int spacing, int margin)
        {
            _utilities.DoPositionSelectedImages(arrangement, spacing, margin);
        }

        internal void DoPositionSelectedImages(int spacing, int margin)
        {
            _utilities.DoPositionSelectedImages(spacing, margin);
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
            using (new StatePreserver(Application).FreezeScreenUpdating())
            using(var progress = (StatusBarProgressIndicator)new StatusBarProgressIndicator(Application).InitProgress(ActiveDocument.Shapes.Count, "Change picture resolution"))
            {
                foreach (var shape in ActiveDocument.Shapes
                    .Cast<Word.Shape>()
                    // ReSharper disable once AccessToDisposedClosure
                    .Pipe(sh => progress.Progress(string.Empty))
                    .Where(sh => sh.LinkFormat.Type == Word.WdLinkType.wdLinkTypePicture))
                {
                    progress.SetCaption(shape.LinkFormat.SourceFullName);
                    var fileInfo = new FileInfo(shape.LinkFormat.SourceFullName);
                    if (fileInfo.DirectoryName == null) break;
                    var newFileInfo = new FileInfo(Path.Combine(fileInfo.DirectoryName, fileNameMaker(fileInfo.Name)));
                    if (newFileInfo.Exists) shape.LinkFormat.SourceFullName = newFileInfo.FullName;
                }
            }
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
            SelectedShapeIterator(sh => sh.WrapFormat.Type = wdWrapType);
        }

        internal void TextWrapping(Word.WdWrapSideType wdWrapSide)
        {
            SelectedShapeIterator(sh => sh.WrapFormat.Side = wdWrapSide);
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
            var positions = spacerFunc(shapes.ToRectangles());

            using (Application.StatePreserver().FreezeScreenUpdating())
                shapes.ApplyPositions(positions);
        }


        public float MarginAdjust(float increment)
        {
            return SelectedShapesAdjustImpl(rr => rr.IncreaseMargin(increment),
                r => r.Aggregate((r1,r2) => r1.Absorb(r2)).AverageDistance(new Rectangle(0, 0, ActiveDocument.PageSetup.PageWidth, ActiveDocument.PageSetup.PageHeight))
                );
        }

        public float SpacingAdjust(float scale)
        {
            return SelectedShapesAdjustImpl(r => r.IncreaseSpacing(scale), _ => 0f);
        }

        T SelectedShapesAdjustImpl<T>(
            Func<IEnumerable<Rectangle>, IEnumerable<Rectangle>> transformation,
            Func<IEnumerable<Rectangle>, T> feedbackFunc
            )
        {
            if (SelectedShapes().Select(sh => sh.GetPageNumber()).Distinct().Count() > 1)
            {
                throw new InvalidOperationException("Please make sure that all the selected shapes are on the same page");
            }
            var rectangles = transformation(SelectedShapes().ToRectangles())
                                .ToArray();
            using (Application.StatePreserver().FreezeScreenUpdating())
                SelectedShapes().ApplyPositions(rectangles);
            return feedbackFunc(rectangles);
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
