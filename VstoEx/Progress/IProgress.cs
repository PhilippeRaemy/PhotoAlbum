namespace VstoEx.Progress
{
    using System;

    public interface IProgress : IDisposable
    {
        IProgress InitProgress(int max, string caption);
        IProgress Progress(string text);
        void CloseProgress();
        event EventHandler<EventArgs> CancelEvent;
    }
}