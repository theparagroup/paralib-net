using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.paralib.Gen.Fluent;

namespace com.parahtml.Tags.Fluent
{
    public interface IHtml:IFluentStack<Html>
    {
        IHtml Tag(TagTypes tagType, string name, Action<GlobalAttributes> attributes = null, bool empty = false);
        IHtml Div(Action<GlobalAttributes> attributes = null);
        IHtml Span(Action<GlobalAttributes> attributes = null);
        IHtml Br(Action<GlobalAttributes> attributes = null);
        IHtml Hr(Action<HrAttributes> attributes = null);
        IHtml Script(Action<ScriptAttributes> attributes = null);
        IHtml NoScript(Action<GlobalAttributes> attributes = null);

        IHtml Ol(Action<GlobalAttributes> attributes = null);
        IHtml Ul(Action<GlobalAttributes> attributes = null);
        IHtml Li(Action<GlobalAttributes> attributes = null);
    }
}
