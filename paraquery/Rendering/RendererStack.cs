using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{
    /*

        RendererStack provides a stack of child Renderers.

        Begin() is called on Renderers when Pushed(), and End() is called when Popped().

        If you Push() a Block under an Inline, all the Renderers up to and including the last Block are Closed()/Ended().

        Otherwise, Renderers are nested.

        TODO what happens when you push a container? So far containers are simply unformatted renders that aren't inline. But what to do?

    */

    public abstract class RendererStack : EndBase
    {
        protected Stack<Renderer> _stack = new Stack<Renderer>();


        protected override void OnEnd()
        {
            CloseAll();
        }

        protected void Push(Renderer renderer)
        {
            if (_stack != null)
            {
                //if we're putting a block under a non-block, close all renderers up to and including the last block and start a new one
                //else nest this renderer inside the last renderer
                if (renderer is BlockRenderer)
                {
                    if (_stack.Count > 0)
                    {
                        Renderer top = _stack.Peek();

                        if (!(top is BlockRenderer))
                        {
                            CloseBlock();
                        }
                    }
                }

                //begin it
                renderer.Begin();

                //push it
                _stack.Push(renderer);
            }
            else
            {
                throw new Exception("Can't add renderers to an empty renderer");
            }
        }


        protected void Pop()
        {
            if (_stack?.Count > 0)
            {
                Renderer top = _stack.Pop();
                top.End();
            }
        }

        public void Close()
        {
            //close last renderer
            Pop();
        }


        public void CloseBlock()
        {
            //end all non-block renderer up to and including the last block
            while (_stack?.Count > 0)
            {
                Renderer top = _stack.Peek();

                if (top is BlockRenderer)
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
            //end all renderers on stack
            while (_stack?.Count > 0)
            {
                Pop();
            }

        }

    }
}