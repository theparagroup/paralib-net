using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml.Attributes
{
    public class HrAttributes : GlobalAttributes
    {
        public HrAlignTypes? Align { set; get; }
        public string align { set; get; }

        public string Size { set; get; }
        public string Width { set; get; }

        public bool? NoShade { set; get; }
        public string noshade { set; get; }
    }
}
