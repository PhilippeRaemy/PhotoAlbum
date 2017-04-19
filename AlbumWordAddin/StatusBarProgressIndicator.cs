
namespace AlbumWordAddin
{
    using System;
    using Microsoft.Office.Interop.Word;

    internal class StatusBarProgressIndicator : IProgress
    {
        readonly Application _application;
        int _max;
        int _progress;
        string _caption;

        public StatusBarProgressIndicator(Application application)
        {
            _application = application;
        }

        public void InitProgress(int max, string caption)
        {
            _max = max;
            _progress = 0;
            _caption = caption;
            _application.StatusBar = _caption;
        }

        public void Progress(string text)
        {
            _application.StatusBar = $"{_caption} - ({++_progress}/{_max}) - {text}";
        }

        public void CloseProgress()
        {
            _application.StatusBar = null;
        }

        public event EventHandler<EventArgs> CancelEvent;
    }
}
