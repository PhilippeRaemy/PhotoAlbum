
namespace VstoEx
{
    using System;
    using Word = Microsoft.Office.Interop.Word;
    public class StatePreserver : IDisposable
    {
        bool _screenUpdatingSet;
        bool _previousScreenUpdating;
        readonly Word.Application _application;

        public StatePreserver(Word.Application application) {
            _application = application;
        }

        public void Dispose()
        {
            if (_screenUpdatingSet) _application.ScreenUpdating = _previousScreenUpdating;
        }

        public StatePreserver FreezeScreenUpdating() {
            
            _screenUpdatingSet = true;
            _previousScreenUpdating = _application.ScreenUpdating;
            _application.ScreenUpdating = false;
            return this;
        }
    }
}
