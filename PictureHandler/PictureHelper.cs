
namespace PictureHandler
{
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;

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

        async public static Task<Image> ReadImageFromStreamAsync(FileInfo imageFullPathName)
         => await new Task<Image>(() => ReadImageFromStream(imageFullPathName));
    }
}
