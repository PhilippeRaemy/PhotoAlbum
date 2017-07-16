using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Office.Tools.Word;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using VstoEx;
using VstoEx.Geometry;
using VstoEx.Extensions;
using System.Text.RegularExpressions;
using Mannex.Collections.Generic;
using MoreLinq;
using stdole;

namespace AlbumWordAddin
{
    using System.Diagnostics;
    using System.IO;
    using VstoEx.Geometry;
    using VstoEx.Progress;

    public class AlbumWordAddinUtils
    {
        readonly Positioner.Parms _positionerParms = new Positioner.Parms();
        Document ActiveDocument => Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveDocument);
        Word.Application Application => ActiveDocument.Application;
        Word.Selection Selection => Application.Selection;

        internal void DoPositionSelectedImages(Arrangement arrangement, int padding, int margin)
        {
            _positionerParms.Padding = padding;
            _positionerParms.Margin = margin;
            var shapesCount = Globals.ThisAddIn.SelectedShapes().Length;
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
                    _positionerParms.Rows = _positionerParms.Cols = ThisAddIn.BestSquare(shapesCount);
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
            var selectedShapes = Globals.ThisAddIn.SelectedShapes();
            if (selectedShapes.Length == 0) throw new InvalidOperationException("Please select one or more images.");
            if (selectedShapes.Any(s => s == null))
            {
                selectedShapes.ForEach(sh => Trace.WriteLine(sh.GetLocationString()));
                throw new InvalidOperationException("Some selected shapes are null");
            }
            var shapes = Globals.ThisAddIn.MoveAllToSamePage(selectedShapes).ReplaceSelection();
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

            using (Application.StatePreserver().FreezeScreenUpdating())
                shapes.ApplyPositions(positions);
        }

        // ReSharper disable once ParameterTypeCanBeEnumerable.Local

        static Tuple<int, int> EuristicArrangeRectangle(int shapeCount)
        {
            var fac1 = (int) Math.Floor(Math.Sqrt(shapeCount));
            var fac2 = fac1;
            while (fac1*fac2 < shapeCount) fac2++;
            return new Tuple<int, int>(fac1, fac2);
        }

        public void DoRelativePositionSelectedImages()
        {
            Globals.ThisAddIn.SelectedShapeIterator(sh =>
                {
                    sh.RelativeHorizontalPosition = Word.WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
                    sh.RelativeVerticalPosition   = Word.WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
                }
            );
        }
    }
}

