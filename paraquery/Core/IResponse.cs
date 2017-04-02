using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Core
{
    public interface IResponse
    {
        void Write(string text);
        void WriteLine(string text);
        void NewLine();

        void Tab();
        void Indent();
        void Dedent();

        void Snippet(string name, string text);

        void Attribute(string name, string value);

        void Block(string text);
        void StartBlock(string name);
        void EndBlock();
        void CloseBlock(string name);

        void Inline(string text);
        void StartInline(string name);
        void EndInline();
        void CloseInline(string name);

        void Empty(string name);

    }
}
