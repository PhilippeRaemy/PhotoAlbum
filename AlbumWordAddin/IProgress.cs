namespace AlbumWordAddin
{
    using System;
    public interface IProgress
    {
        void InitProgress(int max, string caption);
        void Progress(string text);
        void CloseProgress();
        event EventHandler<EventArgs> CancelEvent;
    }
}