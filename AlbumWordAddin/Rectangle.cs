using System;
using System.Linq;

namespace AlbumWordAddin
{
    using System.Collections.Generic;
    using Microsoft.Office.Interop.Word;

    public class Rectangle
    {
        public float Left   { get; }
        public float Top    { get; }
        public float Width  { get; }
        public float Height { get; }
        const float Epsilon = .000001f;

        public Rectangle(float left, float top, float width, float height)
        {
            if (width < float.Epsilon) throw new InvalidOperationException("Rectangle cannot have negative or zero width.");
            if (height < float.Epsilon) throw new InvalidOperationException("Rectangle cannot have negative or zero height.");
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        public Rectangle(Shape s) : this(s.Left, s.Top, s.Width, s.Height){}

        public Rectangle MoveBy(float x, float y)
            => new Rectangle(Left + x, Top + y, Width, Height);

        public Rectangle MoveTo(float x, float y)
            => new Rectangle(x, y, Width, Height);

        public Rectangle Grow(float g)
            => Grow(g, g);

        public Rectangle Grow(float w, float h)
            => new Rectangle(Left, Top, Width * w, Height * h);

        public Rectangle Scale(float scale)
            => Scale(scale, scale);

        public Rectangle Scale(float scaleX, float scaleY)
            => new Rectangle(Left * scaleX, Top * scaleY, Width * scaleX, Height * scaleY);

        public Rectangle FitIn(Rectangle other, float fitLeftPerc, float fitTopPerc, float padding) {
            if (Math.Abs(padding) > Epsilon)
            {
                other=new Rectangle(other.Left + padding, other.Top + padding, other.Width - 2 * padding, other.Height - 2 * padding); ;   
            }
            var scale = new[] { other.Width / Width, other.Height / Height }.Min();
            var newWidth  = Width * scale;
            var newHeight = Height * scale;
            return new Rectangle(
                other.Left + (other.Width - newWidth) * fitLeftPerc,
                other.Top + (other.Height - newHeight) * fitTopPerc,
                newWidth,
                newHeight
            );
        }

        public override string ToString()
            => $"[{Left},{Top}]..[{Left + Width},{Top + Height}] ({Width}x{Height})";

        public override bool Equals(object obj)
        {
            var other = obj as Rectangle;
            if (other == null) return false;
            return Math.Abs(Left   - other.Left  ) < Epsilon
                   && Math.Abs(Top    - other.Top   ) < Epsilon
                   && Math.Abs(Width  - other.Width ) < Epsilon
                   && Math.Abs(Height - other.Height) < Epsilon
                ;
        }
        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }
    }

    public static class RectangleExtensions
    {
        public static Rectangle LeftMost  (this IEnumerable<Rectangle> r) => r.WhatMost(rr => rr.Left);
        public static Rectangle RightMost (this IEnumerable<Rectangle> r) => r.WhatMost(rr => rr.Left + rr.Width);
        public static Rectangle TopMost   (this IEnumerable<Rectangle> r) => r.WhatMost(rr => rr.Top);
        public static Rectangle BottomMost(this IEnumerable<Rectangle> r) => r.WhatMost(rr => rr.Top + rr.Height);

        public static float PaddingV(this IEnumerable<Rectangle> r) => 0;
        public static float PaddingH(this IEnumerable<Rectangle> r) => 0;
        public static float Padding (this IEnumerable<Rectangle> r)
        {
            var a = r as Rectangle[] ?? r.ToArray();
            var paddings = new[] {a.PaddingH(), a.PaddingV()}.Where(p => p >= 0).ToArray();
            return paddings.Any() ? paddings.Average() : -1;
        }

        static Rectangle WhatMost(this IEnumerable<Rectangle> r, Func<Rectangle,float> selector )
        {
            var ra = r as Rectangle[] ?? r.ToArray();
            var left = ra.Min(selector);
            // ReSharper disable once CompareOfFloatsByEqualityOperator : the value came from one of the rectangles: we'll find an exact match
            return ra.First(rr => selector(rr) == left);
        }
    }
}