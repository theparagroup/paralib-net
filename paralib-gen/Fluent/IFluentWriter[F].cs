using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Fluent
{
    public interface IFluentWriter<F> where F : class
    {
        F Write(string content);
        F WriteLine(string content);
        F NewLine();
        F Space();
        F Snippet(string text, string newline = null);
    }
}
