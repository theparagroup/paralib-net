using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Fluent
{
    public interface IFluentWriter
    {
        void Write(string content);
        void WriteLine(string content);
        void NewLine();
        void Space();
        void Snippet(string text, string newline = null);
    }
}
