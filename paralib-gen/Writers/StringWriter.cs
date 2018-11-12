using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Writers
{
    public class StringWriter : Writer
    {
        protected StringBuilder _sb { get; }= new StringBuilder();

        protected override void _write(string text)
        {
            _sb.Append(text);
        }

        protected override void _writeLine()
        {
            _sb.Append(_newline);
        }

        protected override void _writeLine(string text)
        {
            _sb.Append(text);
            _sb.Append(_newline);
        }

        public override string ToString()
        {
            return _sb.ToString();
        }

    }
}
