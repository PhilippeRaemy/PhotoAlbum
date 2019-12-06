namespace AlbumWordAddinTests.PositionerTests
{
    using AlbumWordAddin.Positioning;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestHelpers;

    [TestClass]
    public class FlowPositionerTestsBendLeftRight : PositionerTestsBase
    {
        protected override IPositioner GetNewPositioner() => new FlowPositioner();

        static readonly PositionerParms BendLeftPos  = new PositionerParms { Cols = 2, Rows = 1, HShape = HShape.BendLeft, VShape = VShape.Flat, Margin = 0, Spacing = 0};
        static readonly PositionerParms BendRightPos = new PositionerParms { Cols = 2, Rows = 1, HShape = HShape.BendRight, VShape = VShape.Flat, Margin = 0, Spacing = 0};

        [TestMethod]
        public void TestPositioner_BendLeft2Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(2), GridPositionerTests.R1X1.Range(2), BendLeftPos.WithRowsCols(2, 1), GridPositionerTests.R1X1.Range(2, 0, 1), nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendLeft3Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(3), GridPositionerTests.R1X1.Range(3), BendLeftPos.WithRowsCols(3, 1), new []{GridPositionerTests.R1X1.MoveBy(0, 0), GridPositionerTests.R1X1.MoveBy(2, 1) , GridPositionerTests.R1X1.MoveBy(0, 2) } , nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendLeft4Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(4), GridPositionerTests.R1X1.Range(4), BendLeftPos.WithRowsCols(4, 1), new[] {GridPositionerTests.R1X1.MoveBy(0, 0), GridPositionerTests.R1X1.MoveBy(3, 1), GridPositionerTests.R1X1.MoveBy(3, 2), GridPositionerTests.R1X1.MoveBy(0, 3) }, nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendLeft5Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(5), GridPositionerTests.R1X1.Range(5), BendLeftPos.WithRowsCols(5, 1), new[] {GridPositionerTests.R1X1.MoveBy(0, 0), GridPositionerTests.R1X1.MoveBy(2, 1), GridPositionerTests.R1X1.MoveBy(4, 2), GridPositionerTests.R1X1.MoveBy(2, 3), GridPositionerTests.R1X1.MoveBy(0, 4) }, nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendLeft6Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(6), GridPositionerTests.R1X1.Range(6), BendLeftPos.WithRowsCols(6, 1), new[] {GridPositionerTests.R1X1.MoveBy(0, 0), GridPositionerTests.R1X1.MoveBy(2.5f, 1), GridPositionerTests.R1X1.MoveBy(5, 2), GridPositionerTests.R1X1.MoveBy(5, 3), GridPositionerTests.R1X1.MoveBy(2.5f, 4), GridPositionerTests.R1X1.MoveBy(0, 5) }, nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendRight2Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(2), GridPositionerTests.R1X1.Range(2), BendRightPos.WithRowsCols(2, 1), GridPositionerTests.R1X1.MoveBy(1, 0).Range(2, 0, 1), nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendRight3Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(3), GridPositionerTests.R1X1.Range(3), BendRightPos.WithRowsCols(3, 1), new[] {GridPositionerTests.R1X1.MoveBy(2, 0), GridPositionerTests.R1X1.MoveBy(0, 1), GridPositionerTests.R1X1.MoveBy(2, 2) }, nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendRight4Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(4), GridPositionerTests.R1X1.Range(4), BendRightPos.WithRowsCols(4, 1), new[] {GridPositionerTests.R1X1.MoveBy(3, 0), GridPositionerTests.R1X1.MoveBy(0, 1), GridPositionerTests.R1X1.MoveBy(0, 2), GridPositionerTests.R1X1.MoveBy(3, 3) }, nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendRight5Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(5), GridPositionerTests.R1X1.Range(5), BendRightPos.WithRowsCols(5, 1), new[] {GridPositionerTests.R1X1.MoveBy(4, 0), GridPositionerTests.R1X1.MoveBy(2, 1), GridPositionerTests.R1X1.MoveBy(0, 2), GridPositionerTests.R1X1.MoveBy(2, 3), GridPositionerTests.R1X1.MoveBy(4, 4) }, nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendRight6Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(6), GridPositionerTests.R1X1.Range(6), BendRightPos.WithRowsCols(6, 1), new[] {GridPositionerTests.R1X1.MoveBy(5, 0), GridPositionerTests.R1X1.MoveBy(2.5f, 1), GridPositionerTests.R1X1.MoveBy(0, 2), GridPositionerTests.R1X1.MoveBy(0, 3), GridPositionerTests.R1X1.MoveBy(2.5f, 4), GridPositionerTests.R1X1.MoveBy(5, 5) }, nameof(GridPositionerTests.R1X1));
        }
    }
}