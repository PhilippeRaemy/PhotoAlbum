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
}
