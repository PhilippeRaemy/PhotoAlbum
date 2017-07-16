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
            var r = new Rectangle(0, 0, 1, 0);
        }

        [ExpectedException(typeof(InvalidOperationException), "Rectangle should not accept zero or negative width")]
        [TestMethod]
        public void CannotBeThin()
        {
            var r = new Rectangle(0, 0, 0, 1);
        }
    }

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
        public void TestRectangleFitInWithPadding()
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

    [TestClass]
    public class RectangleRefit
    {
        [TestMethod]
        public void RefitToSmallerOriginZero()
        {
            Assert.AreEqual(new Rectangle(.1f, .1f, .1f, .1f),
                new Rectangle(1, 1, 1, 1)
                    .ReFit(new Rectangle(0, 0, 10, 10),
                           new Rectangle(0, 0, 1, 1)));
        }

        [TestMethod]
        public void RefitToSmallerOriginNonZero()
        {
            Assert.AreEqual(new Rectangle(1.1f, 1.1f, .1f, .1f),
                new Rectangle(2, 2, 1, 1)
                    .ReFit(new Rectangle(1, 1, 10, 10),
                           new Rectangle(1, 1, 1, 1)));
        }

        [TestMethod]
        public void RefitToSmallerNonSymetric()
        {
            Assert.AreEqual(new Rectangle(2.125f, 1.375f, 0.75f, 0.75f),
                new Rectangle(2, 2, 1, 1)
                    .ReFit(new Rectangle(1, 1, 10, 10),
                           new Rectangle(1, 1, 10,  5)));
        }

        [TestMethod]
        public void RefitToLargerOriginZero()
        {
            Assert.AreEqual(new Rectangle(10, 10, 10, 10), 
                new Rectangle(1, 1, 1, 1)
                    .ReFit(new Rectangle(0, 0, 3, 3),
                           new Rectangle(0, 0, 30, 30)));
        }

        [TestMethod]
        public void RefitToLargerOriginNonZero()
        {
            Assert.AreEqual(new Rectangle(20, 20, 10, 10), 
                new Rectangle(2, 2, 1, 1)
                    .ReFit(new Rectangle(1, 1, 3, 3),
                           new Rectangle(10, 10, 30, 30)));
        }
    }
}
