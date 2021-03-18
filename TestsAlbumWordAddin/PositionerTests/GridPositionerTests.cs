using System.Linq;
using AlbumWordAddin.Positioning;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;
using TestsAlbumWordAddin.TestHelpers;
using VstoEx.Extensions;
using VstoEx.Geometry;

namespace TestsAlbumWordAddin.PositionerTests
{
    using System.Security.Cryptography.X509Certificates;

    [TestClass]
    public class GridPositionerTests : PositionerTestsBase
    {
        protected override IPositioner GetNewPositioner() => new GridPositioner();

        internal static readonly Rectangle R1X1 = new Rectangle(0, 0, 1, 1);
        static readonly Rectangle R4X1 = new Rectangle(0, 0, 4, 1);
        static readonly Rectangle R1X4 = new Rectangle(0, 0, 1, 4);
        static readonly Rectangle R2X2 = new Rectangle(0, 0, 2, 2);
        static readonly Rectangle R4X2 = new Rectangle(0, 0, 4, 2);
        static readonly Rectangle R2X4 = new Rectangle(0, 0, 2, 4);
        static readonly PositionerParms TrivialPos   = new PositionerParms { Rows = 1, Cols = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly PositionerParms VFlat2X1Pos  = new PositionerParms { Rows = 2, Cols = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly PositionerParms HFlat1X2Pos  = new PositionerParms { Rows = 1, Cols = 2, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly PositionerParms HFlat1X3Pos  = new PositionerParms { Rows = 1, Cols = 3, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0 };
        static readonly PositionerParms HFlatPos     = new PositionerParms { Rows = 1, Cols = 4, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly PositionerParms HFlatPosPad  = new PositionerParms { Rows = 1, Cols = 4, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0.1f };
        static readonly PositionerParms HFlatLeftPos = new PositionerParms { Rows = 1, Cols = 4, HShape = HShape.Left, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly PositionerParms HFlatTopPos  = new PositionerParms { Rows = 1, Cols = 4, HShape = HShape.Flat, VShape = VShape.Top , Margin = 0, Spacing = 0    };
        static readonly PositionerParms VFlatPos     = new PositionerParms { Rows = 4, Cols = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly PositionerParms VFlatPosPad  = new PositionerParms { Rows = 4, Cols = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0.1f };
        static readonly PositionerParms VFlatLeftPos = new PositionerParms { Rows = 4, Cols = 1, HShape = HShape.Left, VShape = VShape.Flat, Margin = 0, Spacing = 0    };
        static readonly PositionerParms VFlatTopPos  = new PositionerParms { Rows = 4, Cols = 1, HShape = HShape.Flat, VShape = VShape.Top, Margin = 0, Spacing = 0 };
        static readonly PositionerParms VFlatTopPosMargin = new PositionerParms { Rows = 4, Cols = 1, HShape = HShape.Flat, VShape = VShape.Top, Margin = 0.1f, Spacing = 0 };


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
            var rc = new GridPositioner().DoPosition(HFlat1X2Pos, R1X1.Grow(factor), new[] { R1X1, R1X1 }).CheapToArray();
            Assert.AreEqual(2, rc.Length);
            var expected = new Rectangle(0, .25f, .5f, .5f).LinearScale(factor, factor);
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
            var rc = new GridPositioner().DoPosition(HFlat1X3Pos, R1X1.Grow(factor), new[] { R1X1, R1X1, R1X1 }).CheapToArray();
            Assert.AreEqual(3, rc.Length);
            var expected = new Rectangle(0, 1 / 3f, 1 / 3f, 1 / 3f).LinearScale(factor, factor);
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
            var clientArea = new Rectangle(0, 0, 4.3f, 1);
            Run(clientArea, R1X1.Range(4), HFlatPosPad, R1X1.Range(4, 1.1f, 0), nameof(R1X1));
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
            var clientArea = new Rectangle(0, 0, 1, 4.3f);
            Run(clientArea, R1X1.Range(4), VFlatPosPad, R1X1.Range(4, 0, 1.1f), nameof(R1X1));
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

        [TestMethod]
        public void TestPositioner_FourInaColTopMarginNoSpacing()
        {
            Run(R1X4, R1X1.Range(4), VFlatTopPosMargin, R1X1.Range(4, 0, 1).Select((r, i) => r.Grow(.8f).MoveBy(.1f,.1f - i*.05f)), nameof(R1X1));
        }

        [TestMethod]
        public void TestPositioner_FourInSquareNoMarginNoSpacing()
        {
            Run(R2X2, R1X1.Range(4),
                new PositionerParms { Rows = 2, Cols = 2, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0 },
                new[]
                {
                    R1X1, 
                    R1X1.MoveBy(1, 0), 
                    R1X1.MoveBy(0, 1), 
                    R1X1.MoveBy(1, 1)
                },
                "TestPositioner_FourInSquareNoMarginNoSpacing"
            );
        }

        [TestMethod]
        public void TestPositioner_FourInSquareSmallMarginNoSpacing()
        {
            Run(R2X2, R1X1.Range(4),
                new PositionerParms { Rows = 2, Cols = 2, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0.1f, Spacing = 0 },
                new[] {
                    R1X1.Grow(.9f).MoveBy(.1f, .1f),
                    R1X1.Grow(.9f).MoveBy(1, .1f),
                    R1X1.Grow(.9f).MoveBy(.1f, 1),
                    R1X1.Grow(.9f).MoveBy(1, 1)
                },
                "TestPositioner_FourInSquareNoMarginNoSpacing"
            );
        }
        [TestMethod]
        public void TestPositioner_FourInSquareLargeMarginNoSpacing()
        {
            Run(R2X2, R1X1.Range(4),
                new PositionerParms { Rows = 2, Cols = 2, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0.5f, Spacing = 0 },
                new[] {
                    R1X1.Grow(.5f).MoveBy(.5f, .5f),
                    R1X1.Grow(.5f).MoveBy(1, .5f),
                    R1X1.Grow(.5f).MoveBy(.5f, 1),
                    R1X1.Grow(.5f).MoveBy(1, 1)
                },
                "TestPositioner_FourInSquareNoMarginNoSpacing"
            );
        }
        [TestMethod]
        public void TestPositioner_FourInSquareSmallMarginSmallSpacing()
        {
            Run(R2X2, R1X1.Range(4),
                new PositionerParms { Rows = 2, Cols = 2, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0.1f, Spacing = .2f },
                new[] {
                    R1X1.Grow(.8f).MoveBy(.1f, .1f),
                    R1X1.Grow(.8f).MoveBy(1.1f, .1f),
                    R1X1.Grow(.8f).MoveBy(.1f, 1.1f),
                    R1X1.Grow(.8f).MoveBy(1.1f, 1.1f)
                },
                "TestPositioner_FourInSquareNoMarginNoSpacing"
            );
        }
        [TestMethod]
        public void TestPositioner_FourInSquareLargeMarginLargeSpacing()
        {
            Run(R2X2, R1X1.Range(4),
                new PositionerParms { Rows = 2, Cols = 2, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0.5f, Spacing = 0.5f },
                new[] {
                    R1X1.Grow(.25f).MoveBy(.5f, .5f),
                    R1X1.Grow(.25f).MoveBy(1.25f, .5f),
                    R1X1.Grow(.25f).MoveBy(.5f, 1.25f),
                    R1X1.Grow(.25f).MoveBy(1.25f, 1.25f)
                },
                "TestPositioner_FourInSquareNoMarginNoSpacing"
            );
        }
        [TestMethod]
        public void TestPositioner_FourInSquareNoMarginSmallSpacing()
        {
            Run(R2X2, R1X1.Range(4),
                new PositionerParms { Rows = 2, Cols = 2, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Spacing = 0.2f },
                new[] {
                    R1X1.Grow(.9f).MoveBy(0, 0),
                    R1X1.Grow(.9f).MoveBy(1.1f, 0),
                    R1X1.Grow(.9f).MoveBy(0, 1.1f),
                    R1X1.Grow(.9f).MoveBy(1.1f, 1.1f)
                },
                "TestPositioner_FourInSquareNoMarginNoSpacing"
            );
        }
    }
}
