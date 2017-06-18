namespace AlbumWordAddin
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Office.Tools.Ribbon;
    using MoreLinq;

    internal class RibbonToggleButtonSet
    {
        readonly RibbonToggleButton[] _buttons;

        public RibbonToggleButtonSet(IEnumerable<RibbonToggleButton> buttons)
        {
            _buttons = buttons.ToArray();
        }

        public RibbonToggleButton SelectedButton
        {
            get { return _buttons.FirstOrDefault(b => b.Checked); }
            set { _buttons.ForEach(b => b.Checked = b == value); }
        }

        public bool Enabled
        {
            set { _buttons.ForEach(b => b.Enabled = value); }
        }
    }
}