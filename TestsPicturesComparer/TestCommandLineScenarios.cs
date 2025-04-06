using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using MoreLinq;

namespace TestsPicturesComparer
{
    internal class TestCommandLineScenarios
    {
        internal class FileFixtures : IDisposable
        {
            public DirectoryInfo TempFolder { get; private set; }

            internal FileFixtures()
            {
                TempFolder = new DirectoryInfo(Guid.NewGuid().ToString());
                TempFolder.Create();
                foreach (var sample in new[] { "Sample", "MultiSample" })
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

    }
}
