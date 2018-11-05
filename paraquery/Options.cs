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

        DebugFlags
            Use these to inject debug information (usually comments) into
            the source. Since comments are dependent upon the kind of content
            being generated, OnDebug() should be overridden for this to work.
            See HtmlRenderer, the base renderer for all HTML renderers, for 
            an example.

    */

    public class Options
    {
        public bool SelfClosingTags { get; set; } = true;
        public DebugFlags Debug { get; set; } = DebugFlags.None;
    }

}
