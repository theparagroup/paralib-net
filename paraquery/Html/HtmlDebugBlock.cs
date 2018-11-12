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

        Used in Page, FluentHtml, etc.

    */
    public class HtmlDebugBlock : DebugRenderer
    {
        public HtmlDebugBlock(Context context, string name, bool visible, bool indentContent) : base(context, name, LineModes.Multiple, ContainerModes.Block, visible, indentContent)
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
