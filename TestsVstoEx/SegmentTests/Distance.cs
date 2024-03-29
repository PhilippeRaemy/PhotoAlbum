﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VstoEx.Geometry;

namespace TestsVstoEx.SegmentTests
{
    [TestClass]
    public class Distance
    {
        [TestMethod]
        public void NonOverlapingRighterSegment()
        {
            Assert.AreEqual(1f, new Segment(0, 1).DistanceTo(new Segment(2, 3)));
        }

        [TestMethod]
        public void NonOverlapingLefterSegment()
        {
            Assert.AreEqual(1f, new Segment(2, 3).DistanceTo(new Segment(0, 1)));
        }

        [TestMethod]
        public void OverlapingRighterSegment()
        {
            Assert.AreEqual(-1f, new Segment(0, 3).DistanceTo(new Segment(2, 4)));
        }

        [TestMethod]
        public void OverlapingLefterSegment()
        {
            Assert.AreEqual(-1f, new Segment(2, 4).DistanceTo(new Segment(0, 3)));
        }

        [TestMethod]
        public void IncludedSegment()
        {
            Assert.AreEqual(-4f, new Segment(0, 5).DistanceTo(new Segment(1, 2)));
        }

        [TestMethod]
        public void ContainingSegment()
        {
            Assert.AreEqual(-4f, new Segment(1, 2).DistanceTo(new Segment(0, 5)));
        }

        [TestMethod]
        public void DistanceIsCommutative()
        {
            var rnd = new Random((int)(DateTime.Now - DateTime.Today).TotalSeconds);
            for (var i = 0; i < 100; i++)
            {
                var s1 = new Segment(rnd.Next(0, 100), rnd.Next(0, 100));
                var s2 = new Segment(rnd.Next(0, 100), rnd.Next(0, 100));
                Assert.AreEqual(s1.DistanceTo(s2), s2.DistanceTo(s1), $"{s1} - {s2}");
                Assert.IsFalse(float.IsNaN(s1.DistanceTo(s2)));
            }
        }

        [TestMethod]
        public void DistanceToSelf()
        {
            var s = new Segment(2, 4);
            Assert.AreEqual(0, s.DistanceTo(s));
        }

        [TestMethod]
        public void SelfOverlap()
        {
            var s = new Segment(2, 4);
            Assert.IsTrue(s.Overlaps(s));
        }

        /*
                                [TestMethod]
                                public void TriangleInequality()
                                {
                                    var rnd = new Random((int)(DateTime.Now - DateTime.Today).TotalSeconds);
                                    for (var i = 0; i < 100; i++)
                                    {
                                        var s1 = new Segment(rnd.Next(0, 100), rnd.Next(0, 100));
                                        var s2 = new Segment(rnd.Next(0, 100), rnd.Next(0, 100));
                                        var s3 = new Segment(rnd.Next(0, 100), rnd.Next(0, 100));
                                        Assert.IsTrue(s1.DistanceTo(s3) <= s1.DistanceTo(s2) + s2.DistanceTo(s3)
                                            , $"{s1}.DistanceTo({s3}){{={s1.DistanceTo(s3)}}} <= {s1}.DistanceTo({s2}){{={s1.DistanceTo(s2)}}} + {s2}.DistanceTo({s2}){{{s1.DistanceTo(s3)}}}");
                                    }
                                }
                                */
    }
}
