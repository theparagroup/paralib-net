using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Blocks;

namespace com.paraquery.Html.Fluent
{
    /*

        ElementContainer provides a stack of child elements, and handles the details of mixing block and inline elements together.

        The standard behavior when adding an element after another element is to nest, however, if you add a block after an inline,
        all elements up to and including the last block will be closed.

    */

    public partial class ElementContainer : SimpleBlock
    {
        protected Stack<Element> _stack = new Stack<Element>();

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

            //begin it
            element.Begin();

            //push it
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

        public void Close()
        {
            //close last element
            Pop();
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

    }
}