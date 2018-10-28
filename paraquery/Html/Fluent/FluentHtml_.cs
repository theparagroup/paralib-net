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

        public virtual FluentHtml Div(Action<GlobalAttributes> attributes = null)
        {
            return Push(_tagBuilder.Div(attributes));
        }

        public virtual FluentHtml Span(Action<GlobalAttributes> attributes = null)
        {
            return Push(_tagBuilder.Span(attributes));
        }

        public virtual FluentHtml Hr(Action<HrAttributes> attributes = null)
        {
            return Push(_tagBuilder.Hr(attributes));
        }

        public virtual FluentHtml Script(Action<ScriptAttributes> attributes = null)
        {
            return Push(_tagBuilder.Script(attributes));
        }

    }
}
