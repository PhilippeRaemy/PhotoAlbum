namespace AlbumWordAddin.Positioning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MoreLinq;
    using VstoEx.Extensions;
    using VstoEx.Geometry;

    public class NewPositioner : IPositioner
    {
        public IEnumerable<Rectangle> DoPosition(PositionerParms parms, Rectangle clientArea, IEnumerable<Rectangle> rectangles)
            => rectangles
                .DoPosition(parms.Rows, parms.Cols, parms.HShape, parms.VShape, clientArea)
                .FitInClientArea(parms.Margin, clientArea)
                .SetMinSpacing(parms.Spacing, clientArea);

    }

    static class PositionerRectanglesExtensions
    {
        public static IEnumerable<Rectangle> DoPosition(this IEnumerable<Rectangle> rectangles,
            int rows,
            int cols,
            HShape hShape,
            VShape vShape,
            Rectangle clientArea)
        {
            var shaperH = ShaperH(hShape, rows, cols);
            var shaperV = ShaperV(vShape, rows, cols);
            var rects = rectangles.CheapToArray();
            Func<int, int> rowNum = i => (i - i % cols) / cols;
            Func<int, int> colNum = i => i % cols;

            // prepare collections of groups of indexes by rows and by columns
            var byRows = Enumerable.Range(0, rects.Length).GroupBy(rowNum).ToArray();
            var byCols = Enumerable.Range(0, rects.Length).GroupBy(colNum).ToArray();

            // get the max horizontal and vertical scales by row and by column
            var hScales = byRows.Select(row => clientArea.Width  / row.Sum(r => rects[r].Width )).ToArray();
            var vScales = byCols.Select(col => clientArea.Height / col.Sum(r => rects[r].Height)).ToArray();

            // scale each input rectangle into it's final size, using minimum scaling factor
            var scaledRects = Enumerable.Range(0, rects.Length)
                .Select(i => new[] {hScales[rowNum(i)], vScales[colNum(i)]}.Min())
                .EquiZip(rects, (s, r) => r.Grow(s))
                .ToArray();

            // get the available width by row and height by column
            var availWidths = byRows.Select(row => (clientArea.Width - row.Sum(r => scaledRects[r].Width)) / row.Count()).ToArray();
            var availHeights = byCols.Select(col => (clientArea.Height - col.Sum(r => scaledRects[r].Height)) / col.Count()).ToArray();

            // distribute the available space using the shaping factors
            return Enumerable.Range(0, rects.Length)
                .Select(i => new {Row = rowNum(i), Col = colNum(i), Rect = scaledRects[i]})
                .Select(r => r.Rect.MoveTo(
                        byRows[r.Row].Take(r.Col).Sum(rr => scaledRects[rr].Width ) + availWidths [r.Row] * (r.Col  + shaperH(r.Row, r.Col)),
                        byCols[r.Col].Take(r.Row).Sum(rr => scaledRects[rr].Height) + availHeights[r.Col] * (r.Row  + shaperV(r.Row, r.Col))
                    )
                );
        }

        // ReSharper disable once UnusedParameter.Local :  for consistency with ShaperH and future usage
        static Func<int, int, float> ShaperH(HShape hShape, int rows, int cols)
        {
            switch (hShape)
            {
                case HShape.Flat: return (_, __) => 0.5F;
                case HShape.Left: return (_, __) => 0F;
                case HShape.Right: return (_, __) => 1F;
                case HShape.RightDown: if (rows <= 1) return (_, __) => 0.5F; return (r, _) => r / ((float)rows - 1);
                case HShape.RightUp: if (rows <= 1) return (_, __) => 0.5F; return (r, _) => 1 - r / ((float)rows - 1);
                case HShape.BendLeft:
                    if (rows <= 2) return (_, __) => 0F;
                    if (rows % 2 == 1) return (r, _) => 1 - Math.Abs(2 * r / ((float)rows - 1) - 1);
                    return (r, _) => r < rows / 2
                        ? 2 * r / ((float)rows - 2)
                        : 2 * (rows - r - 1) / ((float)rows - 2);
                case HShape.BendRight:
                    if (rows <= 2) return (_, __) => 1F;
                    if (rows % 2 == 1) return (r, _) => Math.Abs(2 * r / ((float)rows - 1) - 1);
                    return (r, _) => r < rows / 2
                        ? 1 - 2 * r / ((float)rows - 2)
                        : 1 - 2 * (rows - r - 1) / ((float)rows - 2);
                default:
                    throw new NotImplementedException($"Invalid ShaperH value {hShape}");
            }
        }

        // ReSharper disable once UnusedParameter.Local :  for consistency with ShaperH and future usage
        static Func<int, int, float> ShaperV(VShape vShape, int rows, int cols)
        {
            switch (vShape)
            {
                case VShape.Flat: return (_, __) => 0.5F;
                case VShape.Top: return (_, __) => 0F;
                case VShape.Bottom: return (_, __) => 1F;
                case VShape.Rightdown: if (cols <= 1) return (_, __) => 0.5F; return (_, c) => c / ((float)cols - 1);
                case VShape.Rightup: if (cols <= 1) return (_, __) => 0.5F; return (_, c) => 1 - c / ((float)cols - 1);
                case VShape.Benddown:
                    if (cols <= 2) return (_, __) => 0F;
                    if (cols % 2 == 1) return (_, c) => 1 - Math.Abs(2 * c / ((float)cols - 1) - 1);
                    return (_, c) => c < cols / 2
                        ? 2 * c / ((float)cols - 2)
                        : 2 * (cols - c - 1) / ((float)cols - 2);
                case VShape.Bendup:
                    if (cols <= 2) return (_, __) => 1F;
                    if (cols % 2 == 1) return (_, c) => Math.Abs(2 * c / ((float)cols - 1) - 1);
                    return (_, c) => c < cols / 2
                        ? 1 - 2 * c / ((float)cols - 2)
                        : 1 - 2 * (cols - c - 1) / ((float)cols - 2);
                default:
                    throw new NotImplementedException($"Invalid ShaperV value {vShape}");
            }
        }

        /// <summary>
        /// Proportionally resize each rectangle of the enumeration to fit in a target client area
        /// </summary>
        /// <param name="rectangles"></param>
        /// <param name="margin"></param>
        /// <param name="clientArea"></param>
        /// <returns></returns>
        public static IEnumerable<Rectangle> FitInClientArea(this IEnumerable<Rectangle> rectangles, float margin, Rectangle clientArea)
        {
            var rects = rectangles.CheapToArray();
            var container = rects.Container().CenterOn(clientArea);
            var scale = new[]
            {
                (clientArea.Width - 2 * margin) / container.Width,
                (clientArea.Height - 2 * margin) / container.Height
            }.Min();
            container = container.LinearScale(scale, scale);
            return rects
                .Select(r => r
                    .MoveBy(-container.Left, -container.Top)
                    .LinearScale(scale, scale)
                    .MoveBy(
                        margin + (clientArea.Width - container.Width) / 2,
                        margin + (clientArea.Height - container.Height) / 2)
                );
        }

        public static IEnumerable<Rectangle> SetMinSpacing(this IEnumerable<Rectangle> rectangles, float spacing, Rectangle clientArea)
        {
            /* (1) Determine the minimum space between any of hte rectangles enumeration
             * (2) from the % of that space in the client rectangle determine the % of the desired spacing in the client area
             * (3) shrink the rectangles by teh appropriate scaling factor to create missing space
             * (4) stretch the rectangles to the whole client area by moving them BUT NOT growing
             *      this is achieved by growing the constellation of rectangle centers
             */
            return rectangles;
        }

        public static float GetMinSpacing(this IEnumerable<Rectangle> rectangles)
        {
            var rr = rectangles.CheapToArray();
            var tuples = Enumerable.Range(0, rr.Length)
                .SelectMany(i => Enumerable.Range(i + 1, rr.Length - i - 1).Select(j => (i, j)))
                .ToArray();
            return new[]
            {
                tuples.Min(t => rr[t.i].HorizontalDistanceTo(rr[t.j])),
                tuples.Min(t => rr[t.i].VerticalDistanceTo(rr[t.j]))
            }.Min();
        }
    }
}