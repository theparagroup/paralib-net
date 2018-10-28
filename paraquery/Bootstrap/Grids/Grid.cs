using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;

namespace com.paraquery.Bootstrap.Grids
{
    public class Grid : Renderer
    {
        public Grid(IContext context) : base(context, RenderModes.Block, context.Options.DebugSourceFormatting)
        {
        }

        protected override void OnBegin()
        {
            _writer.Write("<!-- fluent bootstrap grid start -->");
        }

        protected override void OnEnd()
        {
            _writer.Write("<!-- fluent bootstrap grid end -->");
        }
    }
}
