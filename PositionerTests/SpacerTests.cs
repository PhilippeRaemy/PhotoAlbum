namespace PositionerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AlbumWordAddin;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MoreLinq;

    class Validation
    {
        public Validation(string message, Func<Rectangle[], Rectangle[], bool> test)
        {
            Message = message;
            Test = test;
        }
        public string Message { get; }
        public Func<Rectangle[], Rectangle[], bool> Test { get; }

    }

    [TestClass]
    public class SpacerTests
    {
        static readonly Rectangle R1X1 = new Rectangle(0, 0, 1, 1);
        static readonly Rectangle R4X1 = new Rectangle(0, 0, 4, 1);
        static readonly Rectangle R1X4 = new Rectangle(0, 0, 1, 4);
        static readonly Rectangle R4X4 = new Rectangle(0, 0, 4, 4);
        static readonly Rectangle R4X2 = new Rectangle(0, 0, 4, 2);
        static readonly Rectangle R2X4 = new Rectangle(0, 0, 2, 4);

        static readonly Validation[] EqualSpacingAdditionalValidation =
            {
                new Validation( "First rectangle is fixed", (r,s)=>r[0].Equals(s[0])),
                new Validation( "Last rectangle is fixed", (r,s)=>r.Last().Equals(s.Last())),
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
        public void TestSimpleHorizontalEqualSpacingTruple()
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
        public void TestSimpleHorizontalEqualSpacingQuadruple()
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
        public void TestSimpleHorizontalEqualSpacingTrupleWithOverlap()
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
        public void TestSimpleHorizontalEqualSpacingQuadrupleWithOverlap()
        {
            SpacerTestImpl(new[]
                {
                    R1X1, R1X1, R1X1, R1X1.MoveBy(5, 0)
                },
                Enumerable.Range(0, 4).Select(i => R1X1.MoveBy(i + i * 2f / 3, 0)),
                Spacer.HorizontalEqualSpacing, EqualSpacingAdditionalValidation
            );
        }

        static void SpacerTestImpl(IEnumerable<Rectangle> source, IEnumerable<Rectangle> expected,
            Func<IEnumerable<Rectangle>, IEnumerable<Rectangle>> transformation,
            params Validation[] validations
            )
        {
            var sourceA = source as Rectangle[] ?? source.ToArray();
            var results = transformation(sourceA).ToArray();
            var expectedA = expected as Rectangle[] ?? expected.ToArray();
            Assert.AreEqual(expectedA.Length, results.Length, "Series length");
            expectedA.EquiZip(results, Tuple.Create).Index().ForEach(r=>Assert.AreEqual(r.Value.Item1, r.Value.Item2, $"Rectangle #{r.Key}"));
            validations.ForEach(v => Assert.IsTrue(v.Test(sourceA, results), v.Message));
        }
    }

}
