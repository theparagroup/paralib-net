using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Blocks;

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

        public new FluentHtml Write(string content, bool? indent = null)
        {
            base.Write(content, indent);
            return this;
        }

        public new FluentHtml WriteLine(string content, bool? indent = null)
        {
            base.WriteLine(content, indent);
            return this;
        }

        public new FluentHtml NewLine()
        {
            base.NewLine();
            return this;
        }

        public new FluentHtml Space()
        {
            base.Space();
            return this;
        }

        public new FluentHtml Snippet(string name, string text, bool indent = true, string newline = null)
        {
            base.Snippet(name, text, indent, newline);
            return this;
        }

    }
}