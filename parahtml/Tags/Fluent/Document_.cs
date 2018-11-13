using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;

namespace com.parahtml.Tags.Fluent
{
    public partial class Document
    {

        public virtual Document DOCTYPE(DocumentTypes documentType)
        {
            return Push(HtmlBuilder.DOCTYPE(documentType));
        }

        public virtual Document DOCTYPE(string specification)
        {
            return Push(HtmlBuilder.DOCTYPE(specification));
        }

        public virtual Document Html(Action<HtmlAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Html(attributes));
        }

        public virtual Document Head(Action<HeadAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Head(attributes));
        }

        public virtual Document Title(Action<GlobalAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Title(attributes));
        }

        public virtual Document Style(Action<StyleAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Style(attributes));
        }

        public virtual Document Script(Action<ScriptAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Script(attributes));
        }

        public virtual Document Body(Action<BodyAttributes> attributes = null)
        {
            return Push(HtmlBuilder.Body(attributes));
        }

    }
}
