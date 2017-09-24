namespace AlbumWordAddinTests.SpacerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AlbumWordAddin;
    using AlbumWordAddin.Positioning;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MoreLinq;
    using TestHelpers;
    using VstoEx.Extensions;
    using VstoEx.Geometry;

    [TestClass]
    public class SpacerTests
    {
        static readonly Rectangle R1X1 = new Rectangle(0, 0, 1, 1);

        static readonly Validation<Rectangle>[] EqualSpacingAdditionalValidation =
            {
                new Validation<Rectangle>("First rectangle is fixed", (r,s)=>r.First().Equals(s.First())),
                new Validation<Rectangle>("Last rectangle is fixed", (r,s)=>r.Last().Equals(s.Last())),
            };

        [TestMethod]
        public void TestHorizontalEqualSpacingDoesNothingToSingleton()
        {
            SpacerTestImpl(new[] {R1X1}, new[] {R1X1}, Spacer.HorizontalEqualSpacing, EqualSpacingAdditionalValidation);
        }
        [TestMethod]
        public void TestHorizontalEqualSpacingDoesNothingToPair()
        {
            SpacerTestImpl(new[] { R1X1, R1X1.MoveBy(2, 0) }, new[] { R1X1, R1X1.MoveBy(2, 0) }, Spacer.HorizontalEqualSpacing, EqualSpacingAdditionalValidation);
        }
        [TestMethod]
        public void TestHorizontalEqualSpacingTruple()
        {
            SpacerTestImpl(
                new[]
                {
                    R1X1, R1X1.MoveBy(2, 0), R1X1.MoveBy(3, 0)
                },
                Enumerable.Range(0, 3).Select(i => R1X1.MoveBy(i + i * 1f / 2, 0)),
                Spacer.HorizontalEqualSpacing, EqualSpacingAdditionalValidation
            );
        }
        [TestMethod]
        public void TestHorizontalEqualSpacingQuadruple()
        {
            SpacerTestImpl(new[]
                {
                    R1X1, R1X1.MoveBy(2, 0), R1X1.MoveBy(3, 0), R1X1.MoveBy(5, 0)
                },
                Enumerable.Range(0, 4).Select(i => R1X1.MoveBy(i + i * 2f / 3, 0)),
                Spacer.HorizontalEqualSpacing, EqualSpacingAdditionalValidation
            );
        }

        [TestMethod]
        public void TestHorizontalEqualSpacingTrupleWithOverlap()
        {
            SpacerTestImpl(
                new[]
                {
                    R1X1, R1X1, R1X1.MoveBy(3, 0)
                },
                Enumerable.Range(0, 3).Select(i => R1X1.MoveBy(i + i * 1f / 2, 0)),
                Spacer.HorizontalEqualSpacing, EqualSpacingAdditionalValidation
            );
        }
        [TestMethod]
        public void TestHorizontalEqualSpacingQuadrupleWithOverlap()
        {
            SpacerTestImpl(new[]
                {
                    R1X1, R1X1, R1X1, R1X1.MoveBy(5, 0)
                },
                Enumerable.Range(0, 4).Select(i => R1X1.MoveBy(i + i * 2f / 3, 0)),
                Spacer.HorizontalEqualSpacing, EqualSpacingAdditionalValidation
            );
        }

        [TestMethod]
        public void TestVerticalEqualSpacingDoesNothingToSingleton()
        {
            SpacerTestImpl(new[] { R1X1 }, new[] { R1X1 }, Spacer.VerticalEqualSpacing, EqualSpacingAdditionalValidation);
        }
        [TestMethod]
        public void TestVerticalEqualSpacingDoesNothingToPair()
        {
            SpacerTestImpl(new[] { R1X1, R1X1.MoveBy(0, 2) }, new[] { R1X1, R1X1.MoveBy(0, 2) }, Spacer.VerticalEqualSpacing, EqualSpacingAdditionalValidation);
        }
        [TestMethod]
        public void TestVerticalEqualSpacingTruple()
        {
            SpacerTestImpl(
                new[]
                {
                    R1X1, R1X1.MoveBy(0, 2), R1X1.MoveBy(0, 3)
                },
                Enumerable.Range(0, 3).Select(i => R1X1.MoveBy(0, i + i * 1f / 2)),
                Spacer.VerticalEqualSpacing, EqualSpacingAdditionalValidation
            );
        }
        [TestMethod]
        public void TestVerticalEqualSpacingQuadruple()
        {
            SpacerTestImpl(new[]
                {
                    R1X1, R1X1.MoveBy(0, 2), R1X1.MoveBy(0, 3), R1X1.MoveBy(0, 5)
                },
                Enumerable.Range(0, 4).Select(i => R1X1.MoveBy(0, i + i * 2f / 3)),
                Spacer.VerticalEqualSpacing, EqualSpacingAdditionalValidation
            );
        }

        [TestMethod]
        public void TestVerticalEqualSpacingTrupleWithOverlap()
        {
            SpacerTestImpl(
                new[]
                {
                    R1X1, R1X1, R1X1.MoveBy(0, 3)
                },
                Enumerable.Range(0, 3).Select(i => R1X1.MoveBy(0, i + i * 1f / 2)),
                Spacer.VerticalEqualSpacing, EqualSpacingAdditionalValidation
            );
        }
        [TestMethod]
        public void TestVerticalEqualSpacingQuadrupleWithOverlap()
        {
            SpacerTestImpl(new[]
                {
                    R1X1, R1X1, R1X1, R1X1.MoveBy(0, 5)
                },
                Enumerable.Range(0, 4).Select(i => R1X1.MoveBy(0, i + i * 2f / 3)),
                Spacer.VerticalEqualSpacing, EqualSpacingAdditionalValidation
            );
        }

        [TestMethod]
        public void TestChangeSpacingDoesNothingToSingleton()
        {
            SpacerTestImpl(new[] { R1X1 }, new[] { R1X1 }, Spacer.IncreaseHorizontal, EqualSpacingAdditionalValidation);
            SpacerTestImpl(new[] { R1X1 }, new[] { R1X1 }, Spacer.DecreaseHorizontal, EqualSpacingAdditionalValidation);
            SpacerTestImpl(new[] { R1X1 }, new[] { R1X1 }, Spacer.IncreaseVertical, EqualSpacingAdditionalValidation);
            SpacerTestImpl(new[] { R1X1 }, new[] { R1X1 }, Spacer.DecreaseVertical, EqualSpacingAdditionalValidation);
        }

        [TestMethod] public void TestIncreaseHorizontalPair(){SpacerTestImpl(new[] { R1X1, R1X1.MoveBy(2, 2) }, new[] { R1X1, R1X1.MoveBy(2 + Spacer.HorizontalGridUnit, 2) }, Spacer.IncreaseHorizontal);}
        [TestMethod] public void TestDecreaseHorizontalPair(){SpacerTestImpl(new[] { R1X1, R1X1.MoveBy(2, 2) }, new[] { R1X1, R1X1.MoveBy(2 - Spacer.HorizontalGridUnit, 2) }, Spacer.DecreaseHorizontal);}
        [TestMethod] public void TestIncreaseVerticalPair  (){SpacerTestImpl(new[] { R1X1, R1X1.MoveBy(2, 2) }, new[] { R1X1, R1X1.MoveBy(2, 2 + Spacer.VerticalGridUnit  ) }, Spacer.IncreaseVertical  );}
        [TestMethod] public void TestDecreaseVerticalPair  (){SpacerTestImpl(new[] { R1X1, R1X1.MoveBy(2, 2) }, new[] { R1X1, R1X1.MoveBy(2, 2 - Spacer.VerticalGridUnit  ) }, Spacer.DecreaseVertical  );}

        [TestMethod] public void TestIncreaseHorizontalTruple(){SpacerTestImpl(new[] { R1X1, R1X1.MoveBy(2, 2), R1X1.MoveBy(2, 2) }, new[] { R1X1, R1X1.MoveBy(2 + Spacer.HorizontalGridUnit, 2), R1X1.MoveBy(2 + 2 * Spacer.HorizontalGridUnit, 2) }, Spacer.IncreaseHorizontal);}
        [TestMethod] public void TestDecreaseHorizontalTruple(){SpacerTestImpl(new[] { R1X1, R1X1.MoveBy(2, 2), R1X1.MoveBy(2, 2) }, new[] { R1X1, R1X1.MoveBy(2 - Spacer.HorizontalGridUnit, 2), R1X1.MoveBy(2 - 2 * Spacer.HorizontalGridUnit, 2) }, Spacer.DecreaseHorizontal);}
        [TestMethod] public void TestIncreaseVerticalTruple  (){SpacerTestImpl(new[] { R1X1, R1X1.MoveBy(2, 2), R1X1.MoveBy(2, 2) }, new[] { R1X1, R1X1.MoveBy(2, 2 + Spacer.VerticalGridUnit  ), R1X1.MoveBy(2, 2 + 2 * Spacer.VerticalGridUnit  ) }, Spacer.IncreaseVertical  );}
        [TestMethod] public void TestDecreaseVerticalTruple  (){SpacerTestImpl(new[] { R1X1, R1X1.MoveBy(2, 2), R1X1.MoveBy(2, 2) }, new[] { R1X1, R1X1.MoveBy(2, 2 - Spacer.VerticalGridUnit  ), R1X1.MoveBy(2, 2 - 2 * Spacer.VerticalGridUnit  ) }, Spacer.DecreaseVertical  );}

        static void SpacerTestImpl(IEnumerable<Rectangle> source, IEnumerable<Rectangle> expected,
            Func<IEnumerable<Rectangle>, IEnumerable<Rectangle>> transformation,
            params Validation<Rectangle>[] validations
            )
        {
            var sourceA = source.CheapToArray();
            var results = transformation(sourceA).CheapToArray();
            var expectedA = expected.CheapToArray();
            Assert.AreEqual(expectedA.Length, results.Length, "Series length");
            expectedA.EquiZip(results, Tuple.Create).Index().ForEach(r=>Assert.AreEqual(r.Value.Item1, r.Value.Item2, $"Rectangle #{r.Key}"));
            validations.ForEach(v => v.Test(sourceA, results));
        }
    }
}
