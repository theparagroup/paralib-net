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
    public class Fragment : HtmlComponent
    {
        protected Fragment(HtmlContext context, Renderer starter, bool begin) : base(context, starter)
        {
            if (begin)
            {
                Begin();
            }
        }

        public Fragment(HtmlContext context, bool begin = true) : this(context, new HtmlContainer(context, "fragment", context.IsDebug(DebugFlags.Fragment), false), begin)
        {

        }

        protected override void OnBegin()
        {
        }

        public FluentHtml Html()
        {
            var fluentHtml = new FluentHtml(Context, false);
            Push(fluentHtml);
            return fluentHtml;
        }

    }
}
