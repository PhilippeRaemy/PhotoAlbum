namespace AlbumWordAddin
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Office.Tools.Ribbon;
    using MoreLinq;


    internal enum RibbonControlEnablereasonEnum { Functional, Selection}

    internal class RibbonControlSet
    {
        protected readonly RibbonControl[] Buttons;
        readonly Dictionary<RibbonControlEnablereasonEnum, bool> _enableReasons = new Dictionary<RibbonControlEnablereasonEnum, bool>();

        public RibbonControlSet(IEnumerable<RibbonControl> buttons)
        {
            Buttons = buttons.ToArray();
        }

        public void SetEnabled(RibbonControlEnablereasonEnum reason, bool enabled)
        {
            _enableReasons[reason] = enabled;
            var allEnabled = _enableReasons.All(e => e.Value);
            foreach(var button in Buttons) button.Enabled = allEnabled;
        }
        public static RibbonControlSet operator +(RibbonControlSet c1, IEnumerable<RibbonControl> buttons)
            => new RibbonControlSet(c1.Buttons.Concat(buttons));

    }

    internal class RibbonToggleButtonSet : RibbonControlSet
    {

        public RibbonToggleButtonSet(IEnumerable<RibbonToggleButton> buttons) : base(buttons)
        {
        }

        public RibbonToggleButton SelectedButton
        {
            get { return (RibbonToggleButton)Buttons.FirstOrDefault(b => ((RibbonToggleButton)b).Checked); }
            set { Buttons.ForEach(b => ((RibbonToggleButton)b).Checked = b == value); }
        }
    }
}