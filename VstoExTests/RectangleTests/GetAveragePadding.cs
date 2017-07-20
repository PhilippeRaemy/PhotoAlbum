namespace VstoExTests.RectanglesTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VstoEx.Geometry;
    using VstoEx.Extensions;

    /// <summary>
    /// Summary description for RectanglessTest
    /// </summary>
    [TestClass]
    public class GetAveragePadding
        {

        [TestMethod]
        public void OneRectangleAloneHasPaddingZero()
        {
            Assert.AreEqual(0f, new[] { new Rectangle(1, 1, 1, 1) }.GetAveragePadding());
        }

        [TestMethod]
        public void TwoAdjacentRectanglesHavePaddingZero_H()
        {
            Assert.AreEqual(0f, new[] { new Rectangle(1, 1, 1, 1), new Rectangle(2, 1, 1, 1) }.GetAveragePadding());
        }

        [TestMethod]
        public void TwoAdjacentRectanglesHavePaddingZero_V()
        {
            Assert.AreEqual(0f, new[] {new Rectangle(1, 1, 1, 1), new Rectangle(1, 2, 1, 1)}.GetAveragePadding());
        }

        [TestMethod]
        public void TwoAlignedNonAdjacentRectanglesHavePaddingAsTheirDistance_H()
        {
            Assert.AreEqual(1f, new[] {new Rectangle(1, 1, 1, 1), new Rectangle(3, 1, 1, 1)}.GetAveragePadding());
        }

        [TestMethod]
        public void TwoAlignedNonAdjacentRectanglesHavePaddingAsTheirDistance_V()
        {
            Assert.AreEqual(1f, new[] {new Rectangle(1, 1, 1, 1), new Rectangle(1, 3, 1, 1)}.GetAveragePadding());
        }

        [TestMethod]
        public void ThreeAdjacentRectanglesHavePaddingZero_H()
        {
            Assert.AreEqual(0f,
                new[] {new Rectangle(1, 1, 1, 1), new Rectangle(2, 1, 1, 1), new Rectangle(3, 1, 1, 1)}
                    .GetAveragePadding());
        }

        [TestMethod]
        public void ThreeAdjacentRectanglesHavePaddingZero_V()
        {
            Assert.AreEqual(0f,
                new[] {new Rectangle(1, 1, 1, 1), new Rectangle(1, 2, 1, 1), new Rectangle(1, 3, 1, 1)}
                    .GetAveragePadding());
        }

        [TestMethod]
        public void ThreeAlignedNonAdjacentRectanglesHavePaddingAsTheirDistance_H()
        {
            Assert.AreEqual(1f,
                new[] {new Rectangle(1, 1, 1, 1), new Rectangle(3, 1, 1, 1), new Rectangle(5, 1, 1, 1)}
                    .GetAveragePadding());
        }

        [TestMethod]
        public void ThreeAlignedNonAdjacentRectanglesHavePaddingAsTheirDistance_V()
        {
            Assert.AreEqual(1f,
                new[] {new Rectangle(1, 1, 1, 1), new Rectangle(1, 3, 1, 1), new Rectangle(1, 5, 1, 1)}
                    .GetAveragePadding());
        }

        [TestMethod]
        public void ThreeAlignedNonAdjacentRectanglesHavePaddingAsTheirAverageDistance_H()
        {
            Assert.AreEqual(1.5f,
                new[] {new Rectangle(1, 1, 1, 1), new Rectangle(3, 1, 1, 1), new Rectangle(6, 1, 1, 1)}
                    .GetAveragePadding());
        }

        [TestMethod]
        public void ThreeAlignedNonAdjacentRectanglesHavePaddingAsTheirAverageDistance_V()
        {
            Assert.AreEqual(1.5f,
                new[] {new Rectangle(1, 1, 1, 1), new Rectangle(1, 3, 1, 1), new Rectangle(6, 5, 1, 1)}
                    .GetAveragePadding());
        }

        [TestMethod]
        public void TwoOverlappingdRectanglesHaveNegativePaddingAsTheirDistance_H()
        {
            Assert.AreEqual(-1f, new[] {new Rectangle(1, 1, 2, 1), new Rectangle(2, 1, 1, 1)}.GetAveragePadding());
        }

        [TestMethod]
        public void TwoOverlappingdRectanglesHaveNegativePaddingAsTheirDistance_V()
        {
            Assert.AreEqual(-1f, new[] {new Rectangle(1, 1, 1, 2), new Rectangle(1, 2, 1, 1)}.GetAveragePadding());
        }

        [TestMethod]
        public void FourRectanglesInSquareHavePaddingAsTheirDistance()
        {
            Assert.AreEqual(1f, new[]
            {
                new Rectangle(1, 1, 1, 2),
                new Rectangle(3, 1, 1, 1),
                new Rectangle(1, 3, 1, 1),
                new Rectangle(3, 3, 1, 1)
            }.GetAveragePadding());
        }

        [TestMethod]
        public void FourRectanglesInParallelogramHavePaddingAsTheirDistance()
        {
            Assert.AreEqual(1f, new[]
            {
                new Rectangle(1, 1, 1, 2),
                new Rectangle(3, 1.5f, 1, 1),
                new Rectangle(1, 3, 1, 1),
                new Rectangle(3, 3.5f, 1, 1)
            }.GetAveragePadding());
        }

        [TestMethod]
        public void FourRectanglesInExtremeParallelogramHavePaddingAsTheirDistance_H()
        {
            Assert.AreEqual(1f, new[]
            {
                new Rectangle(1, 1, 1, 2),
                new Rectangle(5, 1, 1, 1),
                new Rectangle(1, 1, 1, 1),
                new Rectangle(7, 1, 1, 1)
            }.GetAveragePadding());
        }

        [TestMethod]
        public void FourRectanglesInExtremeParallelogramHavePaddingAsTheirDistance_V()
        {
            Assert.AreEqual(1f, new[]
            {
                new Rectangle(1, 1, 1, 2),
                new Rectangle(3, 3, 1, 1),
                new Rectangle(1, 5, 1, 1),
                new Rectangle(3, 7, 1, 1)
            }.GetAveragePadding());
        }
    }
}
