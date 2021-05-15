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
        public void BasicLoadSignature() => TraceSignature(new PictureSignature(new FileInfo(Jpg), 16, 4));

        void CompareSignatureImpl(int size, double similarity, ushort levels)
        {
            var sign = TraceSignature(new PictureSignature(new FileInfo(Jpg), size, levels));
            var signSmall = TraceSignature(new PictureSignature(new FileInfo(JpgSmall), size, levels));
            Assert.IsTrue(sign.GetSimilarityWith(signSmall) >= similarity);
        }

        [TestMethod] public void CompareSignature_005_99_2() => CompareSignatureImpl(005, .99, 2);
        [TestMethod] public void CompareSignature_025_99_2() => CompareSignatureImpl(025, .99, 2);
        [TestMethod] public void CompareSignature_005_99_4() => CompareSignatureImpl(005, .99, 4);
        [TestMethod] public void CompareSignature_025_99_4() => CompareSignatureImpl(025, .98, 4);

    }
}
