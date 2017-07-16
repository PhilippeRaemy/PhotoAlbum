namespace VstoEx.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Geometry;

    public static class RectangleExtensions
    {
        public static Rectangle LeftMost(this IEnumerable<Rectangle> r) => r.WhatMost(rr => rr.Left);
        public static Rectangle RightMost(this IEnumerable<Rectangle> r) => r.WhatMost(rr => rr.Left + rr.Width);
        public static Rectangle TopMost(this IEnumerable<Rectangle> r) => r.WhatMost(rr => rr.Top);
        public static Rectangle BottomMost(this IEnumerable<Rectangle> r) => r.WhatMost(rr => rr.Top + rr.Height);

        static Rectangle WhatMost(this IEnumerable<Rectangle> r, Func<Rectangle, float> selector)
        {
            var ra = r as Rectangle[] ?? r.ToArray();
            var left = ra.Min(selector);
            // ReSharper disable once CompareOfFloatsByEqualityOperator : the value came from one of the rectangles: we'll find an exact match
            return ra.First(rr => selector(rr) == left);
        }

    }
}