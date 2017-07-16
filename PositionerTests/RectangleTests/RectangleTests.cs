namespace AlbumWordAddinTests.RectangleTests
{
    using System;
    using AlbumWordAddin;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RectangleTests
    {
        [TestMethod]
        public void TestRectangle()
        {
            var r = new Rectangle(0, 0, 1, 1);
            Assert.AreEqual(0f             , r.Top        , "Top"        );
            Assert.AreEqual(0f             , r.Left       , "Left"       );
            Assert.AreEqual(1f             , r.Width      , "Width"      );
            Assert.AreEqual(1f             , r.Height     , "Height"     );
            Assert.AreEqual(1f             , r.Right      , "Right"      );
            Assert.AreEqual(1f             , r.Bottom     , "Bottom"     );
            Assert.AreEqual(new Point(0, 0), r.TopLeft    , "TopLeft"    );
            Assert.AreEqual(new Point(1, 1), r.BottomRight, "BottomRight");
        }

        [ExpectedException(typeof(InvalidOperationException), "Rectangle should not accept zero or negative height")]
        [TestMethod]
        public void TestRectangleCannotBeFlat()
        {
                var r = new Rectangle(0, 0, 1, 0);
        }

        [ExpectedException(typeof(InvalidOperationException), "Rectangle should not accept zero or negative width")]
        [TestMethod]
        public void TestRectangleCannotBeThin()
        {
            var r = new Rectangle(0, 0, 0, 1);
        }

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
    }
}
