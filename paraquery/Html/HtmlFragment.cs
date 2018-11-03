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
    public abstract class HtmlFragment : HtmlComponent
    {
        public HtmlFragment(HtmlContext context, HtmlRenderer renderer) : base(context, renderer)
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
