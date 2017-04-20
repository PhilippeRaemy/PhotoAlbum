namespace AlbumWordAddin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Spacer
    {
        public const int HorizontalGridUnit = 5;
        public const int VerticalGridUnit   = 5;

        public static IEnumerable<Rectangle> HorizontalEqualSpacing(IEnumerable<Rectangle> rectangles)
            => EqualSpacingImpl(
                rectangles,
                r => r.Left,
                r => r.Width,
                (r, p) => r.MoveTo(p, r.Top)
            );

        public static IEnumerable<Rectangle> VerticalEqualSpacing(IEnumerable<Rectangle> rectangles)
            => EqualSpacingImpl(
                rectangles,
                r => r.Top,
                r => r.Height,
                (r, p) => r.MoveTo(r.Left, p)
            );

        static IEnumerable<Rectangle> EqualSpacingImpl(IEnumerable<Rectangle> rectangles,
            Func<Rectangle, float> positionFunc,
            Func<Rectangle, float> sizeFunc,
            Func<Rectangle, float, Rectangle> positionerFunc
        )
        {
            var rectA = rectangles as Rectangle[] ?? rectangles.ToArray();
            var count = rectA.Length;
            if (count == 0) yield break;
            yield return rectA[0];
            if (count == 2)
            {
                yield return rectA[1];
                yield break;
            }
            var interSpace = (positionFunc(rectA.Last()) - positionFunc(rectA[0]) - sizeFunc(rectA[0]) -
                             rectA.Skip(1).Take(count - 2).Sum(sizeFunc))
                             / (count - 1);
            var position = positionFunc(rectA[0]);
            for (var i = 1; i < rectA.Length; i++)
            {
                position += sizeFunc(rectA[i - 1]) + interSpace;
                yield return positionerFunc(rectA[i], position);
            }
        }

        public static IEnumerable<Rectangle> IncreaseHorizontal(IEnumerable<Rectangle> rectangles)
        {
            return rectangles;
        }

    }
}