using System;
using System.Linq;
using System.Text.RegularExpressions;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using MoreLinq;
using VstoEx;

namespace AlbumWordAddin
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
        }

        internal void RemoveEmptyPages()
        {
            var doc = Globals.Factory.GetVstoObject(Application.ActiveDocument);
            // get paragraph information by page

            var paragraphsToBeDeleted = doc.Paragraphs.Cast<Word.Paragraph>()
                .Select(p => new
                    {
                        Paragraph  = p,
                        PageNumber = p.Range.GetPageNumber(),
                        IsEmpty    = p.Range.ShapeRange.Count == 0 
                            && string.IsNullOrWhiteSpace(Regex.Replace(p.Range.Text, @"\x12\x09", string.Empty))
                    }
                )
                .GroupBy          (p=>p.PageNumber              )
                .OrderByDescending(g=>g.Key                     )
                .Where            (g=>g.All(p=>p.IsEmpty)       )
                .SelectMany       (g=>g.Select(gg=>gg.Paragraph))
                .ToList           (                             )
                ;
            foreach (var paragraph in paragraphsToBeDeleted)
            {
                paragraph.Range.Delete();
            }
        }

        public void SelectShapesOnPage()
        {
            var doc = Globals.Factory.GetVstoObject(Application.ActiveDocument);
            var pageNumber = doc.Application.Selection.GetPageNumber();
            var shapesOnPage = doc.Shapes.Cast<Word.Shape>()
                .Where(s => ( s.Type == Office.MsoShapeType.msoLinkedPicture 
                           || s.Type == Office.MsoShapeType.msoPicture 
                           ) && s.Anchor.GetPageNumber() == pageNumber
                           );
            shapesOnPage.ForEach(s => s.Select(Replace:false));
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
