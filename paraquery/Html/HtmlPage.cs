using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;
using com.paraquery.Rendering;

namespace com.paraquery.Html
{
    public abstract class HtmlPage : HtmlComponent
    {
        protected DocTypes? _docType;

        public HtmlPage(Context context, DocTypes? docType = null) : base(context, FormatModes.None, StackModes.Block)
        {
            _docType = docType;

            Begin();
        }

        protected virtual string Title
        {
            get
            {
                return GetType().Name;
            }
        }

        protected override void OnBegin()
        {
        }

        protected override void OnPreContent()
        {
            OnDocType();

            Html();

            Writer.Space();

            var head = Head();
            //head content
            Close(head);

            Writer.Space();

            var body = Body();
            //content
            //scripts
            Close(body);

            Writer.Space();

        }

        protected override void OnPostContent()
        {
        }

        protected virtual void OnDocType()
        {
            //default is "quirks mode" - no doctype

            if (_docType!=null)
            {
                Push(new DocType(Context, _docType.Value));
                Pop();
                Writer.Space();
            }
        }



        protected virtual void Html()
        {
            Push(TagBuilder.Html());
        }

        protected virtual Tag Head()
        {
            var head = TagBuilder.Head();
            Push(head);

            Push(TagBuilder.Title());
            Writer.Write(Title);
            Pop();

            //script (inline or external src)
            //style (inline style)
            //link (external style)

            //base
            //meta
            //noscript

            OnHead();

            return head;
        }

        protected virtual void OnHead()
        {
        }

        protected virtual Tag Body()
        {
            var body = TagBuilder.Body();
            Push(body);

            OnBody();

            return body;
        }

        protected virtual void OnBody()
        {
        }
    }
}
