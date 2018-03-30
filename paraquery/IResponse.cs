using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    public interface IResponse
    {
        void Write(string text);
        void WriteLine(string text);

        void NewLine();

        void Tab();
        void Tabs();
        void Indent();
        void Dedent();
        int TabLevel { get; }

        void Snippet(string name, string text);

    }
}
