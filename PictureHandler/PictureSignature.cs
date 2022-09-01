namespace PictureHandler
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Signature = System.Collections.Generic.List<ushort>;

    public class PictureSignatureComparer : IEqualityComparer<PictureSignature>
    {
        public bool Equals(PictureSignature x, PictureSignature y)
         => x?.Equals(y) ?? false;

        public int GetHashCode(PictureSignature obj)
            => obj?.Signature?.GetHashCode() ?? int.MinValue;
    }

    public class PictureSignature: IEquatable<PictureSignature>
    {
        readonly int _size;
        readonly ushort _levels;
        Signature _signature;
        public FileInfo FileInfo { get; }
        public Signature Signature => _signature ?? GetSignature();
        public async Task<Signature> SignatureAsync() { return _signature ?? await GetSignatureAsync(); }

        SelectablePictureBox _pictureBox;
        public SelectablePictureBox PictureBox
        {
            get => _pictureBox;
            set => _pictureBox = value;
        }

        public Point Location { get; private set; }
        bool _selected;

        public bool Selected
        {
            get => _pictureBox?.Selected ?? _selected;
            set
            {
                _selected = value;
                if (_pictureBox is null) return;
                _pictureBox.Selected = value;
            }
        }

        public PictureSignature SetLocation(int x, int y)
        {
            Location = new Point(x, y);
            return this;
        }

        public PictureSignature(FileInfo fileInfo, int size, ushort levels, bool selected)
        {
            _size = size;
            _levels = levels;
            _selected = selected;
            FileInfo = fileInfo;
        }

        public Signature GetSignature(Action<PictureSignature> feedback = null)
        {
            var image = PictureHelper.ReadImageFromFileInfo(FileInfo);
            //if (image.Width > image.Height)
            //    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            // var bmp = new Bitmap(image, size, size);
            var bmp = new Bitmap(_size, _size);
            var g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, 0, 0, _size, _size);
            bmp.Save(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".bmp"), ImageFormat.Bmp);
            var results = Enumerable.Range(0, _size)
                .SelectMany(x => Enumerable.Range(0, _size)
                    .Select(y => (ushort)Math.Round(bmp.GetPixel(x, y).GetBrightness() * _levels)))
                .ToList();
            feedback?.Invoke(this);
            _signature = results;
            return results;
        }

        public async Task<List<ushort>> GetSignatureAsync(Action<PictureSignature> feedback = null)
        {
            var image = await PictureHelper.ReadImageFromFileInfoAsync(FileInfo);
            //if (image.Width > image.Height)
            //    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            // var bmp = new Bitmap(image, size, size);
            var bmp = new Bitmap(_size, _size);
            var g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, 0, 0, _size, _size);
            bmp.Save(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".bmp"), ImageFormat.Bmp);
            var results = Enumerable.Range(0, _size)
                .SelectMany(x => Enumerable.Range(0, _size)
                    .Select(y => (ushort)Math.Round(bmp.GetPixel(x, y).GetBrightness() * _levels)))
                .ToList();
            feedback?.Invoke(this);
            _signature = results;
            return results;
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

        public double GetSimilarityWith(PictureSignature other)
            =>
                other == null ? 0
                : ReferenceEquals(this, other) ? 1
                : _size != other._size ? 0
                : Signature.Zip(other.Signature, (a, b) => a == b)
                .Count(t => t) * 1.0 / _size / _size;

        public async Task<double> GetSimilarityWithAsync(PictureSignature other)
            =>
                other == null ? 0
                : ReferenceEquals(this, other) ? 1
                : _size != other._size ? 0
                : (await SignatureAsync()).Zip(await other.SignatureAsync(), (a, b) => a == b)
                    .Count(t => t) * 1.0 / _size / _size;

        public bool Equals(PictureSignature other)
        {
            return GetSimilarityWith(other) >= .999;
        }
    }
    
}
