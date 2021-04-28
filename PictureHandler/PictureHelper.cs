﻿
namespace PictureHandler
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;

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


        public static List<ushort> ComputeSignature(FileInfo fileInfo, int size, ushort levels)
        {
            var image = ReadImageFromStream(fileInfo);
            var bmp = new Bitmap(image, size, size);
            return Enumerable.Range(0, size)
                .SelectMany(x => Enumerable.Range(0, size)
                    .Select(y => (ushort) Math.Round(bmp.GetPixel(x, y).GetBrightness() * levels))
                )
                .ToList();
        }
    }
}
