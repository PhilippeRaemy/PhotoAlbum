namespace VstoEx.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Geometry;

    public static class RectanglesExtensions
    {
        public static IEnumerable<Rectangle> IncreaseMargin(this IEnumerable<Rectangle> rectangles, float increment)
        {
            var aRectangles = rectangles as Rectangle[] ?? rectangles.ToArray();
            var oldContainer = aRectangles.Aggregate((r1, r2) => r1.Absorb(r2));

            var largestDim = new[] {oldContainer.Width, oldContainer.Height}.Max();
            var newContainer = oldContainer.ScaleInPlace((largestDim + increment)/largestDim);
            return aRectangles.Select(r => r.ReFit(oldContainer, newContainer));
        }

        public static IEnumerable<Rectangle> IncreasePadding(this IEnumerable<Rectangle> rectangles, float scale)
        {
            var aRectangles  = rectangles as Rectangle[] ?? rectangles.ToArray();
            var oldContainer = aRectangles.Aggregate((r1, r2) => r1.Absorb(r2));
            var scaled       = aRectangles.Select(r => r.ScaleInPlace(scale)).ToArray();
            var newContainer = scaled.Aggregate((r1, r2) => r1.Absorb(r2));
            return scaled.Select(r => r.ReFit(newContainer, oldContainer));
        }

        public static float GetAveragePadding(this IEnumerable<Rectangle> rectangles)
        {
            var rr = rectangles as Rectangle[] ?? rectangles.ToArray();
            if (rr.Length <= 1 ) return 0;
            if (rr.Length == 2)
            {
                var vOverlap = rr[0].VerticalSegment.Overlaps(rr[1].VerticalSegment);
                var hOverlap = rr[0].HorizontalSegment.Overlaps(rr[1].HorizontalSegment);
                if (vOverlap && !hOverlap) return rr[0].HorizontalSegment.DistanceTo(rr[1].HorizontalSegment);
                if (!vOverlap && hOverlap) return rr[0].VerticalSegment.DistanceTo(rr[1].VerticalSegment);
                return new[] 
                {
                    rr[0].VerticalSegment.DistanceTo(rr[1].VerticalSegment) ,
                    rr[0].HorizontalSegment.DistanceTo(rr[1].HorizontalSegment)
                }
                .Average();
            }
            ;
            return float.NaN;
        }
    }
}