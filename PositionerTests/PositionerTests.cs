using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlbumWordAddin;
using System.Linq;
using MoreLinq;

namespace PositionerTests
{
    [TestClass]
    public class PositionerTests
    {
        static readonly Rectangle Square1X1 = new Rectangle(0, 0, 1, 1);

        [TestMethod]
        public void TestPositioner_1x1()
        {
            var pos = new Positioner { Cols = 1, Rows = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Padding = 0 };
            var rc = pos.DoPosition(Square1X1, new[] { Square1X1 }).ToArray();
            Assert.AreEqual(1, rc.Length);
            Assert.AreEqual(Square1X1, rc.First());
        }
        [TestMethod]
        public void TestPositioner_2x1()
        {
            var pos = new Positioner { Cols = 1, Rows = 2, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Padding = 0 };
            var rc = pos.DoPosition(Square1X1, new[] { Square1X1, Square1X1 }).ToArray();
            Assert.AreEqual(2, rc.Length);
            Assert.AreEqual(new Rectangle(.25f, 0, .5f, .5f), rc.First());
            Assert.AreEqual(new Rectangle(.25f, .5f, .5f, .5f), rc.Skip(1).First());
        }
        [TestMethod]
        public void TestPositioner_1x2()
        {
            new[] { .5f, 1f, 1.5f, 2f }.ForEach(TestPositioner_1x2);
        }
        public void TestPositioner_1x2(float factor)
        {
            var pos = new Positioner { Cols = 2, Rows = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Padding = 0 };
            var rc = pos.DoPosition(Square1X1.Grow(factor), new[] { Square1X1, Square1X1 }).ToArray();
            Assert.AreEqual(2, rc.Length);
            var expected = new Rectangle(0, .25f, .5f, .5f).Scale(factor, factor);
            Assert.AreEqual(expected, rc.First());
            expected = expected.Move(.5f * factor, 0);
            Assert.AreEqual(expected, rc.Skip(1).First());
        }
        [TestMethod]
        public void TestPositioner_1x3()
        {
            new[] { .5f, 1f, 1.5f, 2f }.ForEach(TestPositioner_1x3);
        }
        void TestPositioner_1x3(float factor)
        {
            var pos = new Positioner { Cols = 3, Rows = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Padding = 0 };
            var rc = pos.DoPosition(Square1X1.Grow(factor), new[] { Square1X1, Square1X1, Square1X1 }).ToArray();
            Assert.AreEqual(3, rc.Length);
            var expected = new Rectangle(0, 1 / 3f, 1 / 3f, 1 / 3f).Scale(factor, factor);
            Assert.AreEqual(expected, rc.First());
            expected = expected.Move(1 / 3f * factor, 0);
            Assert.AreEqual(expected, rc.Skip(1).First());
            expected = expected.Move(1 / 3f * factor, 0);
            Assert.AreEqual(expected, rc.Skip(2).First());
        }
        [TestMethod]
        public void TestPositioner_global()
        {
        }
        void TestPositioner_global(Rectangle clientArea, Positioner pos, Rectangle[] expected)
        {
        }
    }
}
