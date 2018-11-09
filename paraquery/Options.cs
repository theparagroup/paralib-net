using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    /*
        SelfClosingTags
            For empty/void elements. Use this for XHTML, e.g,  "<br />", 
            otherwise, "<br>".

        MinimizeBooleans
            Use short form for booleans, e.g. defer="defer".

        EscapeAttributeValues
            Convert " into &quot;

       

    */

    public class Options
    {
        public bool DebugSourceFormatting { get; set; } = false;

        public bool SelfClosingEmptyTags { get; set; } = true;
        public bool MinimizeBooleans { get; set; } = true;
        public bool EscapeAttributeValues { get; set; } = false;

        
    }

}
