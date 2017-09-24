namespace AlbumWordAddinTests.PositionerTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using AlbumWordAddin.Positioning;
    using MoreLinq;
    using TestHelpers;
    using VstoEx.Extensions;
    using VstoEx.Geometry;

    [TestClass]
    public class PositionerTests : PositionerTestsBase
    {
        internal static readonly Rectangle R1X1 = new Rectangle(0, 0, 1, 1);
        static readonly Rectangle R4X1 = new Rectangle(0, 0, 4, 1);
        static readonly Rectangle R1X4 = new Rectangle(0, 0, 1, 4);
        static readonly Rectangle R4X4 = new Rectangle(0, 0, 4, 4);
        static readonly Rectangle R4X2 = new Rectangle(0, 0, 4, 2);
        static readonly Rectangle R2X4 = new Rectangle(0, 0, 2, 4);
        static readonly Positioner.Parms TrivialPos   = new Positioner.Parms { Rows = 1, Cols = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly Positioner.Parms VFlat2X1Pos  = new Positioner.Parms { Rows = 2, Cols = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly Positioner.Parms HFlat1X2Pos  = new Positioner.Parms { Rows = 1, Cols = 2, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly Positioner.Parms HFlat1X3Pos  = new Positioner.Parms { Rows = 1, Cols = 3, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0 };
        static readonly Positioner.Parms HFlatPos     = new Positioner.Parms { Rows = 1, Cols = 4, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly Positioner.Parms HFlatPosPad  = new Positioner.Parms { Rows = 1, Cols = 4, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0.1f };
        static readonly Positioner.Parms HFlatLeftPos = new Positioner.Parms { Rows = 1, Cols = 4, HShape = HShape.Left, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly Positioner.Parms HFlatTopPos  = new Positioner.Parms { Rows = 1, Cols = 4, HShape = HShape.Flat, VShape = VShape.Top , Margin = 0, Spacing = 0    };
        static readonly Positioner.Parms VFlatPos     = new Positioner.Parms { Rows = 4, Cols = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly Positioner.Parms VFlatPosPad  = new Positioner.Parms { Rows = 4, Cols = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0.1f };
        static readonly Positioner.Parms VFlatLeftPos = new Positioner.Parms { Rows = 4, Cols = 1, HShape = HShape.Left, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly Positioner.Parms VFlatTopPos  = new Positioner.Parms { Rows = 4, Cols = 1, HShape = HShape.Flat, VShape = VShape.Top , Margin = 0, Spacing = 0    };

        [TestMethod]
        public void TestPositioner_1x1()
        {
            Run(R1X1, new[] { R1X1 }, TrivialPos, new[] { R1X1 }, string.Empty);
        }

        [TestMethod]
        public void TestPositioner_VFlat2X1()
        {
            Run(R1X1, new[] {R1X1, R1X1}, VFlat2X1Pos, new[] {new Rectangle(.25f, 0, .5f, .5f), new Rectangle(.25f, .5f, .5f, .5f)}, string.Empty);
        }

        [TestMethod]
        public void TestPositioner_HFlat1X2()
        {
            new[] { .5f, 1f, 1.5f, 2f }.ForEach(TestPositioner_HFlat1X2);
        }

        static void TestPositioner_HFlat1X2(float factor)
        {
            var rc = Positioner.DoPosition(HFlat1X2Pos, R1X1.Grow(factor), new[] { R1X1, R1X1 }).CheapToArray();
            Assert.AreEqual(2, rc.Length);
            var expected = new Rectangle(0, .25f, .5f, .5f).Scale(factor, factor);
            Assert.AreEqual(expected, rc.First());
            expected = expected.MoveBy(.5f * factor, 0);
            Assert.AreEqual(expected, rc.Skip(1).First());
        }

        [TestMethod]
        public void TestPositioner_HFlat1X3()
        {
            new[] { .5f, 1f, 1.5f, 2f }.ForEach(TestPositioner_HFlat1X3);
        }

        static void TestPositioner_HFlat1X3(float factor)
        {
            var rc = Positioner.DoPosition(HFlat1X3Pos, R1X1.Grow(factor), new[] { R1X1, R1X1, R1X1 }).CheapToArray();
            Assert.AreEqual(3, rc.Length);
            var expected = new Rectangle(0, 1 / 3f, 1 / 3f, 1 / 3f).Scale(factor, factor);
            Assert.AreEqual(expected, rc.First());
            expected = expected.MoveBy(1 / 3f * factor, 0);
            Assert.AreEqual(expected, rc.Skip(1).First());
            expected = expected.MoveBy(1 / 3f * factor, 0);
            Assert.AreEqual(expected, rc.Skip(2).First());
        }

        [TestMethod]
        public void TestPositioner_FourInaRowFlatNoSpacing()
        {
            Run(R4X1, R1X1.Range(4), HFlatPos, R1X1.Range(4, 1, 0), nameof(R1X1));
            Run(R4X1, R4X2.Range(4), HFlatPos, R4X2.MoveBy(0, .25f).Grow(.25f).Range(4, 1, 0), nameof(R4X2));
            Run(R4X1, R2X4.Range(4), HFlatPos, R2X4.MoveBy(.25f, 0).Grow(.25f).Range(4, 1, 0), nameof(R2X4));
        }

        [TestMethod]
        public void TestPositioner_FourInaRowFlatSpacing()
        {
            Run(R4X1, R1X1.Range(4), HFlatPosPad, R1X1.MoveBy(HFlatPosPad.Spacing, HFlatPosPad.Spacing).Grow(1 - 2 * HFlatPosPad.Spacing).Range(4, 1, 0), nameof(R1X1));
        }

        [TestMethod]
        public void TestPositioner_FourInaRowLeftNoSpacing()
        {
            Run(R4X1, R1X1.Range(4), HFlatLeftPos, R1X1.Range(4, 1, 0), nameof(R1X1));
            Run(R4X1, R4X2.Range(4), HFlatLeftPos, R4X2.MoveBy(0, .25f).Grow(.25f).Range(4, 1, 0), nameof(R4X2));
            Run(R4X1, R2X4.Range(4), HFlatLeftPos, R2X4.Grow(.25f).Range(4, 1, 0), nameof(R2X4));
        }

        [TestMethod]
        public void TestPositioner_FourInaRowTopNoSpacing()
        {
            Run(R4X1, R1X1.Range(4), HFlatTopPos, R1X1.Range(4, 1, 0), nameof(R1X1));
            Run(R4X1, R4X2.Range(4), HFlatTopPos, R4X2.Grow(.25f).Range(4, 1, 0), nameof(R4X2));
            Run(R4X1, R2X4.Range(4), HFlatTopPos, R2X4.MoveBy(.25f, 0).Grow(.25f).Range(4, 1, 0), nameof(R2X4));
        }

        [TestMethod]
        public void TestPositioner_FourInaColFlatNoSpacing()
        {
            Run(R1X4, R1X1.Range(4), VFlatPos, R1X1.Range(4, 0, 1), nameof(R1X1));
            Run(R1X4, R4X2.Range(4), VFlatPos, R4X2.MoveBy(0, .25f).Grow(.25f).Range(4, 0, 1), nameof(R4X2));
            Run(R1X4, R2X4.Range(4), VFlatPos, R2X4.MoveBy(.25f, 0).Grow(.25f).Range(4, 0, 1), nameof(R2X4));
        }

        [TestMethod]
        public void TestPositioner_FourInaColFlatSpacing()
        {
            Run(R1X4, R1X1.Range(4), VFlatPosPad, R1X1.MoveBy(VFlatPosPad.Spacing, VFlatPosPad.Spacing).Grow(1 - 2 * VFlatPosPad.Spacing).Range(4, 0, 1), nameof(R1X1));
        }

        [TestMethod]
        public void TestPositioner_FourInaColLeftNoSpacing()
        {
            Run(R1X4, R1X1.Range(4), VFlatLeftPos, R1X1.Range(4, 0, 1), nameof(R1X1));
            Run(R1X4, R4X2.Range(4), VFlatLeftPos, R4X2.MoveBy(0, .25f).Grow(.25f).Range(4, 0, 1), nameof(R4X2));
            Run(R1X4, R2X4.Range(4), VFlatLeftPos, R2X4.Grow(.25f).Range(4, 0, 1), nameof(R2X4));
        }

        [TestMethod]
        public void TestPositioner_FourInaColTopNoSpacing()
        {
            Run(R1X4, R1X1.Range(4), VFlatTopPos, R1X1.Range(4, 0, 1), nameof(R1X1));
            Run(R1X4, R4X2.Range(4), VFlatTopPos, R4X2.Grow(.25f).Range(4, 0, 1), nameof(R4X2));
            Run(R1X4, R2X4.Range(4), VFlatTopPos, R2X4.MoveBy(.25f, 0).Grow(.25f).Range(4, 0, 1), nameof(R2X4));
        }
    }
}
