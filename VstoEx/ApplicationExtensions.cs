namespace VstoEx
{
    using Word = Microsoft.Office.Interop.Word;

    public static class ApplicationExtensions
    {
        public static StatePreserver StatePreserver(this Word.Application application) {
            return new StatePreserver(application);
        }
    }
}
