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
                .FitInContainer(parms.Margin, clientArea)
                .SetMinSpacing(parms.Spacing);

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
        /// <param name="targetContainer"></param>
        /// <returns></returns>
        public static IEnumerable<Rectangle> FitInContainer(this IEnumerable<Rectangle> rectangles, float margin, Rectangle targetContainer)
        {
            var rects = rectangles.CheapToArray();
            var container = rects.Container().CenterOn(targetContainer);
            var scale = new[]
            {
                (targetContainer.Width - 2 * margin) / container.Width,
                (targetContainer.Height - 2 * margin) / container.Height
            }.Min();
            container = container.LinearScale(scale, scale);
            return rects
                .Select(r => r
                    .MoveBy(-container.Left, -container.Top)
                    .LinearScale(scale, scale)
                    .MoveBy(
                        margin + (targetContainer.Width - container.Width) / 2,
                        margin + (targetContainer.Height - container.Height) / 2)
                );
        }

        public static IEnumerable<Rectangle> SetMinSpacing(this IEnumerable<Rectangle> rectangles, float spacing)
        {
            /* (1) Determine the minimum space between any of hte rectangles enumeration
             * (2) from the % of that space in the client rectangle determine the % of the desired spacing in the client area
             * (3) shrink the rectangles by teh appropriate scaling factor to create missing space
             * (4) stretch the rectangles to the whole client area by moving them BUT NOT growing
             *      this is achieved by growing the constellation of rectangle centers
             */
            var rects = rectangles.CheapToArray();
            var container = rects.Container();
            var (hMinSpace, vMinSpace) = rects.GetMinSpacing(); /* (1) */
            var shrinkFactor = hMinSpace < vMinSpace  /* (2) */
                ? (1 - spacing / container.Width) / (1 - hMinSpace / container.Width)
                : (1 - spacing / container.Height) / (1 - vMinSpace / container.Height);
            return rects.Select(r => r.ScaleInPlace(shrinkFactor)) /* (3) */
                .StretchToContainer(container) /* (4) */;
        }

        public static IEnumerable<Rectangle> StretchToContainer(this IEnumerable<Rectangle> rectangles, Rectangle targetContainer)
        {
            var rects = rectangles.CheapToArray(); 
            var container = rects.Container();

            var centerContainer = rects.Select(r => r.Center).Container();
            var targetCenterContainer = new Rectangle(
                targetContainer.Left + rects.LeftMost().Width / 2,
                targetContainer.Top + rects.TopMost().Height /2,
                targetContainer.Width - rects.RightMost().Width / 2,
                targetContainer.Height - rects.BottomMost().Height /2
                );
            var center = targetCenterContainer.Center;
            var scaleX = targetCenterContainer.Width / centerContainer.Width;
            var scaleY = targetCenterContainer.Height / centerContainer.Height;
            return rects.Select(r => r.MoveBy(center - centerContainer.Center))
                    .Select(r => (r.Center, r))
                    .Select(r => r.r.CenterOn(new Point((center.X - r.Center.X) * scaleX, (center.Y - r.Center.Y) * scaleY)));
        }

        static (float horizontal, float vertical) GetMinSpacing(this Rectangle[] rectangles)
        {
            var tuples = Enumerable.Range(0, rectangles.Length)
                .SelectMany(i => Enumerable.Range(i + 1, rectangles.Length - i - 1).Select(j => (i, j)))
                .ToArray();
            return (
                tuples.Min(t => rectangles[t.i].HorizontalDistanceTo(rectangles[t.j])),
                tuples.Min(t => rectangles[t.i].VerticalDistanceTo(rectangles[t.j]))
            );
        }
    }
}