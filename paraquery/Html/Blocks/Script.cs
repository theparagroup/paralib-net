using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;

namespace com.paraquery.Html.Blocks
{
    public class Script : HtmlBlock
    {
        public Script(Context context, object attributes = null) : base(context, attributes)
        {
            Begin();
        }

        protected override void OnBegin()
        {
            Context.Response.StartBlock("script");
            Context.Attributes(new { _attributes, defaults = new { type = "text/javascript" } });
            Context.Response.EndBlock();
        }

        protected override void OnEnd()
        {
            Context.Response.CloseBlock("script");
        }


    }
}
