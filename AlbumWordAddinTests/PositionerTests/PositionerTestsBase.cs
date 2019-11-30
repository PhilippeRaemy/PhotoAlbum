namespace AlbumWordAddinTests.PositionerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AlbumWordAddin.Positioning;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MoreLinq;
    using VstoEx.Extensions;
    using VstoEx.Geometry;

    public abstract class PositionerTestsBase
    {
        protected abstract IPositioner GetNewPositioner();

        internal void Run(Rectangle clientArea, IEnumerable<Rectangle> rectangles, PositionerParms pos, IEnumerable<Rectangle> expected, string label) 
            => Test(GetNewPositioner().DoPosition(pos, clientArea, rectangles), expected, label);

        internal void Test(IEnumerable<Rectangle> rectangles, IEnumerable<Rectangle> expected, string label)
        {
            expected = expected.CheapToArray();
            var rc = rectangles.CheapToArray();
            Assert.AreEqual(expected.Count(), rc.Length, $"{label}: Results length");
            var results = expected.EquiZip(rc, (e, r) => new { expected = e, results = r })
                .Select((r, i) => new { i, r.expected, r.results, success = r.expected.Equals(r.results) })
                .ToArray();

            Assert.IsTrue(results.All(r => r.success), results.Select(r => $"{Environment.NewLine}{r.expected} {(r.success ? "==" : "<>")} {r.results}").ToDelimitedString(","), $"{label}: equality");
        }
    }
}