namespace AlbumWordAddin
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Office.Tools.Ribbon;
    using MoreLinq;
    using VstoEx.Extensions;

    internal class RibbonControlSet
    {
        static readonly Dictionary<RibbonControl, List<RibbonControlSet>> ControlsDic 
            = new Dictionary<RibbonControl, List<RibbonControlSet>>();

        protected readonly RibbonControl[] _buttons;
        bool _enabled;

        public RibbonControlSet(IEnumerable<RibbonControl> buttons)
        {
            _buttons = buttons.CheapToArray();
            foreach (var button in _buttons)
            {
                if (ControlsDic.ContainsKey(button))
                {
                    ControlsDic[button].Add(this);
                }
                else
                {
                    ControlsDic[button] = new List<RibbonControlSet> {this};
                }
            }
        }

        public bool Enabled
        {
            set
                {
                _enabled = value;
                foreach (var button in _buttons) button.Enabled = ControlsDic[button].All(r => r.Enabled);
            }
            private get { return _enabled; }
        }
    }

    internal class RibbonToggleButtonSet : RibbonControlSet
    {

        public RibbonToggleButtonSet(IEnumerable<RibbonToggleButton> buttons) : base(buttons)
        {
        }

        public RibbonToggleButton SelectedButton
        {
            get { return (RibbonToggleButton)_buttons.FirstOrDefault(b => ((RibbonToggleButton)b).Checked); }
            set { _buttons.ForEach(b => ((RibbonToggleButton)b).Checked = b == value); }
        }
    }
}