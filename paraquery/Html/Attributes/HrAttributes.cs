using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Attributes
{
    public class HrAttributes : GlobalAttributes
    {
        public string align { get; set; }
        public HrAlignTypes? Align { get; set; }
        public string Size { get; set; }
        public string Width { get; set; }
        public bool? NoShade { get; set; }
    }
}
