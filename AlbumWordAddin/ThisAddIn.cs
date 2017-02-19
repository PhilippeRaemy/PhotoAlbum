using System;
using System.Linq;
using System.Windows.Forms;
using Mannex;
using Word = Microsoft.Office.Interop.Word;

using Microsoft.Office.Tools.Word;

namespace AlbumWordAddin
{
    using System.IO;
    using Microsoft.Office.Core;
    using UserPreferences;

    public partial class ThisAddIn
    {
        Document       ActiveDocument => Globals.Factory.GetVstoObject(Application.ActiveDocument);
        Word.Selection Selection      => ActiveDocument.Application.Selection;
        public static AlbumRibbon ThisRibbon { get; set; }

        AlbumWordAddinUtils _utilities;

        protected override object RequestComAddInAutomationService()
        {
            return _utilities ?? (_utilities = new AlbumWordAddinUtils());
        }

        void ThisAddIn_Startup(object sender, EventArgs e)
        {
            try
            {
                Application_DocumentOpen(Application.ActiveDocument);
            }
            catch
            {
                // ignored
            }
            Application.DocumentOpen += Application_DocumentOpen;
            ((Word.ApplicationEvents4_Event)Application).NewDocument += Application_DocumentOpen;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(((Exception) e.ExceptionObject).Message, "An error occured in AlbumWordAddin",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void Application_DocumentOpen(Word.Document doc)
        {
            Globals.Factory.GetVstoObject(doc).SelectionChange += ThisAddIn_SelectionChange;
        }

        void ThisAddIn_SelectionChange(object sender, SelectionEventArgs e)
        {
            Func<float, float, float> sameOrDefect = (t, s) => t<=-2 ? t : t<=-1 ? s : Math.Abs(t - s) < .5 ? s : -2f;
            Func<float, string> sizeToText = t => t < 0 ? string.Empty : t.ToInvariantString();

            var sizes= e.Selection.ShapeRange.Cast<Word.Shape>()
                .Aggregate(Tuple.Create(-1f, -1f),
                    (t, s) => Tuple.Create(sameOrDefect(t.Item1, s.Width), sameOrDefect(t.Item2, s.Height)));
            ThisRibbon.editBoxSizeWidth.Text  = sizeToText(sizes.Item1);
            ThisRibbon.editBoxSizeHeight.Text = sizeToText(sizes.Item2);
        }

        internal void AlignSelectedImages(Alignment alignment, float forced = float.NaN)
        {
            _utilities.AlignSelectedImages(alignment, forced);
        }

        void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
        }

        public void RemoveEmptyPages()
        {
            _utilities.RemoveEmptyPages();
        }
        public void SelectShapesOnPage()
        {
            _utilities.SelectShapesOnPage();
        }

        public void FixAnchorOfSelectedImages() {
            _utilities.FixAnchorOfSelectedImages();
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        void InternalStartup()
        {
            Startup += ThisAddIn_Startup;
            Shutdown += ThisAddIn_Shutdown;
        }

        #endregion

        public void DoPositionSelectedImages(string hAlign=null, string vAlign = null)
        {
            _utilities.DoPositionSelectedImages(hAlign, vAlign);
        }

        public void ArrangeSelectedImages(Arrangement arrangement)
        {
            _utilities.DoPositionSelectedImages(arrangement);
        }
        internal void DoPositionSelectedImages(int padding, int margin)
        {
            _utilities.DoPositionSelectedImages(padding, margin);
        }

        public void DoRelativePositionSelectedImages()
        {
            _utilities.DoRelativePositionSelectedImages();
        }

        public void CreateNewAlbumDocument(DirectoryInfo directoryInfo)
        {
            var userPrefs = new PersistedUserPreferences();
            var newDocFile = new FileInfo(Path.Combine(directoryInfo.FullName, $"{directoryInfo.Name}.docx"));
            if(newDocFile.Exists) newDocFile = new FileInfo(Path.Combine(directoryInfo.FullName, $"{directoryInfo.Name}.{DateTime.Now:yyyyMMdd.HHmmss}.docx"));
            Application.Documents.Add(Template: userPrefs.NewDocumentTemplate, NewTemplate: false,
                DocumentType: Word.WdNewDocumentType.wdNewBlankDocument);
        }

        public void CloseCurrentAlbumDocument(DirectoryInfo directoryInfo)
        {
            Application.ActiveDocument.Close(Word.WdSaveOptions.wdSaveChanges);
        }

        public void AddPictureToCurrentDocument(FileInfo fileInfo)
        {
            var pageWidth = Selection.PageSetup.PageWidth;
            var pageHeight = Selection.PageSetup.PageHeight;
            var sel = Application.Selection;
            sel.MoveEnd(Word.WdUnits.wdStory);
            var shp = sel.Document.Shapes.AddPicture(fileInfo.FullName, LinkToFile: true, SaveWithDocument: false,
                Left: pageWidth * .05f, Top: pageHeight * .05f, Anchor: sel);
            shp.RelativeHorizontalPosition = Word.WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
            shp.RelativeVerticalPosition = Word.WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
            shp.LockAspectRatio = MsoTriState.msoCTrue;
            var hRatio = pageWidth / shp.Width;
            var vRatio = pageHeight / shp.Height;
            shp.Width = shp.Width * 0.90f * (vRatio < hRatio ? vRatio : hRatio);
            shp.WrapFormat.Type = Word.WdWrapType.wdWrapTight;
            sel.MoveEnd(Word.WdUnits.wdStory);
            sel.InsertBreak(Type: Word.WdBreakType.wdPageBreak);
        }
    }
}
