using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlbumWordAddin;
using System.Linq;
using MoreLinq;

namespace PositionerTests
{
    [TestClass]
    public class PositionerTests
    {
        static readonly Rectangle square1x1 = new Rectangle(0, 0, 1, 1);

        [TestMethod]
        public void TestPositioner_1x1()
        {
            var pos = new Positioner { cols = 1, rows = 1, HShape = HShape.FLAT, VShape = VShape.FLAT, Margin = 0, Padding = 0 };
            var rc = pos.DoPosition(square1x1, new[] { square1x1 });
            Assert.AreEqual(1, rc.Count());
            Assert.AreEqual(square1x1, rc.First());
        }
        [TestMethod]
        public void TestPositioner_2x1()
        {
            var pos = new Positioner { cols = 1, rows = 2, HShape = HShape.FLAT, VShape = VShape.FLAT, Margin = 0, Padding = 0 };
            var rc = pos.DoPosition(square1x1, new[] { square1x1, square1x1 });
            Assert.AreEqual(2, rc.Count());
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
            var pos = new Positioner { cols = 2, rows = 1, HShape = HShape.FLAT, VShape = VShape.FLAT, Margin = 0, Padding = 0 };
            var rc = pos.DoPosition(square1x1.Grow(factor), new[] { square1x1, square1x1 });
            Assert.AreEqual(2, rc.Count());
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
        public void TestPositioner_1x3(float factor)
        {
            var pos = new Positioner { cols = 3, rows = 1, HShape = HShape.FLAT, VShape = VShape.FLAT, Margin = 0, Padding = 0 };
            var rc = pos.DoPosition(square1x1.Grow(factor), new[] { square1x1, square1x1, square1x1 });
            Assert.AreEqual(3, rc.Count());
            var expected = new Rectangle(0, 1/3f, 1 / 3f, 1 / 3f).Scale(factor, factor);
            Assert.AreEqual(expected, rc.First());
            expected = expected.Move(1 / 3f * factor, 0);
            Assert.AreEqual(expected, rc.Skip(1).First());
            expected = expected.Move(1 / 3f * factor, 0);
            Assert.AreEqual(expected, rc.Skip(2).First());
        }
    }
}
