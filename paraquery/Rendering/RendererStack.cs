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
        protected Stack<Renderer> Stack { private set; get; } = new Stack<Renderer>();

        public RendererStack(Context context, FormatModes formatMode, StackModes stackMode) : base(context, formatMode, stackMode)
        {
        }

        protected override void OnEnd()
        {
            CloseAll();
        }

        public virtual Renderer Top
        {
            get
            {
                return Stack.Peek();
            }
        }

        internal virtual void Push(Renderer renderer)
        {
            //we don't want nulls on the stack
            if (renderer != null)
            {
                //if we're putting a non-inline under an inline,
                //or anything under a line,
                //close all renderers up to and including the last non-inline and start a new one
                //else nest this renderer inside the last renderer

                if (Stack.Count > 0)
                {
                    Renderer top = Stack.Peek();

                    if (renderer.StackMode != StackModes.Inline && top.StackMode == StackModes.Inline) 
                    {
                        CloseInline();
                    }
                    else if (top.StackMode == StackModes.Line)
                    {
                        Close();
                    }
                }

                //begin it
                renderer.Begin();

                //push it
                Stack.Push(renderer);
            }
        }

        public virtual void Open(Renderer renderer)
        {
            Push(renderer);
        }

        internal virtual void Pop()
        {
            if (Stack.Count > 0)
            {
                Renderer top = Stack.Pop();

                //derived classes can always bypass Push()
                if (top!=null)
                {
                    top.End();
                }
            }
        }

        public virtual void Close()
        {
            //close last renderer
            Pop();
        }

        public virtual void Close(Renderer renderer)
        {
            //end all renderers up to and including the specified one
            while (Stack.Count > 0)
            {
                Renderer top = Stack.Peek();

                if (top == renderer)
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

        public virtual void CloseInline()
        {
            //end all inline renderers up to but NOT including the last non-inline
            while (Stack.Count > 0)
            {
                Renderer top = Stack.Peek();

                if (top.StackMode == StackModes.Inline)
                {
                    Pop();
                }
                else
                {
                    break;
                }
            }
        }

        public virtual void CloseBlock()
        {
            //end all inline renderers up to AND including the last non-inline
            while (Stack.Count > 0)
            {
                Renderer top = Stack.Peek();

                if (top.StackMode != StackModes.Inline)
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
            while (Stack.Count > 0)
            {
                Pop();
            }

        }

    }
}