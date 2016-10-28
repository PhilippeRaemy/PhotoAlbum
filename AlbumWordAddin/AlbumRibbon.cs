using System;
using System.Globalization;
using Microsoft.Office.Tools.Ribbon;

namespace AlbumWordAddin
{
    public partial class AlbumRibbon
    {
        void AlbumRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            PerformLayout();
        }

        void ButtonRemoveEmptyPages_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.RemoveEmptyPages();
        }

        void ButtonSelectShapesOnPage_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SelectShapesOnPage();
        }

        void ButtonFixAnchors_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.FixAnchorOfSelectedImages();
        }

        void buttonSizeToWidest_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Widest);
        }

        void buttonSizeToTallest_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Tallest);
        }

        void editBoxSizeWidth_TextChanged(object sender, RibbonControlEventArgs e)
        {
            float f;
            if (float.TryParse(editBoxSizeWidth.Text, NumberStyles.Float, CultureInfo.CurrentUICulture, out f))
            {
                Globals.ThisAddIn.AlignSelectedImages(Alignment.ForceWidth, f);
            }
        }

        void editBoxSizedHeight_TextChanged(object sender, RibbonControlEventArgs e)
        {
            float f;
            if (float.TryParse(editBoxSizeWidth.Text, NumberStyles.Float, CultureInfo.CurrentUICulture, out f))
            {
                Globals.ThisAddIn.AlignSelectedImages(Alignment.ForceHeight, f);
            }
        }

        void buttonBestFit_Click(object sender, RibbonControlEventArgs e)
        {

        }

        void buttonAlignTop_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Top);
        }

        void buttonAlignMiddle_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Middle);
        }

        void buttonAlignBottom_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Bottom);
        }

        void buttonAlignLeft_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Left);
        }

        void buttonAlignCenter_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Center);
        }

        void buttonAlignRight_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Right);
        }

        void buttonSizeToNarrowest_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Narrowest);
        }

        void buttonSizeToShortest_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.AlignSelectedImages(Alignment.Shortest);
        }

        void buttonArrangeLV_Click(object sender, RibbonControlEventArgs e)
        {

        }

        void buttonArrangeRV_Click(object sender, RibbonControlEventArgs e)
        {

        }

        void buttonArrangeSq_Click(object sender, RibbonControlEventArgs e)
        {

        }

        void buttonArrangeRH_Click(object sender, RibbonControlEventArgs e)
        {

        }

        void buttonArrangeH_Click(object sender, RibbonControlEventArgs e)
        {

        }

        void MenuItemHAlign_Click(object sender, RibbonControlEventArgs e)
        {
            var ribbonButton = sender as RibbonButton;
            if (ribbonButton == null) throw new InvalidOperationException();
            mnuHAlign.Image = ribbonButton.Image;
            mnuHAlign.Tag = ribbonButton.Id;
            mnuHAlign_Click(sender, e);
        }

        void MenuItemVAlign_Click(object sender, RibbonControlEventArgs e)
        {
            var ribbonButton = sender as RibbonButton;
            if (ribbonButton == null) throw new InvalidOperationException();
            mnuVAlign.Image = ribbonButton.Image;
            mnuVAlign.Tag = ribbonButton.Id;
            mnuVAlign_Click(sender, e);
        }

        void mnuHAlign_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.DoPositionSelectedImages(hAlign: (string)mnuHAlign.Tag);
        }

        void mnuVAlign_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.DoPositionSelectedImages(vAlign: (string)mnuVAlign.Tag);
        }
    }
}
