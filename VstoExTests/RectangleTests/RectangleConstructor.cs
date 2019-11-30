namespace VstoExTests.RectangleTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VstoEx.Geometry;

    [TestClass]
    public class RectangleConstructor
    {
        [TestMethod]
        public void TopLeftWidthHeight()
        {
            var r = new Rectangle(1, 1, 1, 1);
            Assert.AreEqual(1f, r.Top, "Top");
            Assert.AreEqual(1f, r.Left, "Left");
            Assert.AreEqual(1f, r.Width, "Width");
            Assert.AreEqual(1f, r.Height, "Height");
            Assert.AreEqual(2f, r.Right, "Right");
            Assert.AreEqual(2f, r.Bottom, "Bottom");
            Assert.AreEqual(new Point(1, 1), r.TopLeft, "TopLeft");
            Assert.AreEqual(new Point(2, 2), r.BottomRight, "BottomRight");
        }

        [TestMethod]
        public void Points()
        {
            var r = new Rectangle(new Point(1, 1), new Point(2, 2));
            Assert.AreEqual(1f, r.Top, "Top");
            Assert.AreEqual(1f, r.Left, "Left");
            Assert.AreEqual(1f, r.Width, "Width");
            Assert.AreEqual(1f, r.Height, "Height");
            Assert.AreEqual(2f, r.Right, "Right");
            Assert.AreEqual(2f, r.Bottom, "Bottom");
            Assert.AreEqual(new Point(1, 1), r.TopLeft, "TopLeft");
            Assert.AreEqual(new Point(2, 2), r.BottomRight, "BottomRight");
        }

        [ExpectedException(typeof(InvalidOperationException), "Rectangle should not accept zero or negative height")]
        [TestMethod]
        public void CannotBeFlat()
        {
            var r = new Rectangle(0, 0, 1, -float.Epsilon);
        }

        [ExpectedException(typeof(InvalidOperationException), "Rectangle should not accept zero or negative width")]
        [TestMethod]
        public void CannotBeThin()
        {
            var r = new Rectangle(0, 0, -float.Epsilon, 1);
        }
    }
}