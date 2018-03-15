using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;

namespace com.paraquery.jQuery.Blocks
{
    /*
        $(document).ready(function() {
        ...
        );
    */
    public class Ready : JsBlock
    {
        public Ready(IContext context) : base(context)
        {
            Begin();
        }

        protected override void OnBegin()
        {
            Context.Response.WriteLine("$(document).ready(function() {");
        }

        protected override void OnEnd()
        {
            Context.Response.WriteLine("}); //end ready");
        }

    }
}
