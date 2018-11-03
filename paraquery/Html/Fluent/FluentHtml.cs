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

        public FluentHtml(HtmlContext context) : base(context, new Marker(context))
        {

        }

        public FluentHtml(HtmlContext context, HtmlRenderer marker) : base(context, marker)
        {

        }

        public class Marker : HtmlRenderer
        {
            public Marker(HtmlContext context) : base(context, context.Options.CommentFluentHtml ? FormatModes.Block : FormatModes.None, StructureModes.Block)
            {
            }

            protected override void OnBegin()
            {
                if (Context.Options.CommentFluentHtml)
                {
                    Comment("fluent html start");
                }
            }

            protected override void OnEnd()
            {
                if (Context.Options.CommentFluentHtml)
                {
                    Comment("fluent html end");
                }
            }
        }

        protected override void OnBegin()
        {
            //do nothing, the marker does it all
        }

        protected override void OnEnd()
        {
            CloseAll();
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

        public new FluentHtml Open(Renderer renderer)
        {
            return Push(renderer);
        }

        public new FluentHtml CloseInline()
        {
            base.CloseInline();
            return this;
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