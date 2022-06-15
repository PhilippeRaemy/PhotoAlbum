﻿namespace PictureHandler
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class PictureSignature
    {
        readonly int _size;
        readonly ushort _levels;
        public List<ushort> Signature { get; }

        public PictureSignature(FileInfo fileInfo, int size, ushort levels)
        {
            _size = size;
            _levels = levels;
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

        public override string ToString()
        {
            var (format, sep) = _levels <= 16 ? ("x1", false)
                : _levels <= 256 ? ("x2", true)
                : _levels <= 4096 ?("x3", true)
                : ("x4", true);
            var sb = new StringBuilder();
            foreach (var i in Signature)
            {
                sb.Append(i.ToString(format));
                if (sep) sb.Append("-");
            }
            return sb.ToString();
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
