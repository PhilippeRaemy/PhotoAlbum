namespace AlbumWordAddin
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Spacer
    {
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
    }
}