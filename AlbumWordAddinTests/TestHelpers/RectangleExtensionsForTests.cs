namespace AlbumWordAddinTests.TestHelpers
{
    using System.Collections.Generic;
    using System.Linq;
    using VstoEx.Geometry;

    public static class RectangleExtensionsForTests
    {
        /// <summary>
        /// Replicate a model rectangle a certain number of times with given position offsets
        /// </summary>
        /// <param name="model"></param>
        /// <param name="count"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        public static IEnumerable<Rectangle> Range(this Rectangle model, int count, float offsetX, float offsetY)
        {
            return Enumerable.Range(0, count).Select(i => model.MoveBy(i * offsetX, i * offsetY));
        }
        /// <summary>
        /// Replicate a model rectangle a certain number of times
        /// </summary>
        /// <param name="model"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<Rectangle> Range(this Rectangle model, int count)
            => model.Range(count, 0, 0);
    }
}