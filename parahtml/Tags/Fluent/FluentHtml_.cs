using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.parahtml.Css.Fluent;

namespace com.parahtml.Tags.Fluent
{
    public partial class FluentHtml
    {

        public virtual FluentHtml Block(string name, Action<GlobalAttributes> attributes = null, bool empty=false)
        {
            return Push(HtmlBuilder.Block(name, attributes, empty));
        }

        public virtual FluentHtml Inline(string name, Action<GlobalAttributes> attributes = null, bool empty = false)
        {
            return Push(HtmlBuilder.Inline(name, attributes, empty));
        }

        public virtual FluentHtml Html(Action<HtmlAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Html(attributes));
        }

        public virtual FluentHtml Head(Action<HeadAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Head(attributes));
        }

        public virtual FluentHtml Title(Action<GlobalAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Title(attributes));
        }

        public virtual FluentHtml Style(Action<StyleAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Style(attributes));
        }

        public FluentCss FluentCss()
        {
            var fluentCss = new FluentCss(Context, this);
            Push(fluentCss);
            return fluentCss;
        }

        public virtual FluentHtml Body(Action<BodyAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Body(attributes));
        }

        public virtual FluentHtml Div(Action<GlobalAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Div(attributes));
        }

        public virtual FluentHtml Span(Action<GlobalAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Span(attributes));
        }

        public virtual FluentHtml Br(Action<GlobalAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Br(attributes));
        }

        public virtual FluentHtml Hr(Action<HrAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Hr(attributes));
        }

        public virtual FluentHtml Script(Action<ScriptAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Script(attributes));
        }

        public virtual FluentHtml NoScript(Action<GlobalAttributes> attributes = null)
        {
            return Push(HtmlBuilder.NoScript(attributes));
        }

    }
}
