namespace PictureHandler
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;
    using ImageMagick;

    public static class PictureHelper
    {
        public static async Task<Image> ReadImageFromFileInfoAsync(FileInfo imageFullPathName)
        {
            if (imageFullPathName is null) return null;
            imageFullPathName.Refresh();
            if (!imageFullPathName.Exists) return null;
            using (var fStream = new FileStream(imageFullPathName.FullName, FileMode.Open, FileAccess.Read))
            using (var mStream = new MemoryStream())
            {
                Trace.WriteLine($"Reading image from {imageFullPathName.FullName}");
                try
                {
                    await fStream.CopyToAsync(mStream).ConfigureAwait(false);
                    mStream.Seek(0, SeekOrigin.Begin);
                    return await Task.Run(() => Image.FromStream(mStream)).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"Reading image from {imageFullPathName} failed with {e}");
                    try
                    {
                        return LoadWebP(imageFullPathName.FullName);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine($"Reading image from {imageFullPathName} failed with {ex}");
                        return null;
                    }
                }
            }
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