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
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public class AlbumWordAddinUtils : IAlbumWordAddinUtils, IDispatch
    {
        readonly Positioner.Parms _positionerParms = new Positioner.Parms();
        Document ActiveDocument => Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveDocument);
        Word.Selection Selection => ActiveDocument.Application.Selection;
        IEnumerable<Word.Shape> SelectedShapes() => Selection.ShapeRange.Cast<Word.Shape>();

        public void RemoveEmptyPages()
        {
            using (ActiveDocument.Application.StatePreserver().FreezeScreenUpdating())
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
            shapesOnPage.ForEach(s => s.Select(Replace: false));
        }

        /// <summary>
        /// Aligns the selected images according to the passl Alignment enum.
        /// </summary>
        /// <param name="alignment"></param>
        /// <param name="forced">Used only for some forced alignment behaviors such as ForcedWidth or ForcedHeight</param>
        public void AlignSelectedImages(Alignment alignment, float forced)
        {
            using (ActiveDocument.Application.StatePreserver().FreezeScreenUpdating())
            {
                var doc = ActiveDocument;
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
                        shapes.ForEach(shp => shp.Top = pos - shp.Width/2);
                    }
                        break;
                    case Alignment.Right:
                    {
                        var pos = shapes.Max(shp => shp.Left + shp.Width);
                        shapes.ForEach(shp => shp.Top = pos - shp.Width);
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
            using (ActiveDocument.Application.StatePreserver().FreezeScreenUpdating())
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

        internal void DoPositionSelectedImages(Arrangement arrangement)
        {
            var shapesCount = SelectedShapes().Count();
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
                    _positionerParms.Rows = _positionerParms.Cols = (int) Math.Floor(Math.Sqrt(shapesCount)) + 1;
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
            var shapes = MoveAllToSamePage(SelectedShapes()).ToArray();
            if (shapes.Length == 0) throw new InvalidOperationException("Please select one or more images.");
            var clientArea = new Rectangle(0, 0, shapes[0].Anchor.PageSetup.PageWidth,
                shapes[0].Anchor.PageSetup.PageHeight);
            var rectangles = shapes.Select(s => new Rectangle(s.Left, s.Top, s.Width, s.Height));
            var positions = Positioner.DoPosition(positionerParms, clientArea, rectangles);

            foreach (var pos in shapes.ZipLongest(positions, (sh, re) => new {sh, re})
                    .Where(r => r.re != null && r.sh != null)
            )
            {
                pos.sh.Left = pos.re.Left;
                pos.sh.Top = pos.re.Top;
                pos.sh.Width = pos.re.Width;
                pos.sh.Height = pos.re.Height;
            }
        }


        IEnumerable<Word.Shape> MoveAllToSamePage(IEnumerable<Word.Shape> selectedShapes)
        {
            Word.Range anchor = null;
            var pageNumber = -1;
            foreach (var shape in selectedShapes)
            {
                if (anchor == null)
                {
                    anchor = shape.Anchor;
                    pageNumber = anchor.GetPageNumber();
                    yield return shape;
                }
                else
                {
                    if (shape.Anchor.GetPageNumber() == pageNumber)
                    {
                        yield return shape;
                    }
                    else
                    {
                        shape.Select(Replace: true);
                        Selection.Cut();
                        anchor.Select();
                        Selection.Paste();
                        yield return SelectedShapes().FirstOrDefault();
                    }
                }
            }
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
            using (ActiveDocument.Application.StatePreserver().FreezeScreenUpdating())
            {
                var shapes = SelectedShapes().ToArray();
                if (!shapes.Any()) return;
                foreach (var shape in shapes)
                {
                    shape.RelativeHorizontalPosition = Word.WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
                    shape.RelativeVerticalPosition   = Word.WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
                }
            }
        }
    }
}

