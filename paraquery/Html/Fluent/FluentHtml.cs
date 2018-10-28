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
        protected IContext _context;
        protected IWriter _writer;

        public FluentHtml(TagBuilder tagBuilder) 
        {
            _tagBuilder = tagBuilder;
            _context = _tagBuilder.Context;
            _writer = _context.Writer;
        }

        protected TagBuilder TagBuilder
        {
            get
            {
                return _tagBuilder;
            }
        }

        protected new FluentHtml Push(Renderer renderer)
        {
            //this method is just to simplify fluent methods...
            base.Push(renderer);
            return this;
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