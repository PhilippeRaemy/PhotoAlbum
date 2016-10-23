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
        FLAT, RIGHTDOWN, RIGHTUP, BENDRIGHT, BENDLEFT
    }
    public enum HShape
    {
        FLAT, RIGHTDOWN, RIGHTUP, BENDUP, BENDDOWN
    }
    public class Positionner
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
            return DoPosition(rectangles, HShape.FLAT, VShape.FLAT);
        }
        public IEnumerable<Rectangle> DoPosition(IEnumerable<Rectangle> rectangles, HShape HShape, VShape VShape)
        {
            var scaleX = 1f / cols;
            var scaleY = 1f / rows;
            return Enumerable.Range(0, rows)
                .SelectMany(r => Enumerable.Range(0, cols).Select(c => new Rectangle( r* scaleY, c* scaleX, scaleY, scaleX)))
                .ZipLongest(rectangles, (rectangle, area) => new { rectangle, area })
                .Where(x => x.rectangle != null && x.area != null)
                .Select(x=> x.rectangle.FitIn(x.area, .5f, .5f))
            ;
        }
        static Func<int, int, float> ShaperH(HShape hShape, int rows, int cols)
        {
            switch (hShape)
            {
                case HShape.FLAT: return (_, __) => 0.5F;
                case HShape.RIGHTDOWN:
                case HShape.RIGHTUP:
                case HShape.BENDDOWN:
                case HShape.BENDUP:
                default:
                    throw new NotImplementedException($"Invalid ShaperH value {hShape}");
            }
        }
        static Func<int, int, float> ShaperV(VShape vShape, int rows, int cols)
        {
            switch (vShape)
            {
                case VShape.FLAT: return (_, __) => 0.5F;
                case VShape.RIGHTDOWN:
                case VShape.RIGHTUP:
                case VShape.BENDLEFT:
                case VShape.BENDRIGHT:
                default:
                    throw new NotImplementedException($"Invalid ShaperV value {vShape}");
            }
        }
    }


    public class Rectangle
    {
        public float Left { get; }
        public float Top { get; }
        public float Width { get; }
        public float Height { get; }

        public Rectangle(float left, float top, float width, float height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        public Rectangle Move(float x, float y)
           => new Rectangle(Left + x, Top + y, Width, Height);

        public Rectangle Grow(float g)
           => new Rectangle(Left, Top, Width * g, Height * g);

        public Rectangle Scale(float scaleX, float scaleY)
           => new Rectangle(Left * scaleX, Top * scaleY, Width * scaleX, Height * scaleY);

        public Rectangle FitIn(Rectangle other, float padTopPerc = 0.5f, float padLeftPerc = 0.5f) {
            var factor = new[] { other.Width / Width, other.Height / Height }.Min();
            var newWidth  = Width * factor;
            var newHeight = Height * factor;
            return new Rectangle(
                other.Left + (other.Width - newWidth) * padLeftPerc,
                other.Top + (other.Height - newHeight) * padTopPerc,
                newWidth,
                newHeight
            );
        }
    }
}
