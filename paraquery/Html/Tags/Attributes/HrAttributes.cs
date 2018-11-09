using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags.Values;

namespace com.paraquery.Html.Tags.Attributes
{
    public class HrAttributes : GlobalAttributes
    {
        public HrAlignTypes? Align { get; set; }
        public string align { get; set; }
        public string Size { get; set; }
        public string Width { get; set; }
        public bool? NoShade { get; set; }
    }
}
