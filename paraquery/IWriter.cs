using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    public interface IWriter
    {
        void Write(string text);
        void WriteLine(string text);
        void NewLine();
        void Space();
        void Snippet(string text, string newline = null);

        void Indent();
        void Dedent();

        int TabLevel { get; }
        bool IsNewLine { get; }

    }
}
