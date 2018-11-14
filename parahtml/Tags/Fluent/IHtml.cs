using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.paralib.Gen.Fluent;

namespace com.parahtml.Tags.Fluent
{
    public interface IHtml<F>: IFluentRendererStack<F> where F:class
    {
        F Tag(TagTypes tagType, string name, Action<GlobalAttributes> attributes = null, bool empty = false);
        F Div(Action<GlobalAttributes> attributes = null);
        F Span(Action<GlobalAttributes> attributes = null);
        F Br(Action<GlobalAttributes> attributes = null);
        F Hr(Action<HrAttributes> attributes = null);
        F Script(Action<ScriptAttributes> attributes = null);
        F NoScript(Action<GlobalAttributes> attributes = null);

        F Ol(Action<GlobalAttributes> attributes = null);
        F Ul(Action<GlobalAttributes> attributes = null);
        F Li(Action<GlobalAttributes> attributes = null);
    }
}
