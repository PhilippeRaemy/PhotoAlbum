namespace PicturesSorter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    internal class StateKeeper:IDisposable
    {
        readonly Stack<Action> _unwindActions = new Stack<Action>();

        public StateKeeper Hourglass(Form form)
        {
            var previousCursor = form.Cursor;
            _unwindActions.Push(() => form.Cursor = previousCursor);
            form.Cursor = Cursors.WaitCursor;
            return this;
        }

        public StateKeeper Disable(Control control)
        {
            _unwindActions.Push(() => control.Enabled=true);
            control.Enabled = false;
            return this;
        }

        public void Dispose()
        {
            while(_unwindActions.Any()) _unwindActions.Pop().Invoke();
        }
    }
}
