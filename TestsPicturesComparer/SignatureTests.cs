namespace TestsPicturesComparer
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PictureHandler;

    [TestClass]
    public class SignatureTests
    {
        const string Jpg = @"Sample\Sample.jpg";
        const string JpgSmall = @"Sample\SampleSmall.jpg";

        static List<T> TraceSignature<T>(List<T> signature)
        {
            var sb = new StringBuilder();
            foreach (var i in signature)
                sb.Append(i);
            Trace.WriteLine(sb.ToString());
            return signature;
        }

        [TestMethod]
        public void BasicLoadSignature() => TraceSignature(PictureHelper.ComputeSignature(new FileInfo(Jpg), 16, 4));

        [TestMethod]
        public void CompareSignature()
        {
            var size = 3;
            var sign      = TraceSignature(PictureHelper.ComputeSignature(new FileInfo(Jpg), size, 2));
            var signSmall = TraceSignature(PictureHelper.ComputeSignature(new FileInfo(JpgSmall), size, 2));

            Assert.IsTrue(sign.Zip(signSmall, (a, b) => a==b).Count(t => t) * 1.0 / size / size > .99);
        }
    }
}
