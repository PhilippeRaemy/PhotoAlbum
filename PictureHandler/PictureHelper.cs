
namespace PictureHandler
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;

    public static class PictureHelper
    {
        public static Image ReadImageFromFileInfo(FileInfo imageFullPathName)
        {
            if (!imageFullPathName.Exists) return null;
            using (var stream = new FileStream(imageFullPathName.FullName, FileMode.Open, FileAccess.Read))
            {
                Trace.WriteLine($"Reading image from {imageFullPathName}");
                return Image.FromStream(stream);
            }
        }

        public static async Task<Image> ReadImageFromFileInfoAsync(FileInfo imageFullPathName) 
            => await Task.Run(() => ReadImageFromFileInfo(imageFullPathName)).ConfigureAwait(false);
    }
}
