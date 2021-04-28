
namespace PictureHandler
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Text;

    public static class PictureHelper
    {
        public static Image ReadImageFromStream(FileInfo imageFullPathName)
        {
            if (!imageFullPathName.Exists) return null;
            using (var stream = new FileStream(imageFullPathName.FullName, FileMode.Open, FileAccess.Read))
            {
                Trace.WriteLine($"ImageHost reading from {imageFullPathName}");
                return Image.FromStream(stream);
            }
        }


        public static string ComputeSignature(FileInfo fileInfo)
        {
            var bmp = new Bitmap(1, 1);
            var image = ReadImageFromStream(fileInfo);
            var xScale = image.Width / 100.0;
            var yScale = image.Height / 100.0;
            var sb = new StringBuilder();

            using (var g = Graphics.FromImage(bmp))
            {
                for (var x = 0; x < 100; x++)
                for (var y = 0; y < 100; y++)
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, new Rectangle(0, 0, 1, 1), new Rectangle((int)(x * xScale), (int)(y * yScale),
                        (int)Math.Round(xScale), (int)Math.Round(yScale)), GraphicsUnit.Pixel);
                    var pixel = bmp.GetPixel(0, 0);
                    sb.Append($"{pixel.R:X}{pixel.G:X}{pixel.B:X} ");
                }

            }

            return sb.ToString();
        }
    }
}
