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
    public class Page : Fragment
    {

        public Page(HtmlContext context, bool begin = true) : base(context, new HtmlBlock(context, "page", context.IsDebug(DebugFlags.Page), false), begin)
        {
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
                return HtmlBuilder.DOCTYPE(documentType.Value);
            }
            else
            {
                return null;
            }
        }

        protected virtual DocumentTypes? DocumentType
        {
            get
            {
                //quirks mode is default
                return null;
            }
        }

        protected virtual Tag OnHtml()
        {
            return HtmlBuilder.Html(a => a.Lang = Language);
        }

        protected virtual string Language
        {
            get
            {
                return null;
            }
        }

        protected virtual void Head()
        {
            var head = OnHead();
            Open(head);

            Open(HtmlBuilder.Title());
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
            return HtmlBuilder.Head();
        }

        public virtual string Title
        {
            get
            {
                return GetType().Name;
            }
        }

        protected virtual void OnHeadContent()
        {
        }

        protected virtual Tag OnBody()
        {
            return HtmlBuilder.Body();
        }

        protected virtual void OnContent()
        {
            //you can override this or add content in a using
        }

        protected override void OnPostContent()
        {
        }


    }
}
