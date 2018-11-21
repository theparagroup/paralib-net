using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using com.paralib.Gen;

namespace com.parahtml.Mvc
{
    public class MvcWriter : Writer
    {
        protected TextWriter _textWriter { private set; get; }

        public MvcWriter(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        protected override void _write(string text)
        {
            _textWriter.Write(text);
        }

        protected override void _writeLine(string text)
        {
            _textWriter.WriteLine(text);
        }

        protected override void _writeLine()
        {
            _textWriter.WriteLine();
        }

    }
}