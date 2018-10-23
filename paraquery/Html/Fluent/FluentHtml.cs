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
        protected bool _writeIndented; 

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

        protected override void OnPush(Element element)
        {
            //clear write indented if starting a new block
            if (element.ElementType == ElementTypes.Block)
            {
                _writeIndented = false;
            }
        }

        public FluentHtml Write(string content, bool? indent = null)
        {
            //if we haven't already indented during a write and caller doesn't specify, let's see if we're under a block and auto indent
            if (!_writeIndented)
            {
                if (!indent.HasValue)
                {
                    indent = false;

                    if (_stack.Count > 0)
                    {
                        indent = (_stack.Peek().ElementType == ElementTypes.Block);
                    }
                }

                //save state for future writes/writelines
                _writeIndented = indent.Value;

            }

            _context.Writer.Write(content, indent??false);


            return this;
        }

        public FluentHtml WriteLine(string content, bool? indent = null)
        {
            //if we haven't already indented during a write and caller doesn't specify, let's see if we're under a block and auto indent
            if (!_writeIndented)
            {
                if (!indent.HasValue)
                {
                    indent = false;

                    if (_stack.Count > 0)
                    {
                        indent = (_stack.Peek().ElementType == ElementTypes.Block);
                    }
                }
            }

            _context.Writer.WriteLine(content, indent??false);

            //newline, so from here on we're not "write indented"
            _writeIndented = false;

            return this;
        }

        public FluentHtml NewLine()
        {
            _context.Writer.NewLine();

            //newline, so from here on we're not "write indented"
            _writeIndented = false;

            return this;
        }

    }
}