namespace AlbumWordAddinTests

{
    using System.Collections.Generic;
    using System.Linq;
    using AlbumWordAddin;

    public static class RectangleExtensionsForTests
    {
        public static IEnumerable<Rectangle> Range(this Rectangle first, int count, float offsetX, float offsetY)
        {
            return Enumerable.Range(0, count).Select(i => first.MoveBy(i * offsetX, i * offsetY));
        }
        public static IEnumerable<Rectangle> Range(this Rectangle first, int count)
            => first.Range(count, 0, 0);
    }

    public static class PositionerExtensionsForTests
    {
        public static Positioner.Parms WithRowsCols(this Positioner.Parms model, int rows, int cols)
        {
            return new Positioner.Parms { Cols = cols, Rows = rows, HShape = model.HShape, VShape = model.VShape, Padding = model.Padding, Margin = model.Margin };
        }
    }


}