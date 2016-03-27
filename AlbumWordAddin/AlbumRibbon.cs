using Microsoft.Office.Tools.Ribbon;

namespace AlbumWordAddin
{
    public partial class AlbumRibbon
    {
        private void AlbumRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            PerformLayout();
        }
        private void ButtonRemoveEmptyPages_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.RemoveEmptyPages();
        }

        private void ButtonSelectShapesOnPage_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SelectShapesOnPage();
        }
    }
}
