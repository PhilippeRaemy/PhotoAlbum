namespace VstoEx
{
    using System;
    using System.Globalization;
    using Microsoft.Office.Core;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Microsoft.Office.Interop.Word;
    using Document = Microsoft.Office.Tools.Word.Document;

    public class UndoerRedoer : IDisposable
    {
        readonly Document _document;
        bool _trackChanges;
        const string RevisionPropRegex  = @"VstoEx.UndoerRedoer.Revision\((\d+)\).";
        const string RevisionPropFormat = @"VstoEx.UndoerRedoer.Revision({0}).";
        const WdBuiltInProperty UnderlyingProperty = WdBuiltInProperty.wdPropertyKeywords;

        public UndoerRedoer(Document document)
        {
            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        DocumentProperty GetRevisionProperty() 
            => Enumerable.Range(1, (int?)_document.CustomDocumentProperties.Count() ?? 0)
                         .Select(i => (DocumentProperty)_document.CustomDocumentProperties.Item(i))
                         .FirstOrDefault(p => p.Name == RevisionPropRegex);

        int GetRevisionNumber()
        {
            var ma = new Regex(RevisionPropRegex).Match(GetPropertyString() ?? string.Empty);
            return ma.Success
                ? int.Parse(ma.Groups[1].Value, NumberStyles.Integer)
                : 0;
        }

        DocumentProperty GetProperty()
            => (DocumentProperty)_document.BuiltInDocumentProperties[UnderlyingProperty];

        string GetPropertyString()
            => (string)GetProperty().Value;

        void IncreaseRevisionNumber()
        {
            var propertyString = GetPropertyString();
            var revisionNumber = GetRevisionNumber();
            if (revisionNumber > 0)
                GetProperty().Value = propertyString.Replace(
                    string.Format(RevisionPropFormat, revisionNumber),
                    string.Format(RevisionPropFormat, revisionNumber + 1)
                );
            else
            {
                if (string.IsNullOrWhiteSpace(propertyString))
                    propertyString = string.Empty;
                else
                {
                    propertyString += Environment.NewLine;
                }
                GetProperty().Value = 
                    propertyString + string.Format(RevisionPropFormat, 1);
            }
        }

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
            // if 1st undo/redo changes the revision number then undo until another revision number change occurs
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
