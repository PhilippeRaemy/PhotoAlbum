namespace TestsPicturesComparer
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PictureHandler;

    [TestClass]
    public class SignatureTests
    {
        const string Jpg = @"Sample\Sample.jpg";

        [TestMethod]
        public void BasicLoadSignature()
        {
            var fileInfo = new FileInfo(Jpg);
            var signature = PictureHelper.ComputeSignature(fileInfo);
        }
    }
}
