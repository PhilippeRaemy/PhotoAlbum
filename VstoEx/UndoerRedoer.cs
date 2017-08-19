namespace VstoEx
{
    using System;
    using Microsoft.Office.Core;
    using Microsoft.Office.Tools.Word;
    using System.Linq;
;

    public class UndoerRedoer : IDisposable
    {
        readonly Document _document;
        bool _trackChanges;
        const string RevisionPropName = "VstoEx.UndoerRedoer.Revision";

        public UndoerRedoer(Document document)
        {
            if (_document == null) throw new ArgumentNullException(nameof(_document));
            _document = document;
        }

        int IncreaseRevisionNumber()
        {
            dynamic revisionProp = GetRevisionProperty();
            if (revisionProp != null) return revisionProp.Value = (int)revisionProp.Value + 1;
            _document.CustomDocumentProperties.Add(RevisionPropName, false,
                MsoDocProperties.msoPropertyTypeNumber, 1);
            return 1;
        }

        DocumentProperty GetRevisionProperty() 
            => Enumerable.Range(1, (int?)_document.CustomDocumentProperties.Count() ?? 0)
                         .Select(i => (DocumentProperty)_document.CustomDocumentProperties.Item(i))
                         .FirstOrDefault(p => p.Name == RevisionPropName);

        int GetRevisionNumber()
            => (int?) GetRevisionProperty()?.Value ?? 0;

        public UndoerRedoer TrackChanges()
        {
            _trackChanges = true;
            IncreaseRevisionNumber();
            return this;
        }

        public void Dispose()
        {
            if(_trackChanges) IncreaseRevisionNumber();
        }

        public bool Undo() => UndoRedo(() => _document.Undo());
        public bool Redo() => UndoRedo(() => _document.Redo());

        bool UndoRedo(Func<bool> undoredo)
        {
            // if 1st undo changes the revision number then undo until another revision number change occurs
            // otherwise indo only one.
            var revisionNumber = GetRevisionNumber();
            var success = undoredo();
            if (!success) return false;
            var newRrevisionNumber = GetRevisionNumber();
            if (revisionNumber == newRrevisionNumber) return true;
            while(newRrevisionNumber == GetRevisionNumber())
            {
                success = undoredo();
                if (!success) return false;
            }
            return true;
        }
    }
}
