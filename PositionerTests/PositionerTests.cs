using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlbumWordAddin;
using System.Linq;

namespace PositionerTests
{
    [TestClass]
    public class PositionerTests
    {
        static readonly Rectangle rect = new Rectangle(0, 0, 1, 1);

        [TestMethod]
        public void TestPositioner_1x1()
        {
            var pos = new Positioner { cols = 1, rows = 1, HShape = HShape.FLAT, VShape = VShape.FLAT, Margin = 0, Padding = 0 };
            var rc = pos.DoPosition(rect, new[] { rect });
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
            var rc = pos.DoPosition(rect, new[] { rect , rect });
            Assert.AreEqual(2, rc.Count());
            var rc1 = rc.First();
            Assert.AreEqual(0f, rc1.Left, float.Epsilon);
            Assert.AreEqual(0f, rc1.Left, float.Epsilon);
            Assert.AreEqual(.5f, rc1.Width, float.Epsilon);
            Assert.AreEqual(1f, rc1.Height, float.Epsilon);
        }
    }
}
