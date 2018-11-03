using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;
using com.paraquery.Rendering;
using com.paraquery.Html.Fluent;

namespace com.paraquery.Html
{
    public abstract class HtmlPage : HtmlFragment
    {

        public HtmlPage(HtmlContext context) : base(context, new Marker(context))
        {
        }

        public class Marker : HtmlRenderer
        {
            public Marker(HtmlContext context) : base(context, FormatModes.None, StructureModes.Block)
            {
            }

            protected override void OnBegin()
            {
            }

            protected override void OnEnd()
            {
            }
        }

        protected override void OnBegin()
        {
            //derived classes should use the new OnXXX() methods below
        }

        protected override void OnPreContent()
        {
            OnDocument();
        }

        protected virtual void OnDocument()
        {
            //doctype
            var docType = OnDOCTYPE();
            if (docType != null)
            {
                Open(docType);
                Close(docType);

                //space between doctype and html
                Writer.Space();
            }

            //html
            var html = OnHtml();
            Open(html);

            //head
            Head();

            //space between head and body
            Writer.Space();

            //body
            var body = OnBody();
            Open(body);

            //content
            OnContent();
        }

        protected virtual DOCTYPE OnDOCTYPE()
        {
            var documentType = DocumentType;

            if (documentType != null)
            {
                return new DOCTYPE(Context, documentType.Value);
            }
            else
            {
                return null;
            }
        }

        protected abstract DocumentTypes? DocumentType { get; }

        protected virtual Tag OnHtml()
        {
            return TagBuilder.Html(a=> a.Lang = Language);
        }

        protected abstract string Language {get;}

        protected virtual void Head()
        {
            var head = OnHead();
            Open(head);

            Open(TagBuilder.Title());
            Writer.Write(Title);
            Close();

            //script (inline or external src)
            //style (inline style)
            //link (external style)

            //base
            //meta
            //noscript
            Close(head);

        }

        protected virtual Tag OnHead()
        {
            return TagBuilder.Head();
        }

        public abstract string Title { get; }

        protected virtual void OnHeadContent()
        {
        }

        protected virtual Tag OnBody()
        {
            return TagBuilder.Body();
        }

        protected abstract void OnContent();

        protected override void OnPostContent()
        {
        }


    }
}
