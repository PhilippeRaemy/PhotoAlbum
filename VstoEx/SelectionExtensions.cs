namespace VstoEx
{
    public static class SelectionExtensions
    {
        public static int GetPageNumber(this Microsoft.Office.Interop.Word.Selection selection)
        {
            return selection.Range.GetPageNumber();
        }
    }
}