using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumWordAddin
{
    public enum VShape
    {
        VERTICAL, RIGHTDOWN, RIGHTUP, BENDRIGHT, BENDLEFT
    }
    public enum HShape
    {
        HORIZONTAL, RIGHTDOWN, RIGHTUP, BENDUP, BENDDOWN
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
            return rectangles;
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
           => new Rectangle(Left + x, Width, Top + y, Height);
        public Rectangle Grow(float g)
           => new Rectangle(Left, Width * g, Top, Height * g);
        public Rectangle Scale(float scaleX, float scaleY)
           => new Rectangle(Left * scaleX, Width * scaleX, Top * scaleY, Height * scaleY);

    }
}
