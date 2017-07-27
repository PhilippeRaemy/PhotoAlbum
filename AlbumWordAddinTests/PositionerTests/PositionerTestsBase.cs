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

    public class PositionerTestsBase
    {
        internal static void Run(Rectangle clientArea, IEnumerable<Rectangle> rectangles, Positioner.Parms pos, IEnumerable<Rectangle> expected, string label)
        {
            var rc = Positioner.DoPosition(pos, clientArea, rectangles).CheapToArray();
            expected = expected.CheapToArray();
            Assert.AreEqual(expected.Count(), rc.Length, $"{label}: Results length");
            var results = expected.EquiZip(rc, (e, r) => new {expected = e, results = r})
                .Select((r, i) => new {i, r.expected, r.results, success = r.expected.Equals(r.results)})
                .ToArray();

            Assert.IsTrue(results.All(r=>r.success), results.Select(r=> $"{Environment.NewLine}{r.expected} {(r.success ? "==" : "<>")} {r.results}").ToDelimitedString(","), $"{label}: equality");
        }
    }
}