using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Mannex;
using Word = Microsoft.Office.Interop.Word;

using Microsoft.Office.Tools.Word;
using MoreLinq;
using VstoEx;
using Mannex.Collections.Generic;

namespace AlbumWordAddin
{
    public partial class ThisAddIn
    {
        Document       ActiveDocument => Globals.Factory.GetVstoObject(Application.ActiveDocument);
        Word.Selection Selection      => ActiveDocument.Application.Selection;
        public static AlbumRibbon ThisRibbon { get; set; }

        private AlbumWordAddinUtils _utilities;

        protected override object RequestComAddInAutomationService()
        {
            if (_utilities == null)
                _utilities = new AlbumWordAddinUtils();

            return _utilities;
        }
        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            try
            {
                Application_DocumentOpen(Application.ActiveDocument);
            }
            catch { }
            Application.DocumentOpen += Application_DocumentOpen;
            ((Word.ApplicationEvents4_Event)Application).NewDocument += Application_DocumentOpen;
        }

        private void Application_DocumentOpen(Word.Document doc)
        {
            Globals.Factory.GetVstoObject(doc).SelectionChange += ThisAddIn_SelectionChange;
        }

        private void ThisAddIn_SelectionChange(object sender, SelectionEventArgs e)
        {
            Func<float, float, float> sameOrDefect = (t, s) => t<=-2 ? t : t<=-1 ? s : Math.Abs(t - s) < .5 ? s : -2f;
            Func<float, string> sizeToText = t => t < 0 ? string.Empty : t.ToInvariantString();

            var sizes= e.Selection.ShapeRange.Cast<Word.Shape>()
                .Aggregate(Tuple.Create(-1f, -1f),
                    (t, s) => Tuple.Create(sameOrDefect(t.Item1, s.Width), sameOrDefect(t.Item2, s.Height)));
            ThisRibbon.editBoxSizeWidth.Text  = sizeToText(sizes.Item1);
            ThisRibbon.editBoxSizeHeight.Text = sizeToText(sizes.Item2);
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
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
        private void InternalStartup()
        {
            Startup += ThisAddIn_Startup;
            Shutdown += ThisAddIn_Shutdown;
        }

        #endregion


    }
}
