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

        public static Shape[] ReplaceSelection(this IEnumerable<Shape> shapes)
            => ReplaceSelectionImpl(shapes).ToArray();

        static IEnumerable<Shape> ReplaceSelectionImpl(this IEnumerable<Shape> shapes)
        {
            var replace = true;
            foreach (var shape in shapes)
            {
                shape.Select(replace);
                replace = false;
                yield return shape;
            }
        }
    }
}
