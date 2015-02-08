using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Models
{
    public class DisplayableStringItem : DisplayableItem<string>
    {
        public DisplayableStringItem(string value, string displayValue = null) : base(value, displayValue)
        {
        }
    }
}
