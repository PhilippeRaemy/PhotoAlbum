using System;
using System.IO;
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

            internal FileFixtures()
            {
                TempFolder = new DirectoryInfo(Guid.NewGuid().ToString());
                TempFolder.Create();
                foreach (var sample in new[] { "Sample", "MultiSamples" })
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
            using (var fFix = new FileFixtures())
            {
                Program.Main(new[]
                {
                    $"--RootPath={fFix.TempFolder.FullName}",
                    "--deduplicate",
                    "--recurse",
                    "--NoRecycleBin",
                    "--verbose"
                });
            }
        }
    }
}
