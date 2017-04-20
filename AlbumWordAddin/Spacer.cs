namespace AlbumWordAddin
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Spacer
    {
        public const int HorizontalGridUnit = 5;
        public const int VerticalGridUnit   = 5;
        public static IEnumerable<Rectangle> HorizontalEqualSpacing(IEnumerable<Rectangle> rectangles)
        {
            var rectA = rectangles as Rectangle[] ?? rectangles.ToArray();
            var count = rectA.Length;
            if(count==0) yield break;
            yield return rectA[0];
            if (count == 2)
            {
                yield return rectA[1];
                yield break;
            }
            var interSpace = (rectA.Last().Left - rectA[0].Left - rectA[0].Width -
                             rectA.Skip(1).Take(count - 2).Sum(r => r.Width))
                             /(count-1);
            var left = rectA[0].Left;
            for (var i = 1; i < rectA.Length; i++)
            {
                left += rectA[i - 1].Width + interSpace;
                yield return rectA[i].MoveTo(left, rectA[i].Top);
            }
        }

        public static IEnumerable<Rectangle> IncreaseHorizontal(IEnumerable<Rectangle> rectangles)
        {
            return rectangles;
        }

        public static IEnumerable<Rectangle> VerticalEqualSpacing(IEnumerable<Rectangle> rectangles)
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
            var interSpace = (rectA.Last().Top - rectA[0].Top - rectA[0].Height -
                             rectA.Skip(1).Take(count - 2).Sum(r => r.Height))
                             / (count - 1);
            var top = rectA[0].Top;
            for (var i = 1; i < rectA.Length; i++)
            {
                top += rectA[i - 1].Height + interSpace;
                yield return rectA[i].MoveTo(rectA[i].Left, top);
            }
        }
    }
}