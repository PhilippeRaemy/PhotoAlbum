
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
            imageFullPathName.Refresh();
            if (!imageFullPathName.Exists) return null;
            using (var stream = new FileStream(imageFullPathName.FullName, FileMode.Open, FileAccess.Read))
            {
                Trace.WriteLine($"Reading image from {imageFullPathName}");
                try
                {
                    return Image.FromStream(stream);
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"Reading image from {imageFullPathName} failed with {e}");
                    return null;
                }
            }
        }

        public static async Task<Image> ReadImageFromFileInfoAsync(FileInfo imageFullPathName) 
            => await Task.Run(() => ReadImageFromFileInfo(imageFullPathName)).ConfigureAwait(false);
    }
}
