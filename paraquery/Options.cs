using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    /*
        SelfClosingTags
            Use this for XHTML, e.g,  "<br />"

        DebugSourceFormatting
            Use this to see where newlines are being injected

    */

    public class Options
    {
        public bool SelfClosingTags { get; set; } = true;
        public bool DebugSourceFormatting { get; set; } = false;
    }
}
