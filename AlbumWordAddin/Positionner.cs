using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace AlbumWordAddin
{
    public enum VShape
    {
        FLAT, TOP, BOTTOM, RIGHTDOWN, RIGHTUP, BENDRIGHT, BENDLEFT
    }
    public enum HShape
    {
        FLAT, LEFT, RIGHT, RIGHTDOWN, RIGHTUP, BENDUP, BENDDOWN
    }
    public class Positioner
    {
        public int rows { get; set; }
        public int cols { get; set; }
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
        public IEnumerable<Rectangle> DoPosition(IEnumerable<Rectangle> rectangles, HShape HShape, VShape VShape)
        {
            var scaleX = 1f / cols;
            var scaleY = 1f / rows;
            var shaperH = ShaperH(HShape, rows, cols);
            var shaperV = ShaperV(VShape, rows, cols);
            var grid = Enumerable.Range(0, rows)
                .SelectMany(r => Enumerable.Range(0, cols)
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
        static Func<int, int, float> ShaperH(HShape hShape, int rows, int cols)
        {
            switch (hShape)
            {
                case HShape.FLAT     : return (_, __) => 0.5F;
                case HShape.LEFT     : return (_, __) => 0F;
                case HShape.RIGHT    : return (_, __) => 1F;
                case HShape.RIGHTDOWN: return (_, c) => 1 - (c / (cols - 1));
                case HShape.RIGHTUP  : return (_, c) => (c / (cols - 1));
                case HShape.BENDDOWN : 
                case HShape.BENDUP   :
                default:
                    throw new NotImplementedException($"Invalid ShaperH value {hShape}");
            }
        }
        static Func<int, int, float> ShaperV(VShape vShape, int rows, int cols)
        {
            switch (vShape)
            {
                case VShape.FLAT     : return (_, __) => 0.5F;
                case VShape.TOP      : return (_, __) => 0F;
                case VShape.BOTTOM   : return (_, __) => 1F;
                case VShape.RIGHTDOWN: return (r, _) => 1 - (r / (rows - 1));
                case VShape.RIGHTUP  : return (r, _) => (r / (rows - 1));
                case VShape.BENDLEFT:
                case VShape.BENDRIGHT:
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
            if (this == null) return false;
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
            return base.GetHashCode();
        }
    }
}
