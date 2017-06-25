using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Office.Tools.Word;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using VstoEx;
using System.Text.RegularExpressions;
using Mannex.Collections.Generic;
using MoreLinq;
using stdole;

namespace AlbumWordAddin
{
    using System.Diagnostics;
    using System.IO;
    using System.Threading;

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public class AlbumWordAddinUtils : IDispatch
    {
        readonly Positioner.Parms _positionerParms = new Positioner.Parms();
        Document ActiveDocument => Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveDocument);
        Word.Application Application => ActiveDocument.Application;
        Word.Selection Selection => Application.Selection;

        public Word.Shape[] SelectedShapes()
        {
            // ToArray() required to freeze the pointers
            var selectedShapes = Selection.ShapeRange.Cast<Word.Shape>().ToArray();
            Debug.Assert(selectedShapes.All(s => s != null));
            return selectedShapes;
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
            var doc = ActiveDocument;
            var pageNumber = Selection.GetPageNumber();
            var shapesOnPage = doc.Shapes.Cast<Word.Shape>()
                .Where(s => (s.Type == Office.MsoShapeType.msoLinkedPicture
                             || s.Type == Office.MsoShapeType.msoPicture
                            ) && s.Anchor.GetPageNumber() == pageNumber
                );
            using (Application.StatePreserver().FreezeScreenUpdating())
                shapesOnPage.ForEach(s => s.Select(Replace: false));
        }

        /// <summary>
        /// Aligns the selected images according to the passl Alignment enum.
        /// </summary>
        /// <param name="alignment"></param>
        /// <param name="forced">Used only for some forced alignment behaviors such as ForcedWidth or ForcedHeight</param>
        public void AlignSelectedImages(Alignment alignment, float forced)
        {
            using (Application.StatePreserver().FreezeScreenUpdating())
            {
                var shapes = SelectedShapes().ToArray();
                if (shapes.Length < 2) return;
                switch (alignment)
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
                        throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null);
                }
            }
        }

        internal void FixAnchorOfSelectedImages()
        {
            using (Application.StatePreserver().FreezeScreenUpdating())
            {
                var doc = ActiveDocument;
                var shapes = SelectedShapes().ToArray();
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
                        var sh = SelectedShapes().FirstOrDefault();
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

        internal void DoPositionSelectedImages(Arrangement arrangement, int padding, int margin)
        {
            _positionerParms.Padding = padding;
            _positionerParms.Margin = margin;
            var shapesCount = SelectedShapes().Length;
            switch (arrangement)
            {
                case Arrangement.LineVertical:
                    _positionerParms.Rows = shapesCount;
                    _positionerParms.Cols = 1;
                    break;
                case Arrangement.RectangleVertical:
                {
                    var tup = EuristicArrangeRectangle(shapesCount);
                    _positionerParms.Rows = tup.Item2;
                    _positionerParms.Cols = tup.Item1;
                }
                    break;
                case Arrangement.Square:
                    _positionerParms.Rows = _positionerParms.Cols = BestSquare(shapesCount);
                    break;
                case Arrangement.RectangleHorizontal:
                {
                    var tup = EuristicArrangeRectangle(shapesCount);
                    _positionerParms.Rows = tup.Item1;
                    _positionerParms.Cols = tup.Item2;
                }
                    break;
                case Arrangement.LineHorizonal:
                    _positionerParms.Rows = 1;
                    _positionerParms.Cols = shapesCount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(arrangement), arrangement, null);
            }
            DoPositionSelectedImages(_positionerParms);
        }

        static int BestSquare(int shapesCount)
        {
            var edge = (int) Math.Floor(Math.Sqrt(shapesCount));
            return edge*edge < shapesCount ? edge + 1 : edge;
        }

        internal void DoPositionSelectedImages(string hAlign, string vAlign)
        {
            HShape hShape;
            if (Enum.TryParse(hAlign?.Replace("hAlign", string.Empty), true, out hShape))
                _positionerParms.HShape = hShape;
            VShape vShape;
            if (Enum.TryParse(vAlign?.Replace("vAlign", string.Empty), true, out vShape))
                _positionerParms.VShape = vShape;
            DoPositionSelectedImages(_positionerParms);
        }

        internal void DoPositionSelectedImages(int padding, int margin)
        {
            _positionerParms.Padding = padding;
            _positionerParms.Margin = margin;
            DoPositionSelectedImages(_positionerParms);
        }

        void DoPositionSelectedImages(Positioner.Parms positionerParms)
        {
            var selectedShapes = SelectedShapes();
            if (selectedShapes.Length == 0) throw new InvalidOperationException("Please select one or more images.");
            if (selectedShapes.Any(s => s == null))
            {
                selectedShapes.ForEach(sh => Trace.WriteLine(sh.GetLocationString()));
                throw new InvalidOperationException("Some selected shapes are null");
            }
            var shapes = MoveAllToSamePage(selectedShapes).ReplaceSelection();
            if (selectedShapes.Length != shapes.Length)
            {
                Trace.WriteLine($"We had {selectedShapes.Length} selected shapes, {shapes} after MoveallToSamePage.");
                throw new InvalidOperationException("MoveallToSamePage altered shaped count");
            }
            if (shapes.Any(s => s == null))
            {
                selectedShapes.ForEach(sh => Trace.WriteLine(sh.GetLocationString()));
                throw new InvalidOperationException("Some moved shapes are null");
            }
            var clientArea = new Rectangle(0, 0, shapes[0].Anchor.PageSetup.PageWidth,
                shapes[0].Anchor.PageSetup.PageHeight);
            var rectangles = shapes.Select(s => new Rectangle(s));
            var positions = Positioner.DoPosition(positionerParms, clientArea, rectangles);

            ApplyPositions(shapes, positions);
        }

        void ApplyPositions(IEnumerable<Word.Shape> shapes, IEnumerable<Rectangle> positions)
        {
            using (Application.StatePreserver().FreezeScreenUpdating())
                foreach (var pos in shapes.ZipLongest(positions, (sh, re) => new {sh, re})
                    .Where(r => r.re != null && r.sh != null)
                )
                {
                    pos.sh.Left   = pos.re.Left  ;
                    pos.sh.Top    = pos.re.Top   ;
                    pos.sh.Width  = pos.re.Width ;
                    pos.sh.Height = pos.re.Height;
                }
        }

        // ReSharper disable once ParameterTypeCanBeEnumerable.Local
        IEnumerable<Word.Shape> MoveAllToSamePage(Word.Shape[] selectedShapes)
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

        static Tuple<int, int> EuristicArrangeRectangle(int shapeCount)
        {
            var fac1 = (int) Math.Floor(Math.Sqrt(shapeCount));
            var fac2 = fac1;
            while (fac1*fac2 < shapeCount) fac2++;
            return new Tuple<int, int>(fac1, fac2);
        }

        public void DoRelativePositionSelectedImages()
        {
            SelectedShapeIterator(sh =>
                {
                    sh.RelativeHorizontalPosition = Word.WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
                    sh.RelativeVerticalPosition   = Word.WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
                }
            );
        }

        public void ChangePicturesResolution(Func<string, bool> fromPatternIsMatch, Func<string, string> fileNameMaker, Func<string, bool> toPatternIsMatch)
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

        void SelectedShapeIterator(Action<Word.Shape> shapeAction)
        {
            using (Application.StatePreserver().FreezeScreenUpdating())
            {
                SelectedShapes().ToArray().ForEach(shapeAction);
            }
        }

        internal void TextWrapping(Word.WdWrapType     wdWrapType){SelectedShapeIterator(sh => sh.WrapFormat.Type = wdWrapType);}
        internal void TextWrapping(Word.WdWrapSideType wdWrapSide){SelectedShapeIterator(sh => sh.WrapFormat.Side = wdWrapSide);}

        void SpacingImpl(Func<IEnumerable<Rectangle>, IEnumerable<Rectangle>> spacerFunc)
        {
            var shapes = MoveAllToSamePage(SelectedShapes()).ReplaceSelection();
            if (shapes.Length == 0) throw new InvalidOperationException("Please select one or more images.");
            var rectangles = shapes.Select(s => new Rectangle(s));
            var positions = spacerFunc(rectangles);
            ApplyPositions(shapes, positions);
        }

        public void SpacingEqualHorizontal   () { SpacingImpl(Spacer.HorizontalEqualSpacing); }
        public void SpacingDecreaseHorizontal() { SpacingImpl(Spacer.DecreaseHorizontal    ); }
        public void SpacingIncreaseHorizontal() { SpacingImpl(Spacer.IncreaseHorizontal    ); }
        public void SpacingEqualVertical     () { SpacingImpl(Spacer.VerticalEqualSpacing  ); }
        public void SpacingDecreaseVertical  () { SpacingImpl(Spacer.DecreaseVertical      ); }
        public void SpacingIncreaseVertical  () { SpacingImpl(Spacer.IncreaseVertical      ); }
        public void SpacingInterpolate       () { SpacingImpl(Spacer.SpacingInterpolate    ); }
    }
}

