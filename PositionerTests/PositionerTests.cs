using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlbumWordAddin;
using System.Linq;
using MoreLinq;

namespace PositionerTests
{
    public static class RectangleExtensions
    {
        public static IEnumerable<Rectangle> Range(this Rectangle first, int count, float offsetX, float offsetY)
        {
            return Enumerable.Range(0, count).Select(i => first.Move(i * offsetX, i * offsetY));
        }

        public static IEnumerable<Rectangle> Range(this Rectangle first, int count)
            => first.Range(count, 0, 0);
    }

    public static class PositionerExtensions
    {
        public static Positioner.Parms WithRowsCols(this Positioner.Parms model, int rows, int cols)
        {
            return new Positioner.Parms {Cols=cols, Rows=rows,HShape = model.HShape, VShape = model.VShape, Padding = model.Padding, Margin = model.Margin};
        }
    }

    [TestClass]
    public class PositionerTests
    {
        static readonly Rectangle R1X1 = new Rectangle(0, 0, 1, 1);
        static readonly Rectangle R4X1 = new Rectangle(0, 0, 4, 1);
        static readonly Rectangle R1X4 = new Rectangle(0, 0, 1, 4);
        static readonly Rectangle R4X4 = new Rectangle(0, 0, 4, 4);
        static readonly Rectangle R4X2 = new Rectangle(0, 0, 4, 2);
        static readonly Rectangle R2X4 = new Rectangle(0, 0, 2, 4);
        static readonly Positioner.Parms HFlatPos     = new Positioner.Parms { Cols = 4, Rows = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Padding = 0    };
        static readonly Positioner.Parms HFlatPosPad  = new Positioner.Parms { Cols = 4, Rows = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Padding = 0.1f };
        static readonly Positioner.Parms HFlatLeftPos = new Positioner.Parms { Cols = 4, Rows = 1, HShape = HShape.Left, VShape = VShape.Flat, Margin = 0, Padding = 0    };
        static readonly Positioner.Parms HFlatTopPos  = new Positioner.Parms { Cols = 4, Rows = 1, HShape = HShape.Flat, VShape = VShape.Top , Margin = 0, Padding = 0};
        static readonly Positioner.Parms VFlatPos     = new Positioner.Parms { Cols = 1, Rows = 4, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Padding = 0    };
        static readonly Positioner.Parms VFlatPosPad  = new Positioner.Parms { Cols = 1, Rows = 4, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Padding = 0.1f };
        static readonly Positioner.Parms VFlatLeftPos = new Positioner.Parms { Cols = 1, Rows = 4, HShape = HShape.Left, VShape = VShape.Flat, Margin = 0, Padding = 0    };
        static readonly Positioner.Parms VFlatTopPos  = new Positioner.Parms { Cols = 1, Rows = 4, HShape = HShape.Flat, VShape = VShape.Top , Margin = 0, Padding = 0};
        static readonly Positioner.Parms BendDownPos  = new Positioner.Parms { Cols = 2, Rows = 1, HShape = HShape.Flat, VShape = VShape.Benddown, Margin = 0, Padding = 0};
        static readonly Positioner.Parms BendUpPos    = new Positioner.Parms { Cols = 2, Rows = 1, HShape = HShape.Flat, VShape = VShape.Bendup  , Margin = 0, Padding = 0};

        [TestMethod]
        public void TestPositioner_1x1()
        {
            var pos = new Positioner.Parms{ Cols = 1, Rows = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Padding = 0 };
            var rc = Positioner.DoPosition(pos, R1X1, new[] { R1X1 }).ToArray();
            Assert.AreEqual(1, rc.Length);
            Assert.AreEqual(R1X1, rc.First());
        }
        [TestMethod]
        public void TestPositioner_2x1()
        {
            var pos = new Positioner.Parms { Cols = 1, Rows = 2, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Padding = 0 };
            var rc = Positioner.DoPosition(pos, R1X1, new[] { R1X1, R1X1 }).ToArray();
            Assert.AreEqual(2, rc.Length);
            Assert.AreEqual(new Rectangle(.25f, 0, .5f, .5f), rc.First());
            Assert.AreEqual(new Rectangle(.25f, .5f, .5f, .5f), rc.Skip(1).First());
        }
        [TestMethod]
        public void TestPositioner_1x2()
        {
            new[] { .5f, 1f, 1.5f, 2f }.ForEach(TestPositioner_1x2);
        }
        public void TestPositioner_1x2(float factor)
        {
            var pos = new Positioner.Parms { Cols = 2, Rows = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Padding = 0 };
            var rc = Positioner.DoPosition(pos, R1X1.Grow(factor), new[] { R1X1, R1X1 }).ToArray();
            Assert.AreEqual(2, rc.Length);
            var expected = new Rectangle(0, .25f, .5f, .5f).Scale(factor, factor);
            Assert.AreEqual(expected, rc.First());
            expected = expected.Move(.5f * factor, 0);
            Assert.AreEqual(expected, rc.Skip(1).First());
        }
        [TestMethod]
        public void TestPositioner_1x3()
        {
            new[] { .5f, 1f, 1.5f, 2f }.ForEach(TestPositioner_1x3);
        }
        void TestPositioner_1x3(float factor)
        {
            var pos = new Positioner.Parms { Cols = 3, Rows = 1, HShape = HShape.Flat, VShape = VShape.Flat, Margin = 0, Padding = 0 };
            var rc = Positioner.DoPosition(pos, R1X1.Grow(factor), new[] { R1X1, R1X1, R1X1 }).ToArray();
            Assert.AreEqual(3, rc.Length);
            var expected = new Rectangle(0, 1 / 3f, 1 / 3f, 1 / 3f).Scale(factor, factor);
            Assert.AreEqual(expected, rc.First());
            expected = expected.Move(1 / 3f * factor, 0);
            Assert.AreEqual(expected, rc.Skip(1).First());
            expected = expected.Move(1 / 3f * factor, 0);
            Assert.AreEqual(expected, rc.Skip(2).First());
        }

        [TestMethod]
        public void TestPositioner_FourInaRowFlatNoPadding()
        {
            TestPositioner_global(R4X1, R1X1.Range(4), HFlatPos, R1X1.Range(4, 1, 0), nameof(R1X1));
            TestPositioner_global(R4X1, R4X2.Range(4), HFlatPos, R4X2.Move(0, .25f).Grow(.25f).Range(4, 1, 0), nameof(R4X2));
            TestPositioner_global(R4X1, R2X4.Range(4), HFlatPos, R2X4.Move(.25f, 0).Grow(.25f).Range(4, 1, 0), nameof(R2X4));
        }
        [TestMethod]
        public void TestPositioner_FourInaRowFlatPadding()
        {
            TestPositioner_global(R4X1, R1X1.Range(4), HFlatPosPad, R1X1.Move(HFlatPosPad.Padding, HFlatPosPad.Padding).Grow(1 - 2 * HFlatPosPad.Padding).Range(4, 1, 0), nameof(R1X1));
        }
        [TestMethod]
        public void TestPositioner_FourInaRowLeftNoPadding()
        {
            TestPositioner_global(R4X1, R1X1.Range(4), HFlatLeftPos, R1X1.Range(4, 1, 0), nameof(R1X1));
            TestPositioner_global(R4X1, R4X2.Range(4), HFlatLeftPos, R4X2.Move(0, .25f).Grow(.25f).Range(4, 1, 0), nameof(R4X2));
            TestPositioner_global(R4X1, R2X4.Range(4), HFlatLeftPos, R2X4.Grow(.25f).Range(4, 1, 0), nameof(R2X4));
        }
        [TestMethod]
        public void TestPositioner_FourInaRowTopNoPadding()
        {
            TestPositioner_global(R4X1, R1X1.Range(4), HFlatTopPos, R1X1.Range(4, 1, 0), nameof(R1X1));
            TestPositioner_global(R4X1, R4X2.Range(4), HFlatTopPos, R4X2.Grow(.25f).Range(4, 1, 0), nameof(R4X2));
            TestPositioner_global(R4X1, R2X4.Range(4), HFlatTopPos, R2X4.Move(.25f, 0).Grow(.25f).Range(4, 1, 0), nameof(R2X4));
        }
        [TestMethod]
        public void TestPositioner_FourInaColFlatNoPadding()
        {
            TestPositioner_global(R1X4, R1X1.Range(4), VFlatPos, R1X1.Range(4, 0, 1), nameof(R1X1));
            TestPositioner_global(R1X4, R4X2.Range(4), VFlatPos, R4X2.Move(0, .25f).Grow(.25f).Range(4, 0, 1), nameof(R4X2));
            TestPositioner_global(R1X4, R2X4.Range(4), VFlatPos, R2X4.Move(.25f, 0).Grow(.25f).Range(4, 0, 1), nameof(R2X4));
        }
        [TestMethod]
        public void TestPositioner_FourInaColFlatPadding()
        {
            TestPositioner_global(R1X4, R1X1.Range(4), VFlatPosPad, R1X1.Move(VFlatPosPad.Padding, VFlatPosPad.Padding).Grow(1 - 2 * VFlatPosPad.Padding).Range(4, 0, 1), nameof(R1X1));
        }
        [TestMethod]
        public void TestPositioner_FourInaColLeftNoPadding()
        {
            TestPositioner_global(R1X4, R1X1.Range(4), VFlatLeftPos, R1X1.Range(4, 0, 1), nameof(R1X1));
            TestPositioner_global(R1X4, R4X2.Range(4), VFlatLeftPos, R4X2.Move(0, .25f).Grow(.25f).Range(4, 0, 1), nameof(R4X2));
            TestPositioner_global(R1X4, R2X4.Range(4), VFlatLeftPos, R2X4.Grow(.25f).Range(4, 0, 1), nameof(R2X4));
        }
        [TestMethod]
        public void TestPositioner_FourInaColTopNoPadding()
        {
            TestPositioner_global(R1X4, R1X1.Range(4), VFlatTopPos, R1X1.Range(4, 0, 1), nameof(R1X1));
            TestPositioner_global(R1X4, R4X2.Range(4), VFlatTopPos, R4X2.Grow(.25f).Range(4, 0, 1), nameof(R4X2));
            TestPositioner_global(R1X4, R2X4.Range(4), VFlatTopPos, R2X4.Move(.25f, 0).Grow(.25f).Range(4, 0, 1), nameof(R2X4));
        }

        [TestMethod]
        public void TestPositioner_BendDown2Shapes()
        {
            TestPositioner_global(R1X1.Grow(2), R1X1.Range(2), BendDownPos.WithRowsCols(1, 2), R1X1.Range(2, 1, 0), nameof(R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendDown3Shapes()
        {
            TestPositioner_global(R1X1.Grow(3), R1X1.Range(3), BendDownPos.WithRowsCols(1, 3), new []{R1X1.Move(0, 0), R1X1.Move(1, 2) , R1X1.Move(2, 0) } , nameof(R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendDown4Shapes()
        {
            TestPositioner_global(R1X1.Grow(4), R1X1.Range(4), BendDownPos.WithRowsCols(1, 4), new[] { R1X1.Move(0, 0), R1X1.Move(1, 3), R1X1.Move(2, 3), R1X1.Move(3, 0) }, nameof(R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendDown5Shapes()
        {
            TestPositioner_global(R1X1.Grow(5), R1X1.Range(5), BendDownPos.WithRowsCols(1, 5), new[] { R1X1.Move(0, 0), R1X1.Move(1, 2), R1X1.Move(2, 4), R1X1.Move(3, 2), R1X1.Move(4, 0) }, nameof(R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendDown6Shapes()
        {
            TestPositioner_global(R1X1.Grow(6), R1X1.Range(6), BendDownPos.WithRowsCols(1, 6), new[] { R1X1.Move(0, 0), R1X1.Move(1, 2.5f), R1X1.Move(2, 5), R1X1.Move(3, 5), R1X1.Move(4, 2.5f), R1X1.Move(5, 0) }, nameof(R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendUp2Shapes()
        {
            TestPositioner_global(R1X1.Grow(2), R1X1.Range(2), BendUpPos.WithRowsCols(1, 2), R1X1.Move(0, 1).Range(2, 1, 0), nameof(R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendUp3Shapes()
        {
            TestPositioner_global(R1X1.Grow(3), R1X1.Range(3), BendUpPos.WithRowsCols(1, 3), new[] { R1X1.Move(0, 2), R1X1.Move(1, 0), R1X1.Move(2, 2) }, nameof(R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendUp4Shapes()
        {
            TestPositioner_global(R1X1.Grow(4), R1X1.Range(4), BendUpPos.WithRowsCols(1, 4), new[] { R1X1.Move(0, 3), R1X1.Move(1, 0), R1X1.Move(2, 0), R1X1.Move(3, 3) }, nameof(R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendUp5Shapes()
        {
            TestPositioner_global(R1X1.Grow(5), R1X1.Range(5), BendUpPos.WithRowsCols(1, 5), new[] { R1X1.Move(0, 4), R1X1.Move(1, 2), R1X1.Move(2, 0), R1X1.Move(3, 2), R1X1.Move(4, 4) }, nameof(R1X1));
        }

        [TestMethod]
        public void TestPositioner_BendUp6Shapes()
        {
            TestPositioner_global(R1X1.Grow(6), R1X1.Range(6), BendUpPos.WithRowsCols(1, 6), new[] { R1X1.Move(0, 5), R1X1.Move(1, 2.5f), R1X1.Move(2, 0), R1X1.Move(3, 0), R1X1.Move(4, 2.5f), R1X1.Move(5, 5) }, nameof(R1X1));
        }

        static void TestPositioner_global(Rectangle clientArea, IEnumerable<Rectangle> rectangles, Positioner.Parms pos, IEnumerable<Rectangle> expected, string label)
        {
            var rc = Positioner.DoPosition(pos, clientArea, rectangles).ToArray();
            expected = expected.ToArray();
            Assert.AreEqual(expected.Count(), rc.Length, "Results length");
            foreach (var rr in expected.EquiZip(rc,(e,r)=> new {expected=e, results=r})
                                 .Select((r,i)=>new {i, r.expected, r.results}))
            {
                Assert.AreEqual(rr.expected, rr.results, $"{label} failed expected #{rr.i}");
            }
        }
    }
}
