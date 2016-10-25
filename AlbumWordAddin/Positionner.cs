using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MoreLinq;

namespace AlbumWordAddin
{
    public enum VShape
    {
        Flat, Top, Bottom, Rightdown, Rightup, Bendright, Bendleft
    }
    public enum HShape
    {
        Flat, Left, Right, Rightdown, Rightup, Bendup, Benddown
    }
    public class Positioner
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public HShape HShape { get; set; }
        public VShape VShape { get; set; }
        public int Margin { get; set; }
        public int Padding { get; set; }
        public IEnumerable<Rectangle> DoPosition(Rectangle clientArea, IEnumerable<Rectangle> rectangles)
        {
            return from rectangle in DoPosition(rectangles)
                   select rectangle.Scale(clientArea.Width, clientArea.Height);
        }
        public IEnumerable<Rectangle> DoPosition(IEnumerable<Rectangle> rectangles)
        {
            return DoPosition(rectangles, HShape, VShape);
        }
        public IEnumerable<Rectangle> DoPosition(IEnumerable<Rectangle> rectangles, HShape hShape, VShape vShape)
        {
            var scaleX = 1f / Cols;
            var scaleY = 1f / Rows;
            var shaperH = ShaperH(hShape, Rows, Cols);
            var shaperV = ShaperV(vShape, Rows, Cols);
            var grid = Enumerable.Range(0, Rows)
                .SelectMany(r => Enumerable.Range(0, Cols)
                    .Select(c => new {
                        area = new Rectangle(c * scaleX, r * scaleY, scaleX, scaleY),
                        hShape = shaperH(r, c),
                        vShape = shaperV(r, c),
                    }));
            return grid
                .ZipLongest(rectangles, (area, rectangle) => new { area, rectangle })
                .Where(x => x.rectangle != null && x.area != null)
                .Select(x=> x.rectangle.FitIn(x.area.area, x.area.hShape, x.area.vShape))
            ;
        }
        // ReSharper disable once UnusedParameter.Local :  for consistency with ShaperH and future usage
        [SuppressMessage("ReSharper", "RedundantCaseLabel")]
        static Func<int, int, float> ShaperH(HShape hShape, int rows, int cols)
        {
            switch (hShape)
            {
                case HShape.Flat     : return (_, __) => 0.5F;
                case HShape.Left     : return (_, __) => 0F;
                case HShape.Right    : return (_, __) => 1F;
                case HShape.Rightdown: return (_, c) => 1 - c / ((float)cols - 1);
                case HShape.Rightup  : return (_, c) => c / ((float)cols - 1);
                case HShape.Benddown : 
                case HShape.Bendup   :
                default:
                    throw new NotImplementedException($"Invalid ShaperH value {hShape}");
            }
        }
        // ReSharper disable once UnusedParameter.Local :  for consistency with ShaperH and future usage
        [SuppressMessage("ReSharper", "RedundantCaseLabel")]
        static Func<int, int, float> ShaperV(VShape vShape, int rows, int cols)
        {
            switch (vShape)
            {
                case VShape.Flat     : return (_, __) => 0.5F;
                case VShape.Top      : return (_, __) => 0F;
                case VShape.Bottom   : return (_, __) => 1F;
                case VShape.Rightdown: return (r, _) => 1 - r / ((float)rows - 1);
                case VShape.Rightup  : return (r, _) => r / ((float)rows - 1);
                case VShape.Bendleft:
                case VShape.Bendright:
                default:
                    throw new NotImplementedException($"Invalid ShaperV value {vShape}");
            }
        }
    }

    public class Rectangle
    {
        public float Left   { get; }
        public float Top    { get; }
        public float Width  { get; }
        public float Height { get; }
        const float Epsilon = .0000001f;

        public Rectangle(float left, float top, float width, float height)
        {
            if (width < float.Epsilon) throw new InvalidOperationException("Rectangle cannot have negative or zero width.");
            if (height < float.Epsilon) throw new InvalidOperationException("Rectangle cannot have negative or zero height.");
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        public Rectangle Move(float x, float y)
           => new Rectangle(Left + x, Top + y, Width, Height);

        public Rectangle Grow(float g)
           => new Rectangle(Left, Top, Width * g, Height * g);

        public Rectangle Grow(float w, float h)
           => new Rectangle(Left, Top, Width * w, Height * h);

        public Rectangle Scale(float scaleX, float scaleY)
           => new Rectangle(Left * scaleX, Top * scaleY, Width * scaleX, Height * scaleY);

        public Rectangle FitIn(Rectangle other, float padTopPerc = 0.5f, float padLeftPerc = 0.5f) {
            var scale = new[] { other.Width / Width, other.Height / Height }.Min();
            var newWidth  = Width * scale;
            var newHeight = Height * scale;
            return new Rectangle(
                other.Left + (other.Width - newWidth) * padLeftPerc,
                other.Top + (other.Height - newHeight) * padTopPerc,
                newWidth,
                newHeight
            );
        }

        public override string ToString()
            => $"[{Left}..{Left + Width},{Top}..{Top + Height}, ({Width}x{Height})";

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
}
