namespace VstoExTests.RectangleTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VstoEx.Geometry;

    [TestClass]
    public class RectangleTransformations
    {
        [TestMethod]
        public void TestRectangleMove()
        {
            var r = new Rectangle(0, 0, 1, 1).MoveBy(.3f, .6f);
            Assert.AreEqual(.3f, r.Left, float.Epsilon);
            Assert.AreEqual(.6f, r.Top, float.Epsilon);
        }

        [TestMethod]
        public void TestRectangleScale()
        {
            var r = new Rectangle(0, 0, 1, 1).Scale(.3f, .6f);
            Assert.AreEqual(.3f, r.Width, float.Epsilon);
            Assert.AreEqual(.6f, r.Height, float.Epsilon);
        }

        [TestMethod]
        public void TestRectangleFitInWidth()
        {
            var container = new Rectangle(1, 1, 8, 4);
            var contained = new Rectangle(4, 3, 2, 2);
            var r = contained.FitIn(container, .5f, .5f, 0);
            Assert.IsTrue(container.Left <= r.Left, "Overlap left border");
            Assert.IsTrue(r.Left + r.Width <= container.Left + container.Width, "Overlap right border");
            Assert.AreEqual(new Rectangle(3, 1, 4, 4), r );
        }

        [TestMethod]
        public void TestRectangleFitInHeight()
        {
            var container = new Rectangle(1, 1, 8, 4);
            var contained = new Rectangle(4, 3, 2, 2);
            var r = contained.FitIn(container, .5f, .5f, 0);
            Assert.IsTrue(container.Top <= r.Top, "Overlap top border");
            Assert.IsTrue(r.Top + r.Height <= container.Top + container.Height, "Overlap bottom border");
            Assert.AreEqual(new Rectangle(3, 1, 4, 4), r);
        }

        [TestMethod]
        public void TestRectangleFitInWithSpacing()
        {
            var container = new Rectangle(1, 1, 8, 4);
            var contained = new Rectangle(4, 3, 2, 2);
            var r = contained.FitIn(container, .5f, .5f, .1f);
            Assert.AreEqual(new Rectangle(3.1f, 1.1f, 3.8f, 3.8f), r);
        }

        [TestMethod]
        public void TestRectangleScaleInPlace()
        {
            var r = new Rectangle(1, 1, 8, 4);
            Assert.AreEqual(new Rectangle(3, 2, 4, 2), r.ScaleInPlace(.5f) );
        }

        [TestMethod]
        public void TestRectangleAbsorb()
        {
            var r = new Rectangle(1, 1, 1, 1).Absorb(new Rectangle(3, 4, 1, 1));
            Assert.AreEqual(new Rectangle(1, 1, 3, 4), r);
        }

    }
}