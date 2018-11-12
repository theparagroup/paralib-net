using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;
using com.paraquery.Rendering;

namespace com.paraquery.Html
{
    /*

        An HTML-centric version of the DebugRenderer that defines OnDebug.

    */
    public class HtmlDebugInline : DebugRenderer
    {
        public HtmlDebugInline(Context context, string name, bool visible, bool indentContent) : base(context, name, LineModes.None, ContainerModes.Inline, visible, indentContent)
        {
        }

        protected override bool CanDebug
        {
            get
            {
                return true;
            }
        }

        protected override void OnDebug(string text)
        {
            HtmlRenderer.Comment(Writer, text);
        }

    }
}
