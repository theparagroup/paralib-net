using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.parahtml.Attributes;
using com.paralib.Gen.Fluent;

namespace com.parahtml.Html
{
    public class FluentDocument : FluentHtmlBase<FluentDocument>
    {
        public FluentDocument(HtmlContext context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }

        public virtual FluentDocument DOCTYPE(DocumentTypes documentType)
        {
            return Open(HtmlBuilder.DOCTYPE(documentType));
        }

        public virtual FluentDocument DOCTYPE(string specification)
        {
            return Open(HtmlBuilder.DOCTYPE(specification));
        }

        public virtual FluentDocument Html(Action<HtmlAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Html(attributes));
        }

        public virtual FluentDocument Head(Action<HeadAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Head(attributes));
        }

        public virtual FluentDocument Title(Action<GlobalAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Title(attributes));
        }

        public virtual FluentDocument Style(Action<StyleAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Style(attributes));
        }

        public virtual FluentDocument Script(Action<ScriptAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Script(attributes));
        }

        public virtual FluentDocument Body(Action<BodyAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Body(attributes));
        }
    }
}
