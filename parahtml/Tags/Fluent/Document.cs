using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.parahtml.Attributes;
using com.paralib.Gen.Fluent;

namespace com.parahtml.Tags.Fluent
{
    public partial class Document : FluentStack<HtmlContext, Document>, IDocument
    {
        public Document(HtmlContext context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }

        protected HtmlBuilder HtmlBuilder
        {
            get
            {
                return Context.HtmlBuilder;
            }
        }

        public virtual IDocument DOCTYPE(DocumentTypes documentType)
        {
            return Open(HtmlBuilder.DOCTYPE(documentType));
        }

        public virtual IDocument DOCTYPE(string specification)
        {
            return Open(HtmlBuilder.DOCTYPE(specification));
        }

        public virtual IDocument Html(Action<HtmlAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Html(attributes));
        }

        public virtual IDocument Head(Action<HeadAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Head(attributes));
        }

        public virtual IDocument Title(Action<GlobalAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Title(attributes));
        }

        public virtual IDocument Style(Action<StyleAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Style(attributes));
        }

        public virtual IDocument Script(Action<ScriptAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Script(attributes));
        }

        public virtual IDocument Body(Action<BodyAttributes> attributes = null)
        {
            return Open(HtmlBuilder.Body(attributes));
        }
    }
}
