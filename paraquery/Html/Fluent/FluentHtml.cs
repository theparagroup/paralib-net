using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;
using com.paraquery.Rendering;

namespace com.paraquery.Html.Fluent
{
    public partial class FluentHtml : RendererStack
    {
        protected TagBuilder _tagBuilder;

        public FluentHtml(TagBuilder tagBuilder) : base(tagBuilder.Context, RenderModes.Inline, true)
        {
            _tagBuilder = tagBuilder;

            //let's start with an html marker
            Push(new Html(_context));
        }

        protected override void Comment(string text)
        {
            HtmlRenderer.HtmlComment(_writer, text);
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
            _context.Writer.Write(content);
            return this;
        }

        public FluentHtml WriteLine(string content)
        {
            _context.Writer.WriteLine(content);
            return this;
        }

        public FluentHtml NewLine()
        {
            _context.Writer.NewLine();
            return this;
        }

        public FluentHtml Space()
        {
            _context.Writer.Space();
            return this;
        }

        public FluentHtml Snippet(string text, string newline = null)
        {
            _context.Writer.Snippet(text, newline);
            return this;
        }

       
    }
}