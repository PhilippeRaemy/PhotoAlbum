// ReSharper disable LocalizableElement

namespace VstoEx.Geometry
{
    using System;
    using System.Linq;
    using Microsoft.Office.Interop.Word;

    public class Rectangle
    {
        public float Left   { get; }
        public float Top    { get; }
        public float Width  { get; }
        public float Height { get; }

        public float Right => Left + Width;
        public float Bottom => Top + Height;
        public float Area => Width * Height;

        public Point TopLeft     => new Point(Left , Top);
        public Point TopRight    => new Point(Right, Top);
        public Point BottomLeft  => new Point(Left , Bottom);
        public Point BottomRight => new Point(Right, Bottom);
        public Point Center      => new Point(Left + Width / 2, Top + Height / 2);

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

        public Rectangle(Point center, float width, float height)
            : this(center.X - width / 2, center.Y - height / 2, width, height)
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

        /// <summary>
        /// Scale a rectangle by a given scaling factor, preserving the location of its center.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public Rectangle ScaleInPlace(float scale)
            => new Rectangle(Center, scale * Width, scale * Height);

        public Rectangle FitIn(Rectangle other, float fitLeftPerc, float fitTopPerc, float spacing) {
            if (Math.Abs(spacing) > Epsilon)
            {
                other=new Rectangle(other.Left + spacing, other.Top + spacing, other.Width - 2 * spacing, other.Height - 2 * spacing);   
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

        /// <summary>
        /// Refits the Rectangle from an original container into a new one, preserving aspect ratio
        /// </summary>
        /// <param name="originalFit"></param>
        /// <param name="newFit"></param>
        /// <returns></returns>
        public Rectangle ReFit(Rectangle originalFit, Rectangle newFit)
        {
            if (originalFit == null) throw new ArgumentNullException(nameof(originalFit));
            if (originalFit.Width  < float.Epsilon) throw new ArgumentOutOfRangeException(nameof(originalFit), "Rectangle has zero width" );
            if (originalFit.Height < float.Epsilon) throw new ArgumentOutOfRangeException(nameof(originalFit), "Rectangle has zero height");
            if (newFit == null) throw new ArgumentNullException(nameof(newFit));
            var factor = (newFit.Width / originalFit.Width + newFit.Height / originalFit.Height) / 2;
            var center = new Point(newFit.Left + (Center.X - originalFit.Left) / originalFit.Width * newFit.Width,
                newFit.Top  + (Center.Y - originalFit.Top ) / originalFit.Height * newFit.Height);
            return new Rectangle(center, Width * factor, Height * factor);
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

        public float AverageDistance(Rectangle other)
        {
            return new[]
            {
                Top - other.Top,
                other.Right - Right,
                other.Bottom - Bottom,
                Left - other.Left
            }.Average();
        }
    }
}