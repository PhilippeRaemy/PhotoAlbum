
namespace VstoEx.Progress
{
    using System;
    using Microsoft.Office.Interop.Word;

    public class StatusBarProgressIndicator : IProgress
    {
        readonly Application _application;
        int _max;
        int _progress;
        string _caption;

        public StatusBarProgressIndicator(Application application)
        {
            _application = application;
        }

        public IProgress InitProgress(int max, string caption)
        {
            _max = max;
            _progress = 0;
            _caption = caption;
            _application.StatusBar = _caption;
            return this;
        }

        public IProgress Progress(string text)
            => SetCaption(text, ++_progress);

        public IProgress SetCaption(string text)
            => SetCaption(text, _progress);

        StatusBarProgressIndicator SetCaption(string text, int progress)
        {
            _application.StatusBar = $"{_caption} - ({progress}/{_max}) - {text}";
            return this;
        }

        public void CloseProgress()
        {
            Dispose();
        }

        public event EventHandler<EventArgs> CancelEvent;

        public void Dispose()
        {
            _application.StatusBar = null;
        }
    }
}
