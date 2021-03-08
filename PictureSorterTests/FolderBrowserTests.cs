using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using FolderWalker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;

namespace TestsPicturesSorter
{
    [TestClass]
    public class FolderBrowserTests
    {
        DirectoryInfo _root;
        string[] _tree;

        [TestInitialize]
        public void TestInitialize()
        {
            _root = new DirectoryInfo(new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)).AbsolutePath);
            _tree = new[]
            {
                @"A",
                @"A\1",
                @"A\2",
                @"A\3",
                @"A\3\a",
                @"A\3\b",
                @"A\3\c",
                @"A\4",
                @"B",
                @"B\1",
                @"B\1\a",
                @"B\1\a\x",
            };
            _tree.OrderBy(n=>n).Pipe(d => Trace.WriteLine($"Init: {d}")).ForEach(n=> new DirectoryInfo(Path.Combine(_root.FullName, n)).Create());
        }

        [TestCleanup]
        void TestCleanup()
        {
            _tree.OrderByDescending(n => n).Pipe(d=>Trace.WriteLine($"Cleanup: {d}")).ForEach(n => new DirectoryInfo(Path.Combine(_root.FullName, n)).Delete());
        }

        [TestMethod]
        public void TestNextFolder()
        {
            var tree = _root.EnumerateDirectories("*", SearchOption.AllDirectories)
                .Where(d => (d.Attributes & (
                    FileAttributes.ReparsePoint |
                    FileAttributes.Hidden |
                    FileAttributes.System
                    )) == 0)
                .OrderBy(d => d.FullName)
                .ToArray();
            var nextDi = _root;
            foreach (var di in tree)
            {
                Trace.WriteLine(di.FullName);
                nextDi = nextDi.WalkNextFolder(FolderDirection.Forward);
                Assert.AreEqual(di.FullName, nextDi.FullName);
            }
        }
        [TestMethod]
        public void TestPreviousFolder()
        {
            var tree = _root.EnumerateDirectories("*", SearchOption.AllDirectories)
                .Where(d => (d.Attributes & (
                    FileAttributes.ReparsePoint |
                    FileAttributes.Hidden |
                    FileAttributes.System
                    )) == 0)
                .OrderByDescending(d => d.FullName)
                .Pipe(d => Trace.WriteLine(d.FullName))
                .ToArray();
            var nextDi = tree.First();
            foreach (var di in tree.Skip(1))
            {
                Trace.WriteLine(di.FullName);
                nextDi = nextDi.WalkNextFolder(FolderDirection.Backward);
                Assert.AreEqual(di.FullName, nextDi.FullName);
            }
        }
    }
}
