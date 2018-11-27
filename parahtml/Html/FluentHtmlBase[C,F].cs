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
    public abstract class FluentHtmlBase<C, F> : FluentRendererStack<C, F> where F : FluentHtmlBase<C, F> where C : HtmlContext
    {
        public FluentHtmlBase(C context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }

        public new C Context
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

        public virtual F Comment(string text)
        {
            HtmlRenderer.Comment(Context.Writer, text);
            return (F)this;
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

        public virtual F Document(DocumentTypes documentType)
        {
            return Open(HtmlBuilder.DOCTYPE(documentType));
        }

        public virtual F Document(string specification)
        {
            return Open(HtmlBuilder.DOCTYPE(specification));
        }

        public virtual F Html(Action<HtmlAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Html(attributes));
        }

        public virtual F Head(Action<HeadAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Head(attributes));
        }

        public virtual F Title(Action<GlobalAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Title(attributes));
        }

        public virtual F Link(Action<LinkAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Link(attributes));
        }

        public virtual F Meta(Action<MetaAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Meta(attributes));
        }

        public virtual F Style(Action<StyleAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Style(attributes));
        }

        public virtual F Script(Action<ScriptAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Script(attributes));
        }

        public virtual F ExternalScript(Action<ExternalScriptAttributes> attributes = null)
        {
            Open(HtmlBuilder.ExternalScript(attributes));
            Close();
            return (F)this;
        }

        public virtual F NoScript(Action<GlobalAttributes> attributes = null)
        {
            return Open(HtmlBuilder.NoScript(attributes));
        }

        public virtual F Body(Action<BodyAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Body(attributes));
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

        public virtual F Img(Action<ImgAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Img(attributes));
        }

        public virtual F A(Action<AAttributes> attributes = null)
        {
            return Open(HtmlBuilder.A(attributes));
        }

        public virtual F Form(Action<FormAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Form(attributes));
        }

    }
}
