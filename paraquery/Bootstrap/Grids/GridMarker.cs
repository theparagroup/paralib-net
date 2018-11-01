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
        public GridMarker(Context context) : base(context, context.Options.DebugSourceFormatting?FormatModes.Block:FormatModes.None, StackModes.Block)
        {
        }

        protected override void OnBegin()
        {
            if (Context.Options.DebugSourceFormatting)
            {
                Comment("fluent bootstrap grid start");
            }
        }

        protected override void OnEnd()
        {
            if (Context.Options.DebugSourceFormatting)
            {
                Comment("fluent bootstrap grid end");
            }
        }
    }
}
