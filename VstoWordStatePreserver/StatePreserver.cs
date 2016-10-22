
namespace VstoWordStatePreserver
{
    using Word = Microsoft.Office.Interop.Word;
    public class StatePreserver : System.IDisposable
    {
        private bool _screenUpdatingSet;
        private bool _previousScreenUpdating;
        private Word.Application _application;

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
            return this;
        }
    }
}
