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

        DebugFlags
            Use these to inject debug information (usually comments) into
            the source. Since comments are dependent upon the kind of content
            being generated, OnDebug() should be overridden for this to work.
            See HtmlRenderer, the base renderer for all HTML renderers, for 
            an example.

    */

    public class Options
    {
        public bool DebugSourceFormatting { get; set; } = false;

        public bool SelfClosingEmptyTags { get; set; } = true;
        public bool MinimizeBooleans { get; set; } = true;
        public bool EscapeAttributeValues { get; set; } = false;

        public DebugFlags Debug { get; set; } = DebugFlags.None;
    }

}
