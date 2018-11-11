using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html
{
    /*

         DebugFlags
            Use these to inject debug information (usually comments) into
            the source. Since comments are dependent upon the kind of content
            being generated, OnDebug() should be overridden for this to work.
            See HtmlRenderer, the base renderer for all HTML renderers, for 
            an example.

    */
    public class HtmlOptions : Options
    {
        public DebugFlags Debug { set; get; } = DebugFlags.None;
        public CssFormats CssFormat { set; get; } = CssFormats.Readable;

        public bool CreateDependencies { set; get; } = false;

    }
}
