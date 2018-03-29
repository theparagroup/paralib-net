using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;


namespace com.paraquery.StringContext
{
    public class Response : ResponseBase
    {
        protected StringBuilder _sb { get; }= new StringBuilder();

        public Response(IContext context) : base(context)
        {
        }

        protected override void _write(string text)
        {
            _sb.Append(text);
        }

        public override string ToString()
        {
            return _sb.ToString();
        }

    }
}
