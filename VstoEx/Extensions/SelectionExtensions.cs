namespace VstoEx.Extensions
{
    using Microsoft.Office.Interop.Word;

    public static class SelectionExtensions
    {
        public static int GetPageNumber(this Selection selection)
        {
            return selection.Range.GetPageNumber();
        }
    }
}