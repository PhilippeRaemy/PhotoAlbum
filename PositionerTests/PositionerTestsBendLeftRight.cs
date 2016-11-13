namespace PositionerTests
{
    using AlbumWordAddin;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PositionerTestsBendLeftRight
    {
        static readonly Positioner.Parms BendLeftPos  = new Positioner.Parms { Cols = 2, Rows = 1, HShape = HShape.BendLeft, VShape = VShape.Flat, Margin = 0, Padding = 0};
        static readonly Positioner.Parms BendRightPos    = new Positioner.Parms { Cols = 2, Rows = 1, HShape = HShape.BendRight, VShape = VShape.Flat, Margin = 0, Padding = 0};

        [TestMethod]
        public void TestPositioner_BendLeft2Shapes()
        {
            PositionerTests.Run(PositionerTests.R1X1.Grow(2), PositionerTests.R1X1.Range(2), BendLeftPos.WithRowsCols(2, 1), PositionerTests.R1X1.Range(2, 0, 1), nameof(PositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendLeft3Shapes()
        {
            PositionerTests.Run(PositionerTests.R1X1.Grow(3), PositionerTests.R1X1.Range(3), BendLeftPos.WithRowsCols(3, 1), new []{PositionerTests.R1X1.Move(0, 0), PositionerTests.R1X1.Move(2, 1) , PositionerTests.R1X1.Move(0, 2) } , nameof(PositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendLeft4Shapes()
        {
            PositionerTests.Run(PositionerTests.R1X1.Grow(4), PositionerTests.R1X1.Range(4), BendLeftPos.WithRowsCols(4, 1), new[] {PositionerTests.R1X1.Move(0, 0), PositionerTests.R1X1.Move(3, 1), PositionerTests.R1X1.Move(3, 2), PositionerTests.R1X1.Move(0, 3) }, nameof(PositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendLeft5Shapes()
        {
            PositionerTests.Run(PositionerTests.R1X1.Grow(5), PositionerTests.R1X1.Range(5), BendLeftPos.WithRowsCols(5, 1), new[] {PositionerTests.R1X1.Move(0, 0), PositionerTests.R1X1.Move(2, 1), PositionerTests.R1X1.Move(4, 2), PositionerTests.R1X1.Move(2, 3), PositionerTests.R1X1.Move(0, 4) }, nameof(PositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendLeft6Shapes()
        {
            PositionerTests.Run(PositionerTests.R1X1.Grow(6), PositionerTests.R1X1.Range(6), BendLeftPos.WithRowsCols(6, 1), new[] {PositionerTests.R1X1.Move(0, 0), PositionerTests.R1X1.Move(2.5f, 1), PositionerTests.R1X1.Move(5, 2), PositionerTests.R1X1.Move(5, 3), PositionerTests.R1X1.Move(2.5f, 4), PositionerTests.R1X1.Move(0, 5) }, nameof(PositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendRight2Shapes()
        {
            PositionerTests.Run(PositionerTests.R1X1.Grow(2), PositionerTests.R1X1.Range(2), BendRightPos.WithRowsCols(2, 1), PositionerTests.R1X1.Move(1, 0).Range(2, 0, 1), nameof(PositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendRight3Shapes()
        {
            PositionerTests.Run(PositionerTests.R1X1.Grow(3), PositionerTests.R1X1.Range(3), BendRightPos.WithRowsCols(3, 1), new[] {PositionerTests.R1X1.Move(2, 0), PositionerTests.R1X1.Move(0, 1), PositionerTests.R1X1.Move(2, 2) }, nameof(PositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendRight4Shapes()
        {
            PositionerTests.Run(PositionerTests.R1X1.Grow(4), PositionerTests.R1X1.Range(4), BendRightPos.WithRowsCols(4, 1), new[] {PositionerTests.R1X1.Move(3, 0), PositionerTests.R1X1.Move(0, 1), PositionerTests.R1X1.Move(0, 2), PositionerTests.R1X1.Move(3, 3) }, nameof(PositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendRight5Shapes()
        {
            PositionerTests.Run(PositionerTests.R1X1.Grow(5), PositionerTests.R1X1.Range(5), BendRightPos.WithRowsCols(5, 1), new[] {PositionerTests.R1X1.Move(4, 0), PositionerTests.R1X1.Move(2, 1), PositionerTests.R1X1.Move(0, 2), PositionerTests.R1X1.Move(2, 3), PositionerTests.R1X1.Move(4, 4) }, nameof(PositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendRight6Shapes()
        {
            PositionerTests.Run(PositionerTests.R1X1.Grow(6), PositionerTests.R1X1.Range(6), BendRightPos.WithRowsCols(6, 1), new[] {PositionerTests.R1X1.Move(5, 0), PositionerTests.R1X1.Move(2.5f, 1), PositionerTests.R1X1.Move(0, 2), PositionerTests.R1X1.Move(0, 3), PositionerTests.R1X1.Move(2.5f, 4), PositionerTests.R1X1.Move(5, 5) }, nameof(PositionerTests.R1X1));
        }
    }
}