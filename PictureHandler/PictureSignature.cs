﻿namespace PictureHandler
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    public class PictureSignature
    {
        readonly int _size;
        public List<ushort> Signature { get; }

        public PictureSignature(FileInfo fileInfo, int size, ushort levels)
        {
            _size = size;
            var image = PictureHelper.ReadImageFromStream(fileInfo);
            //if (image.Width > image.Height)
            //    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            // var bmp = new Bitmap(image, size, size);
            var bmp = new Bitmap(size, size);
            var g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, 0, 0, size, size);
            bmp.Save(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".bmp"), ImageFormat.Bmp);
            Signature =  Enumerable.Range(0, size)
                .SelectMany(x => Enumerable.Range(0, size)
                    .Select(y => (ushort) Math.Round(bmp.GetPixel(x, y).GetBrightness() * levels))
                )
                .ToList();
        }


        public bool Equals(PictureSignature other, double tolerance) =>
            other != null &&
            (
                ReferenceEquals(this, other)
                || _size == other._size && GetSimilarityWith(other) >= tolerance
            );

        public double GetSimilarityWith(PictureSignature other) =>
            other == null ? 0
            : ReferenceEquals(this, other) ? 1
            : _size != other._size ? 0
            : Signature.Zip(other.Signature, (a, b) => a == b)
                .Count(t => t) * 1.0 / _size / _size;

    }
}