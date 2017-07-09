namespace AlbumWordAddinTests.TestHelpers
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
}