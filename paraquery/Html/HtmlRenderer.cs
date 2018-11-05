using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html
{
    /*
        
        Basically defines Comment and OnDebug.

        All HTML-based renderers should derive from this.

    */
    public abstract class HtmlRenderer : Renderer
    {
        public HtmlRenderer(Context context, LineModes lineMode, StackModes stackMode, bool terminal, bool visible, bool indent=true) : base(context, lineMode, stackMode, terminal, visible, indent)
        {
        }

        public static void Comment(Writer writer, string text)
        {
            writer.Write($"<!-- {text} -->");
        }

        public void Comment(string text)
        {
            Comment(Writer, text);
        }

        protected override void OnDebug(string text)
        {
            Comment(text);
        }

    }
}
