using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.jQuery.Blocks
{
    /*
        $(document).ready(function() {
        ...
        );
    */
    public class Ready : jQueryBlock
    {
        public Ready(IContext context) : base(context)
        {
            Begin();
        }

        protected override string Description
        {
            get
            {
                return "Ready";
            }
        }

        protected override void OnBegin()
        {
            _writer.Write("$(document).ready(function() {");
        }

        protected override void OnEnd()
        {
            _writer.Write("}); //end ready");
        }

    }
}
