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
        int TabLevel { get; }


        void Snippet(string name, string text);

        void Attribute(string name, string value);
        void Attributes(object attributes = null);

        void StartBlock(string name);
        void EndBlock();
        void CloseBlock(string name);

        void StartInline(string name);
        void EndInline();
        void CloseInline(string name);

        void Empty(string name);

    }
}
