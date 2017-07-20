namespace VstoEx.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
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
            return 0;
        }
    }
}