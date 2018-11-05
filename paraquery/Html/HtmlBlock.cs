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
    public class HtmlBlock : DebugRenderer
    {
        public HtmlBlock(Context context, string name, bool debug, bool indent) : base(context, name, debug, indent)
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
