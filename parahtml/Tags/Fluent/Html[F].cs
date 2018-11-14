using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.parahtml.Attributes;
using com.paralib.Gen.Fluent;
using com.paralib.Gen;
using com.parahtml.Tags.Fluent.Grids;

namespace com.parahtml.Tags.Fluent
{
    /*

        FluentHtml must either be in Block or Inline mode because....

        Since adding HTML elements under an empty elment doesn't make any sense, we don't have
        that concept (like Tag does) and never go into the "Single" LineMode.

    */

    public partial class Html<F> : FluentRendererStack<HtmlContext, F>, IHtml<F> where F : Html<F>
    {

        public Html(HtmlContext context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }

        protected HtmlBuilder HtmlBuilder
        {
            get
            {
                return Context.HtmlBuilder;
            }
        }

        public virtual F Tag(TagTypes tagType, string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            if (tagType == TagTypes.Block)
            {
                return Open(HtmlBuilder.Block(name, attributes, empty));
            }
            else
            {
                return Open(HtmlBuilder.Inline(name, attributes, empty));
            }
        }

        public virtual F Div(Action<GlobalAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Div(attributes));
        }

        public virtual F Span(Action<GlobalAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Span(attributes));
        }

        public virtual F Br(Action<GlobalAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Br(attributes));
        }

        public virtual F Hr(Action<HrAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Hr(attributes));
        }

        public virtual F Script(Action<ScriptAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Script(attributes));
        }

        public virtual F NoScript(Action<GlobalAttributes> attributes = null)
        {
            return Open(HtmlBuilder.NoScript(attributes));
        }

        public virtual F Ol(Action<GlobalAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Ol(attributes));
        }

        public virtual F Ul(Action<GlobalAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Ul(attributes));
        }

        public virtual F Li(Action<GlobalAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Li(attributes));
        }

        public IGrid Grid(Action<GridOptions> options = null)
        {
            return new FluentGrid(Context,_rendererStack, options);
        }
    }
}