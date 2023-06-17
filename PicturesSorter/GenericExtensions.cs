namespace PicturesSorter
{
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.Drawing;
    using System.Linq;
    using MoreLinq;

    internal static class GenericExtensions
    {
        static LinkedListNode<T> SafeNext<T>(this LinkedListNode<T> lln) => lln?.Next ?? lln?.List?.First;
        static LinkedListNode<T> SafePrev<T>(this LinkedListNode<T> lln) => lln?.Previous ?? lln?.List?.Last;

        public static LinkedListNode<T> SafeStep<T>(this LinkedListNode<T> lln, int step)
            => step < 0 ? lln?.SafePrev()
                : step > 0 ? lln?.SafeNext()
                : lln;

        public static int IndexOf<T>(this LinkedList<T> ll, T lln) where T : class
        {
            var theOne = ll.Index().FirstOrDefault(n => n.Value.Equals(lln));
            return theOne.Value == null ? -1 : theOne.Key;
        }

        static Image Resize(this Image img, Size size)
        {
            var nPercentW = size.Width / (float)img.Width;
            var nPercentH = size.Height / (float)img.Height;
            var nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;
            using (var b = new Bitmap((int)(img.Width * nPercent), (int)(img.Height * nPercent)))
            using (var g = Graphics.FromImage(b))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, (int)(img.Width * nPercent), (int)(img.Height * nPercent));
                return b;
            }
        }
    }
}