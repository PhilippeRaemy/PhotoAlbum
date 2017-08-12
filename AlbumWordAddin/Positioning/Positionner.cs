namespace AlbumWordAddin.Positioning
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using MoreLinq;
    using VstoEx.Extensions;
    using VstoEx.Geometry;

    public static class Positioner
    {
        public class Parms
        {
            public int Rows         { get; set; }
            public int Cols         { get; set; }
            public HShape HShape    { get; set; }
            public VShape VShape    { get; set; }
            public float Margin     { get; set; }
            public float Spacing    { get; set; }
        }

        public static IEnumerable<Rectangle> DoPosition(Parms parms, Rectangle clientArea, IEnumerable<Rectangle> rectangles)
        {
            return DoPosition(
                parms.Rows,
                parms.Cols,
                parms.HShape,
                parms.VShape,
                parms.Margin,
                parms.Spacing,
                clientArea,
                rectangles
            );
        }

        static IEnumerable<Rectangle> DoPosition(
            int rows,
            int cols,
            HShape hShape,
            VShape vShape,
            float margin,
            float spacing,
            Rectangle clientArea, 
            IEnumerable<Rectangle> rectangles
        )
        {
            Debug.Assert(spacing >- 1 && spacing < 1, "Spacing should be in range ]-1;1[");    
            var scaleX = (clientArea.Width - 2 * margin) / cols;
            var scaleY = (clientArea.Height - 2 * margin) / rows;
            var shaperH = ShaperH(hShape, rows, cols);
            var shaperV = ShaperV(vShape, rows, cols);
            var grid = Enumerable.Range(0, rows)
                .SelectMany(r => Enumerable.Range(0, cols)
                    .Select(c => new
                        {
                            area   = new Rectangle(margin + c*scaleX, margin + r*scaleY, scaleX, scaleY),
                            hShape = shaperH(r, c),
                            vShape = shaperV(r, c),
                        }
                    )
                );
            var draft = grid
                .ZipLongest(rectangles, (area, rectangle) => new { area, rectangle})
                .Where(x => x.rectangle != null && x.area != null)
                .Select(x => x.rectangle.FitIn(x.area.area, x.area.hShape, x.area.vShape, 0))
                .ToArray();
            if (spacing == 0) return draft;
            var y0 = draft.GetAverageSpacing();
            var delta = Math.Sign(spacing) * .05f;
            var candidate = draft;
            for (var i=0; i<10; i++)
            {
                candidate = draft.IncreaseSpacing(delta).CheapToArray();
                var y1 = candidate.GetAverageSpacing();
                Trace.WriteLine($"spacing:{spacing}, y0:{y0}, y1:{y1}, delta:{delta}.");
                if (Math.Abs(y1 - spacing) < 0.01f)
                {
                    return candidate;
                }
                var dy = (y1 - y0) / delta;
                delta = (spacing - y0) / dy;
            }
            return candidate;
        }

        // ReSharper disable once UnusedParameter.Local :  for consistency with ShaperH and future usage
        static Func<int, int, float> ShaperH(HShape hShape, int rows, int cols)
        {
            switch (hShape)
            {
                case HShape.Flat     : return (_, __) => 0.5F;
                case HShape.Left     : return (_, __) => 0F;
                case HShape.Right    : return (_, __) => 1F;
                case HShape.Rightdown: if (rows <= 1) return (_, __) => 0.5F; return (r, _) => r / ((float)rows - 1);
                case HShape.Rightup  : if (rows <= 1) return (_, __) => 0.5F; return (r, _) => 1 - r / ((float)rows - 1);
                case HShape.BendLeft :
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
                case VShape.Flat     : return (_, __) => 0.5F;
                case VShape.Top      : return (_, __) => 0F;
                case VShape.Bottom   : return (_, __) => 1F;
                case VShape.Rightdown: if (cols <= 1) return (_, __) => 0.5F; return (_, c) => c / ((float)cols - 1);
                case VShape.Rightup  : if (cols <= 1) return (_, __) => 0.5F; return (_, c) => 1 - c / ((float)cols - 1);
                case VShape.Benddown :
                    if (cols <= 2) return (_, __) => 0F;
                    if (cols % 2 == 1) return (_, c) => 1 - Math.Abs(2 * c / ((float)cols - 1) - 1);
                    return (_, c) => c < cols / 2
                        ? 2 * c / ((float)cols - 2)
                        : 2 * (cols-c-1) / ((float)cols - 2);
                case VShape.Bendup   :
                    if (cols <= 2) return (_, __) => 1F;
                    if (cols % 2 == 1) return (_, c) => Math.Abs(2 * c / ((float)cols - 1) - 1);
                    return (_, c) => c < cols / 2
                        ? 1 - 2 * c / ((float)cols - 2)
                        : 1 - 2 * (cols - c - 1) / ((float)cols - 2);
                default:
                    throw new NotImplementedException($"Invalid ShaperV value {vShape}");
            }
        }
    }
}