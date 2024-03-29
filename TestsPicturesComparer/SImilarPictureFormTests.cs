﻿namespace TestsPicturesComparer
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using static MoreLinq.Extensions.PipeExtension;
    using static MoreLinq.Extensions.ForEachExtension;
    using PicturesSorter;

    [TestClass]
    public class SimilarPictureFormTests
    {
        const string Jpg = @"Sample\Sample.jpg";
        const string JpgSmall = @"Sample\SampleSmall.jpg";

        readonly DirectoryInfo _testFolder = new DirectoryInfo(Path.GetTempFileName());


        [TestInitialize]
        public void Initialize()
        {
            File.Delete(_testFolder.FullName);
            _testFolder.Create();
            new DirectoryInfo("MultiSamples")
                .GetFiles()
                .ForEach(fi => fi.CopyTo(Path.Combine(_testFolder.FullName, fi.Name), true));
        }

        [TestCleanup]
        public void Cleanup() => _testFolder.Delete(true);

        [TestMethod]
        public void TestDeletePictures()
        {
            TestDeletePictureImpl(Keys.Delete | Keys.Shift);
        }

        [TestMethod]
        public void TestStagePictures()
        {
            TestDeletePictureImpl(Keys.Delete);
        }

        void TestDeletePictureImpl(Keys keyData)
        {
            var files = _testFolder.GetFiles();
            var form = new SimilarPicturesForm();

            form.LoadPictures(_testFolder);
            // form.ShowDialog();
            SimilarPicturesForm.MuteDialogs = true;
            form.SimilarPicturesForm_KeyUp(form.PanelMain, new KeyEventArgs(keyData)); 
            // form.buttonGo_Click(null, null);
            form.Close();
            Task.Delay(250).Wait();
            foreach (var fileInfo in files)
            {
                fileInfo.Refresh();
                switch (fileInfo.Name)
                {
                    case @"20220107_093431_IMG_Large.jpg":
                        Assert.IsTrue(fileInfo.Exists);
                        break;
                    case @"20220107_093431_IMG_Middle.jpg":
                        Assert.IsFalse(fileInfo.Exists);
                        break;
                    case @"20220107_093431_IMG_Small.jpg":
                        Assert.IsFalse(fileInfo.Exists);
                        break;
                    case @"20220107_121343_Philippe_Large(1).jpg":
                        Assert.IsTrue(fileInfo.Exists);
                        break;
                    case @"20220107_121343_Philippe_Large(2).jpg":
                        Assert.IsFalse(fileInfo.Exists);
                        break;
                }
            }

            _testFolder.GetFiles().Pipe(Console.WriteLine);
            Trace.WriteLine("test done!");
        }
    }
}
