using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Blocks;

namespace com.paraquery.Html.Fluent
{
    public partial class FluentHtml : SimpleBlock
    {
        protected TagBuilder _tagBuilder;
        protected Stack<Element> _stack = new Stack<Element>();
        protected bool _inlining;

        public FluentHtml(IContext context, TagBuilder tagBuilder) : base(context)
        {
            _tagBuilder = tagBuilder;
        }

        protected override void OnEnd()
        {
            CloseAll();
        }

        protected TagBuilder TagBuilder
        {
            get
            {
                return _tagBuilder;
            }
        }

        protected FluentHtml Push(Element element)
        {
            _stack.Push(element);
            return this;
        }

        protected void Pop()
        {
            if (_stack.Count > 0)
            {
                Element element = _stack.Pop();
                element.End();
            }
        }

        protected void NewBlock()
        {
            //reset inline content flag
            _inlining = false;

            //if current is inline, close the current block
            //else nest

            if (_stack.Count > 0)
            {
                Element top = _stack.Peek();
                if (top.ElementType == ElementTypes.Inline)
                {
                    CloseBlock();
                }
            }
        }

        protected void NewInline()
        {
            //if we're putting an inline directly under a block for the first time, 
            //we need to tab manually,
            //as inline elements are not formatted

            if (!_inlining)
            {

                _inlining = true;

                if (_stack.Count > 0)
                {
                    Element top = _stack.Peek();

                    if (top.ElementType == ElementTypes.Block)
                    {
                        _response.Tabs();
                    }

                }

            }

        }

        public FluentHtml CloseBlock()
        {
            //end all inline elements up to and including the last block
            while (_stack.Count > 0)
            {
                Element top = _stack.Peek();

                if (top.ElementType == ElementTypes.Inline)
                {
                    Pop();
                }
                else
                {
                    Pop();
                    break;
                }
            }

            return this;
        }

        public FluentHtml CloseAll()
        {
            //end all elements on stack
            while (_stack.Count > 0)
            {
                Pop();
            }

            return this;
        }

        public FluentHtml Close()
        {
            Pop();
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

            _context.Response.Write(content, indent.Value);

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

            _context.Response.WriteLine(content, indent.Value);

            return this;
        }

        public FluentHtml NewLine()
        {
            return this;
        }

    }
}