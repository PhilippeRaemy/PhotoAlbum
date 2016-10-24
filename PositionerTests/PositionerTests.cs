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
            var rc1 = rc.First();
            Assert.AreEqual(0f, rc1.Left, float.Epsilon);
            Assert.AreEqual(0f, rc1.Top, float.Epsilon);
            Assert.AreEqual(1f, rc1.Width, float.Epsilon);
            Assert.AreEqual(1f, rc1.Height, float.Epsilon);
        }
        [TestMethod]
        public void TestPositioner_2x1()
        {
            var pos = new Positioner { cols = 1, rows = 2, HShape = HShape.FLAT, VShape = VShape.FLAT, Margin = 0, Padding = 0 };
            var rc = pos.DoPosition(square1x1, new[] { square1x1, square1x1 });
            Assert.AreEqual(2, rc.Count());
            var rc1 = rc.First();
            Assert.AreEqual(.25f, rc1.Left, float.Epsilon, "Left  :" + rc1.ToString());
            Assert.AreEqual(0f, rc1.Top, float.Epsilon, "Left  :" + rc1.ToString());
            Assert.AreEqual(.5f, rc1.Width, float.Epsilon, "Width :" + rc1.ToString());
            Assert.AreEqual(.5f, rc1.Height, float.Epsilon, "Height:" + rc1.ToString());
        }
        [TestMethod]
        public void TestPositioner_1x2()
        {
            new[] {.5f, 1f, 1.5f, 2f }.ForEach(TestPositioner_1x2);
        }
        public void TestPositioner_1x2(float factor)
        {
            var pos = new Positioner { cols = 2, rows = 1, HShape = HShape.FLAT, VShape = VShape.FLAT, Margin = 0, Padding = 0 };
            var rc = pos.DoPosition(square1x1.Grow(factor), new[] { square1x1, square1x1 });
            Assert.AreEqual(2, rc.Count());
            var rc1 = rc.First();
            Assert.AreEqual(factor * 0f, rc1.Left, float.Epsilon, "Left  :" + rc1.ToString());
            Assert.AreEqual(factor * 0.25f, rc1.Top, float.Epsilon, "Left  :" + rc1.ToString());
            Assert.AreEqual(factor * .5f, rc1.Width, float.Epsilon, "Width :" + rc1.ToString());
            Assert.AreEqual(factor * .5f, rc1.Height, float.Epsilon, "Height:" + rc1.ToString());
        }
    }
}
