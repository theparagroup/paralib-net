using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{
    /*

        RendererStack provides a stack of Renderers.

        RenderStack is itself a Renderer, so RendererStacks can be pushed into other RendererStacks.

        RendererStack is abstract, and therefore doesn't implement OnBegin(). Dervived classes may,
        of course, do something there if they want to. It's not required however, and, as it explained
        below, Renderers added to the stack will render thier start content (Begin() will be called)
        as they are added to the stack. As other Renderers are added, end content will be rendered (End()
        will be called) according to various rules (also described below). Finally the RendererStack's
        End() method should be called, and all lingering end content will be rendered.

        It is up to the instatiator to wrap the RendererStack in a using() statement or else call End()
        explicitly.

        Renderers at the top are "nested" inside lower Renderers when rendered to the writer stream.

        This stack (div at top):

            div
            span
            strong

        Would be rendered:

            <div>
                <span><strong>content</strong></span>
            </div>


        Semantics/Rules:

            Begin() is called on Renderers when they are Pushed(), and End() is called when Popped().

            In between Push/Begin and Pop/End calls, you can push more renderers or generate content with the Writer.

            If you Push() a Block or a Line (i.e., a non-Inline) under an Inline, all the Renderers up to and including 
            the last Block are Closed()/Ended() first.

            If you Push() anything under a Line, Close()/End() the Line first.

            Otherwise, Renderers are nested.

    */

    public abstract class RendererStack : Renderer
    {
        protected Stack<Renderer> _stack = new Stack<Renderer>();

        public RendererStack(IContext context, RenderModes renderMode, bool visible = true) : base(context, renderMode, visible)
        {
        }

        protected override void OnEnd()
        {
            CloseAll();
        }

        protected void Push(Renderer renderer)
        {
            //if we're putting a non-inline under an inline,
            //or anything under a line,
            //close all renderers up to and including the last non-inline and start a new one
            //else nest this renderer inside the last renderer

            if (_stack.Count > 0)
            {
                Renderer top = _stack.Peek();

                if ((renderer.RenderMode != RenderModes.Inline && top.RenderMode == RenderModes.Inline) || top.RenderMode == RenderModes.Line)
                {
                    CloseBlock();
                }
            }

            //begin it
            renderer.Begin();

            //push it
            _stack.Push(renderer);
        }


        protected virtual void Pop()
        {
            if (_stack.Count > 0)
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
            while (_stack.Count > 0)
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
            while (_stack.Count > 0)
            {
                Pop();
            }

        }

    }
}