using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;
using com.paraquery.Html;

namespace com.paraquery.Bootstrap.Grids
{
    public class GridMarker : HtmlRenderer
    {
        public GridMarker(Context context) : base(context, RenderModes.Block, context.Options.DebugSourceFormatting)
        {
        }

        protected override void OnBegin()
        {
            Comment("fluent bootstrap grid start");
        }

        protected override void OnEnd()
        {
            Comment("fluent bootstrap grid start");
        }
    }
}
