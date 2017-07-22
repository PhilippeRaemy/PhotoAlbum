namespace VstoExTests.RectanglesTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VstoEx.Geometry;
    using VstoEx.Extensions;

    /// <summary>
    /// Summary description for RectanglessTest
    /// </summary>
    [TestClass]
    public class GetAverageSpacing
        {

        [TestMethod]
        public void OneRectangleAloneHasSpacingZero()
        {
            Assert.AreEqual(0f, new[] { new Rectangle(1, 1, 1, 1) }.GetAverageSpacing());
        }

        [TestMethod]
        public void TwoAdjacentRectanglesHaveSpacingZero_H()
        {
            Assert.AreEqual(0f, new[] { new Rectangle(1, 1, 1, 1), new Rectangle(2, 1, 1, 1) }.GetAverageSpacing());
        }

        [TestMethod]
        public void TwoAdjacentRectanglesHaveSpacingZero_V()
        {
            Assert.AreEqual(0f, new[] {new Rectangle(1, 1, 1, 1), new Rectangle(1, 2, 1, 1)}.GetAverageSpacing());
        }

        [TestMethod]
        public void TwoAlignedNonAdjacentRectanglesHaveSpacingAsTheirDistance_H()
        {
            Assert.AreEqual(1f, new[] {new Rectangle(1, 1, 1, 1), new Rectangle(3, 1, 1, 1)}.GetAverageSpacing());
        }

        [TestMethod]
        public void TwoAlignedNonAdjacentRectanglesHaveSpacingAsTheirDistance_V()
        {
            Assert.AreEqual(1f, new[] {new Rectangle(1, 1, 1, 1), new Rectangle(1, 3, 1, 1)}.GetAverageSpacing());
        }

        [TestMethod]
        public void ThreeAdjacentRectanglesHaveSpacingZero_H()
        {
            Assert.AreEqual(0f,
                new[] {new Rectangle(1, 1, 1, 1), new Rectangle(2, 1, 1, 1), new Rectangle(3, 1, 1, 1)}
                    .GetAverageSpacing());
        }

        [TestMethod]
        public void ThreeAdjacentRectanglesHaveSpacingZero_V()
        {
            Assert.AreEqual(0f,
                new[] {new Rectangle(1, 1, 1, 1), new Rectangle(1, 2, 1, 1), new Rectangle(1, 3, 1, 1)}
                    .GetAverageSpacing());
        }

        [TestMethod]
        public void ThreeAlignedNonAdjacentRectanglesHaveSpacingAsTheirDistance_H()
        {
            Assert.AreEqual(1f,
                new[] {new Rectangle(1, 1, 1, 1), new Rectangle(3, 1, 1, 1), new Rectangle(5, 1, 1, 1)}
                    .GetAverageSpacing());
        }

        [TestMethod]
        public void ThreeAlignedNonAdjacentRectanglesHaveSpacingAsTheirDistance_V()
        {
            Assert.AreEqual(1f,
                new[] {new Rectangle(1, 1, 1, 1), new Rectangle(1, 3, 1, 1), new Rectangle(1, 5, 1, 1)}
                    .GetAverageSpacing());
        }

        [TestMethod]
        public void ThreeAlignedNonAdjacentRectanglesHaveSpacingAsTheirAverageDistance_H()
        {
            Assert.AreEqual(1.5f,
                new[] {new Rectangle(1, 1, 1, 1), new Rectangle(3, 1, 1, 1), new Rectangle(6, 1, 1, 1)}
                    .GetAverageSpacing());
        }

        [TestMethod]
        public void ThreeAlignedNonAdjacentRectanglesHaveSpacingAsTheirAverageDistance_V()
        {
            Assert.AreEqual(1.5f,
                new[] {new Rectangle(1, 1, 1, 1), new Rectangle(1, 3, 1, 1), new Rectangle(6, 5, 1, 1)}
                    .GetAverageSpacing());
        }

        [TestMethod]
        public void TwoOverlappingdRectanglesHaveNegativeSpacingAsTheirAverageDistance_H()
        {
            Assert.AreEqual(-0.5, new[] {new Rectangle(1, 1, 2, 1), new Rectangle(2, 1, 1, 1)}.GetAverageSpacing());
        }

        [TestMethod]
        public void TwoOverlappingdRectanglesHaveNegativeSpacingAsTheirAverageDistance_V()
        {
            Assert.AreEqual(-0.5, new[] {new Rectangle(1, 1, 1, 2), new Rectangle(1, 2, 1, 1)}.GetAverageSpacing());
        }

        [TestMethod]
        public void FourRectanglesInSquareHaveSpacingAsTheirDistance()
        {
            Assert.AreEqual(1f, new[]
            {
                new Rectangle(1, 1, 1, 2),
                new Rectangle(3, 1, 1, 1),
                new Rectangle(1, 3, 1, 1),
                new Rectangle(3, 3, 1, 1)
            }.GetAverageSpacing());
        }

        [TestMethod]
        public void FourRectanglesInParallelogramHaveSpacingAsTheirDistance()
        {
            Assert.AreEqual(1f, new[]
            {
                new Rectangle(1, 1, 1, 2),
                new Rectangle(3, 1.5f, 1, 1),
                new Rectangle(1, 3, 1, 1),
                new Rectangle(3, 3.5f, 1, 1)
            }.GetAverageSpacing());
        }

        [TestMethod]
        public void FourRectanglesInExtremeParallelogramHaveSpacingAsTheirDistance_H()
        {
            Assert.AreEqual(1f, new[]
            {
                new Rectangle(1, 1, 1, 2),
                new Rectangle(5, 1, 1, 1),
                new Rectangle(1, 1, 1, 1),
                new Rectangle(7, 1, 1, 1)
            }.GetAverageSpacing());
        }

        [TestMethod]
        public void FourRectanglesInExtremeParallelogramHaveSpacingAsTheirDistance_V()
        {
            Assert.AreEqual(1f, new[]
            {
                new Rectangle(1, 1, 1, 2),
                new Rectangle(3, 3, 1, 1),
                new Rectangle(1, 5, 1, 1),
                new Rectangle(3, 7, 1, 1)
            }.GetAverageSpacing());
        }
    }
}
