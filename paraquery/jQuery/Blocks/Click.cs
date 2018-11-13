using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen;

namespace com.paraquery.jQuery.Blocks
{
    /*
        $('selector').click(function() {
            ...
        });
    */
    public class Click : jQueryBlock
    {
        protected string _selector;
        protected object _data;

        public Click(Context context, string selector, object data=null) : base(context)
        {
            _selector = selector;
            _data = data;
            Begin();
        }

        public override string Name
        {
            get
            {
                return "click";
            }
        }

        protected override void OnBegin()
        {
            Writer.WriteLine($"$('{_selector}').click({Utils.Parameters(_data)}function(event) {{");
        }

        protected override void OnEnd()
        {
            Writer.WriteLine($"}}); //end click");
        }

    }
}
