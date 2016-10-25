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
    public enum Alignment
    {
        Top,
        Middle,
        Bottom,
        Left,
        Center,
        Right
    }

    [ComVisible(true)]
    public interface IAlbumWordAddinUtils
    {
        void RemoveEmptyPages();
        void SelectShapesOnPage();
        void AlignSelectedImages(Alignment alignment);
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public class AlbumWordAddinUtils : IAlbumWordAddinUtils, IDispatch
    {
        Document ActiveDocument => Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveDocument);
        Word.Selection Selection => ActiveDocument.Application.Selection;

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
                                && string.IsNullOrWhiteSpace(Regex.Replace(p.Range.Text, @"\x12\x09", string.Empty))
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

        public void  AlignSelectedImages(Alignment alignment)
        {
            using (ActiveDocument.Application.StatePreserver().FreezeScreenUpdating())
            {
                var doc = ActiveDocument;
                var shapes = Selection.ShapeRange.Cast<Word.Shape>().ToArray();
                if (shapes.Length<2) return;
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
                            var pos = shapes.Average(shp => shp.Top + shp.Height / 2);
                            shapes.ForEach(shp => shp.Top = pos - shp.Height / 2);
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
                            var pos = shapes.Average(shp => shp.Left + shp.Width / 2);
                            shapes.ForEach(shp => shp.Top = pos - shp.Width/ 2);
                        }
                        break;
                    case Alignment.Right:
                        {
                            var pos = shapes.Max(shp => shp.Left + shp.Width);
                            shapes.ForEach(shp => shp.Top = pos - shp.Width);
                        }
                        break;
                }
            }
        }

        internal void FixAnchorOfSelectedImages()
        {
            using (ActiveDocument.Application.StatePreserver().FreezeScreenUpdating())
            {
                var doc = ActiveDocument;
                var shapes = Selection.ShapeRange.Cast<Word.Shape>().ToArray();
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
                        var sh = Selection.ShapeRange.Cast<Word.Shape>().FirstOrDefault();
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

    }
}
