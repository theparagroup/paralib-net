using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{
    /*

        RendererStack provides a stack of Renderers.

        Renderers at the top are "nested" inside lower Renderers.

        div - top
        span
        strong

        Would be rendered:

            <div>
                <span><strong>content</strong></span>
            </div>


        Begin() is called on Renderers when they are Pushed(), and End() is called when Popped().

        In between Push/Begin and Pop/End calls, you can push more renderers or generate content with the Writer.

        If you Push() a Block under an Inline, all the Renderers up to and including the last Block are Closed()/Ended().

        Otherwise, Renderers are nested.

    */

    public class RendererStack : EndBase
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
                //if we're putting a non-inline under an inline,
                //or anything under a line,
                //close all renderers up to and including the last non-inline and start a new one
                //else nest this renderer inside the last renderer

                if (_stack.Count > 0)
                {
                    Renderer top = _stack.Peek();

                    if ((renderer.RenderMode != RenderModes.Inline && top.RenderMode == RenderModes.Inline) || top.RenderMode== RenderModes.Line)
                    {
                        CloseBlock();
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


        protected virtual void Pop()
        {
            if (_stack?.Count > 0)
            {
                Renderer top = _stack.Pop();
                top.End();
            }
        }

        public virtual void Open(Renderer renderer)
        {
            Push(renderer);
        }

        public virtual void Close()
        {
            //close last renderer
            Pop();
        }


        public virtual void CloseBlock()
        {
            //end all inline renderers up to and including the last non-inline
            while (_stack?.Count > 0)
            {
                Renderer top = _stack.Peek();

                if (top.RenderMode!=RenderModes.Inline)
                {
                    Pop();
                    break;
                }
                else
                {
                    Pop();
                }
            }
        }

        public virtual void CloseAll()
        {
            //end all renderers on stack
            while (_stack?.Count > 0)
            {
                Pop();
            }

        }

    }
}