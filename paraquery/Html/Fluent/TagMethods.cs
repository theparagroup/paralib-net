using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;

namespace com.paraquery.Html.Fluent
{
    public partial class FluentHtml 
    {
        public virtual FluentHtml Div(object additional = null)
        {
            return Div(null, additional);
        }

        public virtual FluentHtml Div(Action<GlobalAttributes> attributes, object additional = null)
        {
            NewBlock();
            return Push(_tagBuilder.Div(attributes, additional));
        }

        public virtual FluentHtml Span(object additional = null)
        {
            return Span(null, additional);
        }

        public virtual FluentHtml Span(Action<GlobalAttributes> attributes, object additional = null)
        {
            return Push(_tagBuilder.Span(attributes, additional));
        }

        public virtual FluentHtml Hr(object additional = null)
        {
            return Hr(null, additional);
        }

        public virtual FluentHtml Hr(Action<HrAttributes> attributes, object additional = null)
        {
            return Push(_tagBuilder.Hr(attributes, additional));
        }

        public virtual FluentHtml Script(object additional = null)
        {
            return Script(null, additional);
        }

        public virtual FluentHtml Script(Action<ScriptAttributes> attributes, object additional = null)
        {
            return Push(_tagBuilder.Script(attributes, additional));
        }


    }
}
