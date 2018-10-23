using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Blocks;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html.Fluent
{
    public partial class FluentHtml : ElementContainer
    {
        protected TagBuilder _tagBuilder;

        public FluentHtml(IContext context, TagBuilder tagBuilder) : base(context)
        {
            _tagBuilder = tagBuilder;
        }

        protected TagBuilder TagBuilder
        {
            get
            {
                return _tagBuilder;
            }
        }

        protected new FluentHtml Push(Element element)
        {
            base.Push(element);
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