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

        static PictureSignature TraceSignature(PictureSignature ps)
        {
            var sb = new StringBuilder();
            foreach (var i in ps.Signature)
                sb.Append(i);
            Trace.WriteLine(sb.ToString());
            return ps;
        }

        [TestMethod]
        public void BasicLoadSignature() => TraceSignature(new PictureSignature(new FileInfo(Jpg), 16, 4, .99));

        [TestMethod]
        public void CompareSignature()
        {
            var size = 25;
            var sign      = TraceSignature(new PictureSignature(new FileInfo(Jpg), size, 2, .99));
            var signSmall = TraceSignature(new PictureSignature(new FileInfo(JpgSmall), size, 2, .99));
            Assert.AreEqual(sign, signSmall);
        }
    }
}
