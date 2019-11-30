﻿namespace AlbumWordAddinTests.PositionerTests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using AlbumWordAddin.Positioning;
    using MoreLinq;
    using TestHelpers;
    using VstoEx.Extensions;
    using VstoEx.Geometry;

    [TestClass]
    public class NewPositionerTestsStretches : PositionerTestsBase
    {
        protected override IPositioner GetNewPositioner() => new NewPositioner();

        static readonly Rectangle R1X1 = new Rectangle(0, 0, 1, 1);
        static readonly Rectangle R4X1 = new Rectangle(0, 0, 4, 1);
        static readonly Rectangle R1X4 = new Rectangle(0, 0, 1, 4);
        static readonly Rectangle R4X4 = new Rectangle(0, 0, 4, 4);
        static readonly Rectangle R4X2 = new Rectangle(0, 0, 4, 2);
        static readonly Rectangle R2X4 = new Rectangle(0, 0, 2, 4);

        [TestMethod]
        public void Stretch2RectsHoriz() =>
            Test(new[] { R1X1, R1X1.MoveBy(1, 0) }.StretchToContainer(R4X1),
                new[] { R1X1, R1X1.MoveBy(3, 0) },
                string.Empty);

        [TestMethod]
        public void Stretch2RectsVert() =>
            Test(new[] { R1X1, R1X1.MoveBy(0, 1) }.StretchToContainer(R1X4),
                new[] { R1X1, R1X1.MoveBy(0, 3) },
                string.Empty);

        [TestMethod]
        public void Stretch2RectsSquare() =>
            Test(new[] { R1X1, R1X1.MoveBy(1, 1) }.StretchToContainer(R4X4),
                new[] { R1X1, R1X1.MoveBy(3, 3) },
                string.Empty);

    }
}
