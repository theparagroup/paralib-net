using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;

namespace com.paraquery.Html.Fluent
{
    public class Html : Renderer
    {
        public Html(IContext context) : base(context, RenderModes.Block, context.Options.DebugSourceFormatting)
        {
        }

        protected override void OnBegin()
        {
            _writer.Write("<!-- html start -->");
        }

        protected override void OnEnd()
        {
            _writer.Write("<!-- html end -->");
        }
    }
}
