namespace VstoEx
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Office.Interop.Word;

    public static class ShapeExtensions
    {
        public static int GetPageNumber(this Shape shape)
        {
            return shape.Anchor.GetPageNumber();
        }

        public static IEnumerable<Shape> ReplaceSelection(this IEnumerable<Shape> shapes)
        {
            var replace = true;
            var ashapes = shapes.ToArray();
            foreach (var shape in ashapes)
            {
                shape.Select(replace);
                replace = false;
                yield return shape;
            }
        }
    }
}
