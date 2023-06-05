namespace PictureHandler
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
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
        private readonly TimeSpan _loadPictureTimeout = TimeSpan.FromSeconds(5);
        Signature _signature;
        Signature _flippedSignature;

        public FileInfo FileInfo { get; }
        public Signature Signature
        {
            get
            {
                var sign = GetSignatureAsync(_loadPictureTimeout);
                sign.Wait();
                return sign.Result;
            }
        }

        public Signature FlippedSignature => _flippedSignature ?? (_flippedSignature = Signature.Flip());

        public SelectablePictureBox PictureBox { get; set; }

        public Point Location { get; private set; }
        bool _selected;

        public bool Selected
        {
            get => PictureBox?.Selected ?? _selected;
            set
            {
                _selected = value;
                if (PictureBox is null) return;
                PictureBox.Selected = value;
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

        public PictureSignature(Image image, int size, ushort levels, bool selected)
        {
            _size = size;
            _levels = levels;
            _selected = selected;
            SetSignatureFromImage(image);
        }

        async Task Delay(TimeSpan timeout, Task other, string context) {
            await Task.Delay(timeout);
            if (other.IsCompleted)
            {
                Console.WriteLine($"{context} succeeded before timeout!");

                return;
            }
            Console.WriteLine($"{context} Timed-out!");
        }


        public async Task<Signature> GetSignatureAsync(TimeSpan timeout, Action<PictureSignature> feedback = null) {
            var task = GetSignatureAsync(feedback);
            if(await Task.WhenAny(task, Delay(timeout, task, $"GetSignatureAsync {FileInfo.FullName}" )) != task) return null;
            feedback?.Invoke(this);
            return await task;
        }


        /// <summary>
        /// Obtains asynchronously the signature of the picture, if it is not already cached
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        public async Task<Signature> GetSignatureAsync(Action<PictureSignature> feedback = null)
        {
            if (_signature is null)
                using (var image = await PictureHelper.ReadImageFromFileInfoAsync(FileInfo))
                    if (image != null)
                        try
                        {
                            SetSignatureFromImage(image);
                        }
                        catch (Exception e)
                        {
                            Debug.Assert(false);
                        }

            feedback?.Invoke(this);
            return _signature;
        }

        void SetSignatureFromImage(Image image)
        {
            if(image != null)
                using (var bmp = new Bitmap(_size, _size))
                using (var g = Graphics.FromImage(bmp))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, 0, 0, _size, _size);
                    if (image.Width > image.Height) bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    // bmp.Save(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".bmp"), ImageFormat.Bmp);
                    _signature = Enumerable.Range(0, _size)
                        .SelectMany(x => Enumerable.Range(0, _size)
                            .Select(y => (ushort)Math.Round(bmp.GetPixel(x, y).GetBrightness() * _levels)))
                        .ToList();
                }
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
        {
            if (FileInfo is null || other.FileInfo is null) return 0;
            var sim = GetSimilarityWithAsync(other);
            sim.Wait();
            return sim.Result;
        }

        public async Task<double> GetSimilarityWithAsync(PictureSignature other)
        {
            if(other == null) return 0;
            if (ReferenceEquals(this, other)) return 1;
            if (_size != other._size) return 0;
            var signature = GetSignatureAsync(_loadPictureTimeout);
            var otherSignature = other.GetSignatureAsync(_loadPictureTimeout);
            await Task.WhenAll(signature, otherSignature);

            return signature.Result is null || otherSignature.Result is null
                ? 0
                : new[]
            {
                signature.Result.Zip(otherSignature.Result, (a, b) => a == b).Count(t => t) * 1.0 / _size / _size,
                FlippedSignature.Zip(otherSignature.Result, (a, b) => a == b).Count(t => t) * 1.0 / _size / _size
            }.Max();
        }

        public bool Equals(PictureSignature other) 
            => GetSimilarityWith(other) >= .999;
    }

    public static class SignatureExtensions
    {
        public static Signature Flip(this Signature sign)
        {
            var flipped = sign.Select(x => x).ToList();
            flipped.Reverse();
            return flipped;
        }
    }

}
