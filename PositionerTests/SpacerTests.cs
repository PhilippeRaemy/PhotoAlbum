namespace PositionerTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using AlbumWordAddin;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MoreLinq;

    [TestClass]
    public class SpacerTests
    {
        static readonly Rectangle R1X1 = new Rectangle(0, 0, 1, 1);
        static readonly Rectangle R4X1 = new Rectangle(0, 0, 4, 1);
        static readonly Rectangle R1X4 = new Rectangle(0, 0, 1, 4);
        static readonly Rectangle R4X4 = new Rectangle(0, 0, 4, 4);
        static readonly Rectangle R4X2 = new Rectangle(0, 0, 4, 2);
        static readonly Rectangle R2X4 = new Rectangle(0, 0, 2, 4);

        [TestMethod]
        public void TestHorizontalEqualSpacingDoesNothingToSingleton()
        {
            SpacerTestImpl(new[] {R1X1}, new[] {R1X1}, Spacer.HorizontalEqualSpacing);
        }
        [TestMethod]
        public void TestHorizontalEqualSpacingDoesNothingToPair()
        {
            SpacerTestImpl(new[] { R1X1, R1X1.Move(2, 0) }, new[] { R1X1, R1X1.Move(2, 0) }, Spacer.HorizontalEqualSpacing);
        }
        [TestMethod]
        public void TestSimpleHorizontalEqualSpacingTruple()
        {
            SpacerTestImpl(
                new[]
                {
                    R1X1, R1X1.Move(2, 0), R1X1.Move(3, 0)
                },
                Enumerable.Range(0, 4).Select(i => R1X1.Move(i + i * 1f / 2, 0)),
                Spacer.HorizontalEqualSpacing
            );
        }
        [TestMethod]
        public void TestSimpleHorizontalEqualSpacingQuadruple()
        {
            SpacerTestImpl(new[]
                {
                    R1X1, R1X1.Move(2, 0), R1X1.Move(3, 0), R1X1.Move(5, 0)
                }, 
                Enumerable.Range(0, 4).Select(i => R1X1.Move(i + i * 1f / 3, 0)), 
                Spacer.HorizontalEqualSpacing
            );
        }

        static void SpacerTestImpl(IEnumerable<Rectangle> source, IEnumerable<Rectangle> expected,
            Func<IEnumerable<Rectangle>, IEnumerable<Rectangle>> transformation)
        {
            var sourceA = source as Rectangle[] ?? source.ToArray();
            var results = transformation(sourceA).ToArray();
            Assert.AreEqual(sourceA.Length, results.Length, "Series length");
            expected.EquiZip(results, Tuple.Create).Index().ForEach(r=>Assert.AreEqual(r.Value.Item1, r.Value.Item2, $"Rectangle #{r.Key}"));
        }
    }

}
