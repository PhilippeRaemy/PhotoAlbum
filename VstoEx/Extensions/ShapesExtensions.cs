namespace AlbumWordAddin.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Office.Interop.Word;
    using MoreLinq;
    using VstoEx;
    using Rectangle = VstoEx.Geometry.Rectangle;

    public static class ShapesExtensions
    {
        public static IEnumerable<Rectangle> ToRectangles(this IEnumerable<Shape> shapes)
            => shapes.Select(s => new Rectangle(s));

        public static void ApplyPositions(this IEnumerable<Shape> shapes, StatePreserver statePreserver, IEnumerable<Rectangle> positions)
        {
            foreach (var pos in shapes.ZipLongest(positions, (sh, re) => new { sh, re })
                .Where(r => r.re != null && r.sh != null)
            )
            {
                pos.sh.Left = pos.re.Left;
                pos.sh.Top = pos.re.Top;
                pos.sh.Width = pos.re.Width;
                pos.sh.Height = pos.re.Height;
            }
        }
    }
}