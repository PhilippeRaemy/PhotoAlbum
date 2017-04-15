
namespace PictureHandlerTest

{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Media.Imaging;
    using PictureHandler;

    [TestClass]
    public class PictureHandlerTest
    {
        const string Jpg = @"Sample\Sample.jpg";

        [TestMethod]
        public void ReadPictureMetadata()
        {
            var metadata = new JpegMetadataAdapter(Jpg).Metadata;
            TraceMetadata(metadata);
        }
        [TestMethod]
        public void ValidatePictureMetadata()
        {
            ValidateMetadataImpl(Jpg, "Sample Title", "Sample Comments");
        }

        static void ValidateMetadataImpl(string jpg, string title, string comment)
        {
            var metadata = new JpegMetadataAdapter(jpg).Metadata;
            Assert.AreEqual(title, metadata.Title);
            Assert.AreEqual(comment, metadata.Comment);
            TraceMetadata(metadata);
        }

        [TestMethod]
        public void SavePictureMetadata()
        {
            const string title = "Another Title";
            const string comment = "Another Comment";

            var adapter = new JpegMetadataAdapter(Jpg);
            var metadata = adapter.Metadata;
            metadata.Title = title;
            metadata.Comment = comment;
            const string anothersampleJpg = @"Sample\AnotherSample.jpg";
            adapter.SaveAs(anothersampleJpg);
            ValidateMetadataImpl(anothersampleJpg, title, comment);
        }

        static void TraceMetadata(BitmapMetadata metadata)
        {
            foreach (var prop in typeof(BitmapMetadata).GetProperties())
            {
                Trace.WriteLine($"{prop.Name} : {prop.GetValue(metadata)}");
            }
        }

        [TestMethod]
        public void TestReadImage()
        {
            var img = Image.FromFile(Jpg);
            foreach (var imgPropertyItem in img.PropertyItems)
            {
                var value = imgPropertyItem.Type == 2 ? System.Text.Encoding.Default.GetString(imgPropertyItem.Value)
                          : imgPropertyItem.Type == 1 ? System.Text.Encoding.Unicode.GetString(imgPropertyItem.Value)
                          : string.Empty;
                Trace.WriteLine($"{imgPropertyItem.Id.ToString("X")} - {imgPropertyItem.Len} - {imgPropertyItem.Type} - {value}");
            }
        }
    }
}
