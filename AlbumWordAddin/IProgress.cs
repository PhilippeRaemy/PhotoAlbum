namespace AlbumWordAddin
{
    public interface IProgress
    {
        void InitProgress(int max);
        void Progress();
        void CloseProgress();
    }
}