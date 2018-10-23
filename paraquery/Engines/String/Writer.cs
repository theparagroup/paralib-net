using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.StringContext
{
    public class Writer : Engines.Base.Writer
    {
        protected StringBuilder _sb { get; }= new StringBuilder();

        public Writer(IContext context) : base(context)
        {
        }

        protected override void _write(string text)
        {
            _sb.Append(text);
        }

        protected override void _writeLine()
        {
            _sb.Append(Environment.NewLine);
        }

        protected override void _writeLine(string text)
        {
            _sb.Append(text);
            _sb.Append(Environment.NewLine);
        }

        public override string ToString()
        {
            return _sb.ToString();
        }

    }
}
