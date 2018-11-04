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
        protected Fragment(HtmlContext context, Renderer starter) : base(context, starter)
        {
        }

        public Fragment(HtmlContext context) : this(context, new HtmlContainer(context, "fragment", context.IsDebug(DebugFlags.Fragment), false))
        {

        }

        protected override void OnBegin()
        {
        }

        public FluentHtml Html()
        {
            var fluentHtml = new FluentHtml(Context);
            Push(fluentHtml);
            return fluentHtml;
        }

    }
}
