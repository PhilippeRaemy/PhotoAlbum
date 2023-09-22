namespace VstoEx.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Geometry;

    public static class RectanglesExtensions
    {
        public static IEnumerable<Rectangle> IncreaseMargin(this IEnumerable<Rectangle> rectangles, float increment)
        {
            var aRectangles = rectangles.CheapToArray();
            var oldContainer = Container(aRectangles);

            var largestDim = new[] { oldContainer.Width, oldContainer.Height }.Max();
            var newContainer = oldContainer.ScaleInPlace((largestDim + increment) / largestDim);
            return aRectangles.Select(r => r.ReFit(oldContainer, newContainer));
        }

        public static Rectangle Container(this IEnumerable<Rectangle> rectangles) 
            => rectangles.Aggregate((r1, r2) => r1.Absorb(r2));

        public static Rectangle Container(this IEnumerable<Point> points)
            => points.Select(p => p.AsRectangle()).Container();

        public static Point Center(this IEnumerable<Rectangle> rectangles)
        {
            var c = 0;
            var p = rectangles
                .Aggregate<Rectangle, Point>(null, (current, r) => ++c == 1 ? r.Center : current + r.Center);
            return p / c;
        }

        public static IEnumerable<Rectangle> IncreaseSpacing(this IEnumerable<Rectangle> rectangles, float scalePerc)
        {
            if (scalePerc <= -1)
            {
                throw new InvalidOperationException("Cannot decrease spacing of more than 100%.");
            }
            var aRectangles  = rectangles.CheapToArray();
            var oldContainer = aRectangles.Container();
            var scaled       = aRectangles.Select(r => r.ScaleInPlace(1 - scalePerc)).ToArray();
            var newContainer = scaled.Container();
            return scaled.Select(r => r.ReFit(newContainer, oldContainer));
        }

        public static float GetAverageSpacing(this IEnumerable<Rectangle> rectangles)
        {
            var rr = rectangles.CheapToArray();
            var container = rr.Container();
            return 1 - rr.Sum(r => r.Area) / container.Area;
            //if (rr.Length <= 1 ) return 0;
            //if (rr.Length == 2)
            //{
            //    var vOverlap = rr[0].VerticalSegment.Overlaps(rr[1].VerticalSegment);
            //    var hOverlap = rr[0].HorizontalSegment.Overlaps(rr[1].HorizontalSegment);
            //    if (vOverlap && !hOverlap) return rr[0].HorizontalSegment.DistanceTo(rr[1].HorizontalSegment);
            //    if (!vOverlap && hOverlap) return rr[0].VerticalSegment.DistanceTo(rr[1].VerticalSegment);
            //    return new[] 
            //    {
            //        rr[0].VerticalSegment.DistanceTo(rr[1].VerticalSegment) ,
            //        rr[0].HorizontalSegment.DistanceTo(rr[1].HorizontalSegment)
            //    }
            //    .Average();
            //}
            //throw new NotImplementedException();
        }

        public static Rectangle LeftMost  (this IEnumerable<Rectangle> r) => r.WhatMost(rr => rr.Left);
        public static Rectangle RightMost (this IEnumerable<Rectangle> r) => r.WhatMost(rr => rr.Left + rr.Width);
        public static Rectangle TopMost   (this IEnumerable<Rectangle> r) => r.WhatMost(rr => rr.Top);
        public static Rectangle BottomMost(this IEnumerable<Rectangle> r) => r.WhatMost(rr => rr.Top + rr.Height);

        static Rectangle WhatMost(this IEnumerable<Rectangle> r, Func<Rectangle, float> selector)
        {
            var ra = r.CheapToArray();
            var min = ra.Min(selector);
            // ReSharper disable once CompareOfFloatsByEqualityOperator : the value came from one of the rectangles: we'll find an exact match
            return ra.First(rr => selector(rr) == min);
        }
    }
}