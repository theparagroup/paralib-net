using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html.Fluent
{
    public class FluentHtmlMarker : HtmlRenderer
    {
        public FluentHtmlMarker(Context context) : base(context, context.Options.CommentFluentHtml ? FormatModes.Block : FormatModes.None, StackModes.Block)
        {
        }

        protected override void OnBegin()
        {
            if (Context.Options.CommentFluentHtml)
            {
                Comment("fluent html start");
            }
        }

        protected override void OnEnd()
        {
            if (Context.Options.CommentFluentHtml)
            {
                Comment("fluent html end");
            }
        }
    }
}
