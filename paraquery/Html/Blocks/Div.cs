using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;

namespace com.paraquery.Html.Blocks
{
    public class Div : HtmlBlock
    {

        public Div(Context context, object attributes = null) : base(context, attributes)
        {
            Begin();
        }

        protected override void OnBegin()
        {
            Context.Response.StartBlock("div");
            Context.Attributes(_attributes);
            Context.Response.EndBlock();
        }

        protected override void OnEnd()
        {
            Context.Response.CloseBlock("div");
        }


    }
}
