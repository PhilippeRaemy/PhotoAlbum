namespace PictureHandler
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;

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
                Trace.WriteLine($"Reading image from {imageFullPathName}");
                try
                {
                    await fStream.CopyToAsync(mStream).ConfigureAwait(false);
                    mStream.Seek(0, SeekOrigin.Begin);
                    return await Task.Run(() => Image.FromStream(mStream)).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"Reading image from {imageFullPathName} failed with {e}");
                    return null;
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