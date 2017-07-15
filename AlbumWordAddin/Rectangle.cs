using System;
using System.Linq;

namespace AlbumWordAddin
{
    using System.Collections.Generic;
    using Microsoft.Office.Interop.Word;

    public class Point
    {
        public float Y { get; set; }
        public float X { get; set; }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    public class Segment
    {
        public float Start { get; }
        public float End { get; }

        public Segment(float start, float end)
        {
            if (end < start + float.Epsilon) throw new InvalidOperationException("Segment cannot have negative or zero length.");
            Start = start;
            End = end;
        }
        
        public float DistanceTo(Segment other)
            => other.Start >= End ? other.Start - End
             : End >= other.Start ? End - other.Start
             : -1;
    }

    public class Rectangle
    {
        public float Left   { get; }
        public float Top    { get; }
        public float Width  { get; }
        public float Height { get; }

        public float Right => Left + Width;
        public float Bottom => Top + Height;

        public Point TopLeft     => new Point(Left , Top);
        public Point TopRight    => new Point(Right, Top);
        public Point BottomLeft  => new Point(Left , Bottom);
        public Point BottomRight => new Point(Right, Bottom);

        public Segment HorizontalSegment => new Segment(Left, Right );
        public Segment VerticalSegment   => new Segment(Top , Bottom);

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

        public Rectangle(Point topLeft, Point bottomRight)
            : this(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y)
        {
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

        public Rectangle ScaleInPlace(float scale)
         => new Rectangle(Left + (1 - scale) * Width  / 2,
                          Top  + (1 - scale) * Height / 2,
                          (1 - scale) * Width ,
                          (1 - scale) * Height);

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

        public Rectangle Absorb(Rectangle other)
        {

            return new Rectangle(
                new Point(
                    new[] { Left  , other.Left   }.Min(),
                    new[] { Top   , other.Top    }.Min()
                ),
                new Point(
                    new[] { Right , other.Right  }.Max(),
                    new[] { Bottom, other.Bottom }.Max()
                )
            );
        }

        public Rectangle ReFit(Rectangle originalFit, Rectangle newFit)
        {
            if (originalFit == null) throw new ArgumentNullException(nameof(originalFit));
            if (originalFit.Width  < float.Epsilon) throw new ArgumentOutOfRangeException(nameof(originalFit), "Rectangle has zero width" );
            if (originalFit.Height < float.Epsilon) throw new ArgumentOutOfRangeException(nameof(originalFit), "Rectangle has zero height");
            if (newFit == null) throw new ArgumentNullException(nameof(newFit));
            return new Rectangle(
                newFit.Left + (Left - originalFit.Left)/originalFit.Width,
                newFit.Top + (Top - originalFit.Top)/originalFit.Height,
                Width/originalFit.Width*newFit.Width,
                Height/originalFit.Height*newFit.Height
            );
        }

        public bool Contains(Rectangle other)
            => Left <= other.Left && Right  >= other.Right
            && Top  <= other.Top  && Bottom >= other.Bottom;

        public bool IsContainedIn(Rectangle other)
            => other.Contains(this);

        public float HorizontalDistanceTo(Rectangle other)
            => HorizontalSegment.DistanceTo(other.HorizontalSegment);

        public float VerticalDistanceTo(Rectangle other)
            => VerticalSegment.DistanceTo(other.VerticalSegment);

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

        static Rectangle WhatMost(this IEnumerable<Rectangle> r, Func<Rectangle,float> selector )
        {
            var ra = r as Rectangle[] ?? r.ToArray();
            var left = ra.Min(selector);
            // ReSharper disable once CompareOfFloatsByEqualityOperator : the value came from one of the rectangles: we'll find an exact match
            return ra.First(rr => selector(rr) == left);
        }

        static IEnumerable<Rectangle> IncreaseMargin(this IEnumerable<Rectangle> rectangles, float increment)
        {
            var aRectangles = rectangles as Rectangle[] ?? rectangles.ToArray();
            var oldContainer = aRectangles.Aggregate((r1, r2) => r1.Absorb(r2));

            var largestDim = new[] {oldContainer.Width, oldContainer.Height}.Max();
            var newContainer = oldContainer.ScaleInPlace((largestDim + increment)/largestDim);
            return aRectangles.Select(r => r.ReFit(oldContainer, newContainer));
        }

        static IEnumerable<Rectangle> IncreasePadding(this IEnumerable<Rectangle> rectangles, float scale)
        {
            var aRectangles  = rectangles as Rectangle[] ?? rectangles.ToArray();
            var oldContainer = aRectangles.Aggregate((r1, r2) => r1.Absorb(r2));
            var scaled       = aRectangles.Select(r => r.ScaleInPlace(scale)).ToArray();
            var newContainer = scaled.Aggregate((r1, r2) => r1.Absorb(r2));
            return scaled.Select(r => r.ReFit(newContainer, oldContainer));
        }
    }
}