using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Blocks;

namespace com.paraquery.Html.Fluent
{
    public partial class ElementContainer : SimpleBlock
    {
        protected Stack<Element> _stack = new Stack<Element>();
        protected bool _inlining;
        protected bool _writeIndented;


        public ElementContainer(IContext context) : base(context)
        {
        }

        protected override void OnEnd()
        {
            CloseAll();
        }

        protected void Push(Element element)
        {
            if (element is BlockElement)
            {
                //reset inline content flag
                _inlining = false;


                //if we're putting a block under an inline, close it
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
            else //inline
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
                            _writer.Tabs();
                        }

                    }

                }
            }

            //push it
            _stack.Push(element);

            //clear write indented if starting a new block
            if (element.ElementType == ElementTypes.Block)
            {
                _writeIndented = false;
            }
        }


        protected void Pop()
        {
            if (_stack.Count > 0)
            {
                Element element = _stack.Pop();
                element.End();
            }
        }
       
        public void CloseBlock()
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
        }

        public void CloseAll()
        {
            //end all elements on stack
            while (_stack.Count > 0)
            {
                Pop();
            }

        }

        public void Close()
        {
            Pop();
        }

        public void Write(string content, bool? indent = null)
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

            _context.Writer.Write(content, indent ?? false);
        }

        public void WriteLine(string content, bool? indent = null)
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

            _context.Writer.WriteLine(content, indent ?? false);

            //newline, so from here on we're not "write indented"
            _writeIndented = false;

        }

        public void NewLine()
        {
            _context.Writer.NewLine();

            //newline, so from here on we're not "write indented"
            _writeIndented = false;
        }

        public void Space()
        {
            _context.Writer.Space();

            //space ends on a newline, so from here on we're not "write indented"
            _writeIndented = false;
        }

        public virtual void Snippet(string name, string text, bool indent = true, string newline = null)
        {
            _context.Writer.Snippet(name, text, indent, newline);
        }

    }
}