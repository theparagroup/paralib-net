using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;

namespace com.parahtml.Attributes
{
    public class HrAttributes : GlobalAttributes
    {
        public HrAlignTypes? Align { set; get; }
        public string align { set; get; }

        public int Size { set; get; }
        public string size { set; get; }

        public void Width(int pixels)
        {
            width = $"{pixels}";
        }
        public void Width(float percentage)
        {
            width = $"{percentage}%";
        }
        public string width { set; get; }

        public bool? NoShade { set; get; }
        public string noshade { set; get; }
    }
}
