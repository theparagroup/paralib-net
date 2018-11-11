using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;
using com.paraquery.Rendering;
using com.paraquery.Html.Fluent;

namespace com.paraquery.Html
{
    /*
        
        A base class for chunks of HTML.

        Page and other HTML components derive from this class.

    */
    public class Fragment : HtmlComponent<ParaHtmlPackage>
    {
        public Fragment(HtmlContext context, bool begin=true) : base(context, new HtmlBlock(context, "fragment", context.IsDebug(DebugFlags.Fragment), false))
        {
            if (begin)
            {
                Begin();
            }
        }

        protected override void OnBegin()
        {
        }

        public FluentHtml FluentHtml()
        {
            var fluentHtml = new FluentHtml(Context, false);
            Push(fluentHtml);
            return fluentHtml;
        }

    }
}
