using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;
using com.paraquery.Rendering;

namespace com.paraquery.Html.Fluent
{
    public partial class FluentHtml
    {

        public virtual FluentHtml Block(string name, Action<GlobalAttributes> attributes = null, bool empty=false)
        {
            return Push(TagBuilder.Block(name, attributes, empty));
        }

        public virtual FluentHtml Inline(string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            return Push(TagBuilder.Inline(name, attributes, empty));
        }

        public virtual FluentHtml Div(Action<GlobalAttributes> attributes = null)
        {
            return Push(TagBuilder.Div(attributes));
        }

        public virtual FluentHtml Span(Action<GlobalAttributes> attributes = null)
        {
            return Push(TagBuilder.Span(attributes));
        }

        public virtual FluentHtml Hr(Action<HrAttributes> attributes = null)
        {
            return Push(TagBuilder.Hr(attributes));
        }

        public virtual FluentHtml Script(Action<ScriptAttributes> attributes = null)
        {
            return Push(TagBuilder.Script(attributes));
        }

    }
}
