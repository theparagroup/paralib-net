using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;
using com.paraquery.Rendering;

namespace com.paraquery.Html.Fluent
{
    public partial class FluentHtml : HtmlComponent
    {

        public FluentHtml(Context context) : base(context, FormatModes.None, StackModes.Block)
        {
            //let's start with a fluent html marker
            Push(new FluentHtmlMarker(Context));
        }

        protected override void OnDebug(string text)
        {
            HtmlRenderer.Comment(Writer, text);
        }

        protected new FluentHtml Push(Renderer renderer)
        {
            //this method is just to simplify fluent methods...
            base.Push(renderer);
            return this;
        }

        protected override void OnBegin()
        {
            //do nothing here
        }

        protected override void OnEnd()
        {
            CloseAll();
        }

        public new FluentHtml Open(Renderer renderer)
        {
            return Push(renderer);
        }

        public new FluentHtml CloseBlock()
        {
            base.CloseBlock();
            return this;
        }

        public new FluentHtml CloseAll()
        {
            base.CloseAll();
            return this;
        }

        public new FluentHtml Close()
        {
            base.Close();
            return this;
        }

        public FluentHtml Write(string content)
        {
            Context.Writer.Write(content);
            return this;
        }

        public FluentHtml WriteLine(string content)
        {
            Context.Writer.WriteLine(content);
            return this;
        }

        public FluentHtml NewLine()
        {
            Context.Writer.NewLine();
            return this;
        }

        public FluentHtml Space()
        {
            Context.Writer.Space();
            return this;
        }

        public FluentHtml Snippet(string text, string newline = null)
        {
            Context.Writer.Snippet(text, newline);
            return this;
        }

       
    }
}