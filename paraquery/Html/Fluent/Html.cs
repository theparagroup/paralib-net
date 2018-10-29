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
        public Html(IContext context) : base(context, RenderModes.Block, context.Options.DebugSourceFormatting)
        {
        }

        protected override void OnBegin()
        {
            _writer.Write("<!-- fluent html start -->");
        }

        protected override void OnEnd()
        {
            _writer.Write("<!-- fluent html end -->");
        }
    }
}
