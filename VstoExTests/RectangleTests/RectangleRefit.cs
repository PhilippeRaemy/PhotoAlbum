namespace VstoExTests.RectangleTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VstoEx.Geometry;

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
