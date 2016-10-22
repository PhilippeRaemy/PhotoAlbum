﻿using Microsoft.Office.Tools.Ribbon;

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

        private void ButtonFixAnchors_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.FixAnchorOfSelectedImages();
        }

        private void buttonAlignLeft_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void buttonAlignCenter_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void buttonAlignRight_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void buttonSizeToWidest_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void buttonSizeToNarrowset_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void buttonSizeToTallest_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void buttonSizeToSmallest_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void editBoxSizeWidth_TextChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void editBoxSizedHeight_TextChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void buttonBestFit_Click(object sender, RibbonControlEventArgs e)
        {

        }
    }
}