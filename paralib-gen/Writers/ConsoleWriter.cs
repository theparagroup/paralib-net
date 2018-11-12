using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Writers
{
    public class ConsoleWriter : Writer
    {

        protected override void _write(string text)
        {
            Console.Write(text);
        }

        protected override void _writeLine()
        {
            Console.Write(_newline);
        }

        protected override void _writeLine(string text)
        {
            Console.Write(text);
            Console.Write(_newline);
        }

    }
}
