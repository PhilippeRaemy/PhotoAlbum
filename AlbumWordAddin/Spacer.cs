namespace AlbumWordAddin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VstoEx.Extensions;
    using VstoEx.Geometry;

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

        public static IEnumerable<Rectangle> SpacingInterpolate(IEnumerable<Rectangle> rectangles)
            => HorizontalEqualSpacing(VerticalEqualSpacing(rectangles));

        static IEnumerable<Rectangle> EqualSpacingImpl(IEnumerable<Rectangle> rectangles,
            Func<Rectangle, float> positionFunc,
            Func<Rectangle, float> sizeFunc,
            Func<Rectangle, float, Rectangle> positionerFunc
        )
        {
            var rectA = rectangles.CheapToArray();
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
            => IncreaseSpaceImpl(rectangles, HorizontalGridUnit, (r, p) => r.MoveBy(p, 0));

        public static IEnumerable<Rectangle> DecreaseHorizontal(IEnumerable<Rectangle> rectangles)
            => IncreaseSpaceImpl(rectangles, -HorizontalGridUnit, (r, p) => r.MoveBy(p, 0));

        public static IEnumerable<Rectangle> IncreaseVertical(IEnumerable<Rectangle> rectangles)
            => IncreaseSpaceImpl(rectangles, VerticalGridUnit, (r, p) => r.MoveBy(0, p));

        public static IEnumerable<Rectangle> DecreaseVertical(IEnumerable<Rectangle> rectangles)
            => IncreaseSpaceImpl(rectangles, -VerticalGridUnit, (r, p) => r.MoveBy(0, p));

        static IEnumerable<Rectangle> IncreaseSpaceImpl(IEnumerable<Rectangle> rectangles, float gridUnit,
            Func<Rectangle, float, Rectangle> positionerFunc)
        {
            var space = -gridUnit;
            return rectangles.Select(r => positionerFunc(r, space += gridUnit));
        }

    }
}