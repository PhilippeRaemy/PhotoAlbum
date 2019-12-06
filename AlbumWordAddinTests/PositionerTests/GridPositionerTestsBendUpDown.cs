namespace AlbumWordAddinTests.PositionerTests
{
    using AlbumWordAddin.Positioning;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestHelpers;

    [TestClass]
    public class GridPositionerTestsBendUpDown : PositionerTestsBase
    {
        protected override IPositioner GetNewPositioner() => new GridPositioner();

        static readonly PositionerParms BendDownPos  = new PositionerParms { Cols = 2, Rows = 1, HShape = HShape.Flat, VShape = VShape.Benddown, Margin = 0, Spacing = 0};
        static readonly PositionerParms BendUpPos    = new PositionerParms { Cols = 2, Rows = 1, HShape = HShape.Flat, VShape = VShape.Bendup  , Margin = 0, Spacing = 0};

        [TestMethod]
        public void TestPositioner_BendDown2Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(2), GridPositionerTests.R1X1.Range(2), BendDownPos.WithRowsCols(1, 2), GridPositionerTests.R1X1.Range(2, 1, 0), nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendDown3Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(3), GridPositionerTests.R1X1.Range(3), BendDownPos.WithRowsCols(1, 3), new []{GridPositionerTests.R1X1.MoveBy(0, 0), GridPositionerTests.R1X1.MoveBy(1, 2) , GridPositionerTests.R1X1.MoveBy(2, 0) } , nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendDown4Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(4), GridPositionerTests.R1X1.Range(4), BendDownPos.WithRowsCols(1, 4), new[] {GridPositionerTests.R1X1.MoveBy(0, 0), GridPositionerTests.R1X1.MoveBy(1, 3), GridPositionerTests.R1X1.MoveBy(2, 3), GridPositionerTests.R1X1.MoveBy(3, 0) }, nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendDown5Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(5), GridPositionerTests.R1X1.Range(5), BendDownPos.WithRowsCols(1, 5), new[] {GridPositionerTests.R1X1.MoveBy(0, 0), GridPositionerTests.R1X1.MoveBy(1, 2), GridPositionerTests.R1X1.MoveBy(2, 4), GridPositionerTests.R1X1.MoveBy(3, 2), GridPositionerTests.R1X1.MoveBy(4, 0) }, nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendDown6Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(6), GridPositionerTests.R1X1.Range(6), BendDownPos.WithRowsCols(1, 6), new[] {GridPositionerTests.R1X1.MoveBy(0, 0), GridPositionerTests.R1X1.MoveBy(1, 2.5f), GridPositionerTests.R1X1.MoveBy(2, 5), GridPositionerTests.R1X1.MoveBy(3, 5), GridPositionerTests.R1X1.MoveBy(4, 2.5f), GridPositionerTests.R1X1.MoveBy(5, 0) }, nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendUp2Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(2), GridPositionerTests.R1X1.Range(2), BendUpPos.WithRowsCols(1, 2), GridPositionerTests.R1X1.MoveBy(0, 1).Range(2, 1, 0), nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendUp3Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(3), GridPositionerTests.R1X1.Range(3), BendUpPos.WithRowsCols(1, 3), new[] {GridPositionerTests.R1X1.MoveBy(0, 2), GridPositionerTests.R1X1.MoveBy(1, 0), GridPositionerTests.R1X1.MoveBy(2, 2) }, nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendUp4Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(4), GridPositionerTests.R1X1.Range(4), BendUpPos.WithRowsCols(1, 4), new[] {GridPositionerTests.R1X1.MoveBy(0, 3), GridPositionerTests.R1X1.MoveBy(1, 0), GridPositionerTests.R1X1.MoveBy(2, 0), GridPositionerTests.R1X1.MoveBy(3, 3) }, nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendUp5Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(5), GridPositionerTests.R1X1.Range(5), BendUpPos.WithRowsCols(1, 5), new[] {GridPositionerTests.R1X1.MoveBy(0, 4), GridPositionerTests.R1X1.MoveBy(1, 2), GridPositionerTests.R1X1.MoveBy(2, 0), GridPositionerTests.R1X1.MoveBy(3, 2), GridPositionerTests.R1X1.MoveBy(4, 4) }, nameof(GridPositionerTests.R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendUp6Shapes()
        {
            Run(GridPositionerTests.R1X1.Grow(6), GridPositionerTests.R1X1.Range(6), BendUpPos.WithRowsCols(1, 6), new[] {GridPositionerTests.R1X1.MoveBy(0, 5), GridPositionerTests.R1X1.MoveBy(1, 2.5f), GridPositionerTests.R1X1.MoveBy(2, 0), GridPositionerTests.R1X1.MoveBy(3, 0), GridPositionerTests.R1X1.MoveBy(4, 2.5f), GridPositionerTests.R1X1.MoveBy(5, 5) }, nameof(GridPositionerTests.R1X1));
        }
    }
}