namespace PictureHandler
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;
    using ImageMagick;
    using Tracer;

    public static class PictureHelper
    {
        public static async Task<Image> ReadImageFromFileInfoAsync(FileInfo file)
        {
            if (file is null) return null;
            file.Refresh();
            if (!file.Exists) return null;
            Tracer.WriteLine(() => $"Reading image from {file.FullName}");
            Image image=null;
            using (var mStream = new MemoryStream())
            {
                // we want to make sure the file stream is closed before we return the image
                using (var fStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                    await fStream.CopyToAsync(mStream).ConfigureAwait(false);
                /* TODO : determine if we could load all images through LoadWebP */
                try
                {
                    mStream.Seek(0, SeekOrigin.Begin);
                    image = Image.FromStream(mStream);
                }
                catch (Exception e)
                {
                    Tracer.WriteLine(() => $"Reading image from {file} failed with {e}");
                    try
                    {
                        image = LoadWebP(file.FullName);
                    }
                    catch (Exception ex)
                    {
                        Tracer.WriteLine(() => $"Reading image from {file} failed with {ex}");
                    }
                }
            }
            return image;
        }

        static Image LoadWebP(string path)
        {
            using (var magickImage = new MagickImage(path))
            {
                // Convert MagickImage to a memory stream in PNG format (or any other format)
                using (var ms = new MemoryStream())
                {
                    magickImage.Write(ms, MagickFormat.Jpg);
                    return Image.FromStream(ms);
                }
            }
        }

        public static Image ReadImageFromFileInfo(FileInfo imageFullPathName)
        {
            var readImageFromFileInfoAsync = ReadImageFromFileInfoAsync(imageFullPathName);
            readImageFromFileInfoAsync.ConfigureAwait(false);
            readImageFromFileInfoAsync.Wait();
            return readImageFromFileInfoAsync.Result;
        }
    }
}