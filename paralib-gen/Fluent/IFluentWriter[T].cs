using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Fluent
{
    public interface IFluentWriter<T>
    {
        T Write(string content);
        T WriteLine(string content);
        T NewLine();
        T Space();
        T Snippet(string text, string newline = null);
    }
}
