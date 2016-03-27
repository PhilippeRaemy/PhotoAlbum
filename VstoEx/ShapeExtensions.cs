namespace VstoEx
{
    public static class ShapeExtensions
    {
        public static int GetPageNumber(this Microsoft.Office.Interop.Word.Shape shape)
        {
            return shape.Anchor.GetPageNumber();
        }
    }
}
