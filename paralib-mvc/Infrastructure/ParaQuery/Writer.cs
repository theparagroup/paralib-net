using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery;
using System.IO;

namespace com.paralib.Mvc.Infrastructure.ParaQuery
{
    public class Writer : paraquery.Engines.Base.Writer
    {
        protected TextWriter _writer { get; private set; }

        public Writer(IContext context, TextWriter writer) : base(context)
        {
            _writer = writer;
        }

        protected override string _newline
        {
            get
            {
                return _writer.NewLine;
            }
        }

        protected override void _write(string text)
        {
            _writer.Write(text);
        }

        protected override void _writeLine()
        {
            _writer.WriteLine();
        }

        protected override void _writeLine(string text)
        {
            _writer.WriteLine(text);
        }

    }
}
