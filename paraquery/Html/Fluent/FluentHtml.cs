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

        public FluentHtml Write(string content, bool? indent = null)
        {
            if (!indent.HasValue)
            {
                indent = false;

                if (_stack.Count > 0)
                {
                    indent = _stack.Peek().ElementType == ElementTypes.Block;
                }
            }

            _context.Writer.Write(content, indent.Value);

            return this;
        }

        public FluentHtml WriteLine(string content, bool? indent = null)
        {
            if (!indent.HasValue)
            {
                indent = false;

                if (_stack.Count > 0)
                {
                    indent = _stack.Peek().ElementType == ElementTypes.Block;
                }
            }

            _context.Writer.WriteLine(content, indent.Value);

            return this;
        }

        public FluentHtml NewLine()
        {
            _context.Writer.NewLine();
            return this;
        }

    }
}