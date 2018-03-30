using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Js.Blocks
{
    /*
        $(document).ready(function() {
        ...
        );
    */
    public class Function : JsBlock
    {
        protected string _name;
        protected string[] _parameters;

        public Function(IContext context, string name, params string[] parameters) : base(context)
        {
            _name = name;
            _parameters = parameters;
            Begin();
        }

        protected override void OnBegin()
        {
            _response.Write($"function {_name}({Utils.Parameters(_parameters)}) {{");
        }

        protected override void OnEnd()
        {
            _response.Write($"}} //end {_name}");
        }

    }
}
