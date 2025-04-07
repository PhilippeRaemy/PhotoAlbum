using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PictureProcessor;

namespace TestsPicturesComparer
{
    [TestClass]
    public class TestCommandLineScenarios
    {
        internal class FileFixtures : IDisposable
        {
            public DirectoryInfo TempFolder { get; private set; }

            internal FileFixtures(params string[] folders)
            {
                TempFolder = new DirectoryInfo(Guid.NewGuid().ToString());
                TempFolder.Create();
                foreach (var sample in folders)
                {
                    var origFolder = new DirectoryInfo(sample);
                    var subFolder = new DirectoryInfo(Path.Combine(TempFolder.FullName, sample));
                    subFolder.Create();
                    foreach (var file in origFolder.GetFiles()) 
                        file.CopyTo(Path.Combine(subFolder.FullName, file.Name));
                }
            }

            public void Dispose() => TempFolder.Delete(true);
        }

        [TestMethod]
        public void TestCommandLineDeduplicate()
        {
            using (var fFix = new FileFixtures("MultiSamples"))
            {
                Program.Main(new[]
                {
                    $"--RootPath={fFix.TempFolder.FullName}",
                    "--deduplicate",
                    "--recurse",
                    "--NoRecycleBin",
                    "--verbose"
                });
                var allFiles = new DirectoryInfo(fFix.TempFolder.FullName)
                    .EnumerateFiles("*", SearchOption.AllDirectories).Select(fi => fi.Name)
                    .ToHashSet(StringComparer.InvariantCultureIgnoreCase);
                var expected =
                    new[] { "20220107_121343_Philippe_Large(1).jpg", "20220107_093431_IMG_Large.jpg", "Sample.jpg" }
                        .ToHashSet(StringComparer.InvariantCultureIgnoreCase);
                Assert.AreEqual(expected, allFiles);
            }
        }
    }
}
