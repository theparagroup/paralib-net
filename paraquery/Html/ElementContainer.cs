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

        public ElementContainer(IContext context) : base(context)
        {
        }

        protected override void OnEnd()
        {
            CloseAll();
        }

        protected void Push(Element element)
        {
            _stack.Push(element);
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
                        _writer.Tabs();
                    }

                }

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
       

    }
}