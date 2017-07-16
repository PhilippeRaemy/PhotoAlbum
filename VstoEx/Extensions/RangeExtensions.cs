namespace VstoEx.Extensions
{
    public static class RangeExtensions
    {
        public static int GetPageNumber(this Microsoft.Office.Interop.Word.Range range)
        {
            return (int)range.Information[Microsoft.Office.Interop.Word.WdInformation.wdActiveEndPageNumber];
        }
    }
}