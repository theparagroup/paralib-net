using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html.Fluent
{
    public class Html : HtmlRenderer
    {
        public Html(Context context) : base(context, RenderModes.Block, context.Options.DebugSourceFormatting)
        {
        }

        protected override void OnBegin()
        {
            Comment("fluent html start");
        }

        protected override void OnEnd()
        {
            Comment("fluent html end");
        }
    }
}
