using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;
using com.paralib.Utils;

namespace com.paraquery.jQuery.Blocks
{
    /*
        $('selector').click(function() {
            ...
        });
    */
    public class Click : JsBlock
    {
        protected string _selector;
        protected object _data;

        public Click(Context context, string selector, object data=null) : base(context)
        {
            _selector = selector;
            _data = data;
            Begin();
        }

        protected override void OnBegin()
        {
            Context.Response.WriteLine($"$('{_selector}').click({Utils.Parameters(_data)}function(event) {{");
        }

        protected override void OnEnd()
        {
            Context.Response.WriteLine($"}}); //end click");
        }

    }
}
