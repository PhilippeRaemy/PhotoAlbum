/*
 * licence https://opensource.org/licenses/MIT
 * source http://stackoverflow.com/questions/1755185/how-to-add-comments-to-a-jpeg-file-using-c-sharp
 * author http://stackoverflow.com/users/777939/martin-eden
 */
namespace PictureHandler
{
    using System.IO;
    using System.Windows.Media.Imaging;
    using System.Drawing;

    public class JpegMetadataAdapter
    {
        readonly string _path;
        readonly BitmapFrame _frame;
        public readonly BitmapMetadata Metadata;

        public JpegMetadataAdapter(string path)
        {
            _path = path;
            _frame = getBitmapFrame(path);
            Metadata = (BitmapMetadata)_frame.Metadata?.Clone();
        }

        public void Save()
        {
            SaveAs(_path);
        }

        public void SaveAs(string path)
        {
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(_frame, _frame.Thumbnail, Metadata, _frame.ColorContexts));
            using (Stream stream = File.Open(path, FileMode.Create, FileAccess.ReadWrite))
            {
                encoder.Save(stream);
            }
        }

        BitmapFrame getBitmapFrame(string path)
        {
            BitmapDecoder decoder;
            using (Stream stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                decoder = new JpegBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            }
            //var img = (System.Drawing.Image) decoder.Frames[0];
            return decoder.Frames[0];
        }
    }
}
