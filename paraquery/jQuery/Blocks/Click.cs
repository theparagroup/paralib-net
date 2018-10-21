using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Click(IContext context, string selector, object data=null) : base(context)
        {
            _selector = selector;
            _data = data;
            Begin();
        }

        protected override string Description
        {
            get
            {
                return "Click";
            }
        }

        protected override string Id
        {
            get
            {
                return _selector;
            }
        }

        protected override void OnBegin()
        {
            _writer.WriteLine($"$('{_selector}').click({Utils.Parameters(_data)}function(event) {{");
        }

        protected override void OnEnd()
        {
            _writer.WriteLine($"}}); //end click");
        }

    }
}
