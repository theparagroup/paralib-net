using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Fluent;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.parahtml.Attributes;

namespace com.parahtml.Html
{
    public abstract class FluentHtmlBase<F> : FluentRendererStack<HtmlContext, F> where F : FluentHtmlBase<F>
    {
        public FluentHtmlBase(HtmlContext context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }

        public new HtmlContext Context
        {
            get
            {
                return base.Context;
            }
        }

        protected HtmlOptions Options
        {
            get
            {
                return base.Context.Options;
            }
        }

        public HtmlBuilder HtmlBuilder
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

    }
}
