
namespace PictureHandler
{
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;

    public static class PictureHelper
    {
        public static Image ReadImageFromStream(FileInfo imageFullPathName)
        {
            if (!imageFullPathName.Exists) return null;
            using (var stream = new FileStream(imageFullPathName.FullName, FileMode.Open, FileAccess.Read))
            {
                Trace.WriteLine($"Reading image from {imageFullPathName}");
                return Image.FromStream(stream);
            }
        }
    }
}
