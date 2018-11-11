using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{
    /*

        RendererStack provides a stack of (nested) renderers. The goal of the RenderStack is to
        make it simple to create structured content by simply pushing and popping renderers onto 
        the stack. This is helpful in general, but extemely helpful when creating fluent interfaces.

        RendererStack is abstract, and therefore doesn't implement OnBegin(). Dervived classes may,
        of course, do something there if they want to. It's not required however, and, as it explained
        below, renderers added to the stack will render thier start content (Begin() will be called)
        as they are added to the stack. As other Renderers are added, end content will be rendered (End()
        will be called) according to various rules (also described below). Finally the RendererStack's
        End() method should be called, and all lingering end content will be rendered.

        It is up to the instatiator to wrap the RendererStack in a using() statement or else call End()
        explicitly.

        When Renderers are pushed onto the stack, we look at a couple of renderer properties to decide
        what to do:

            StackMode
            Terminal

        StackMode can have the following values:

            Nested:    may contain Linear and other Nested renderers
            Linear:    may only contain other Linear renderers

        Terminal means the renderer cannot contain other renderers.

        Note, we're talking about nesting renderers inside other renderers, not "content". Any
        StackMode may have nested "content" between the Begin() and End() calls, such as
        text or code. 

        Derived classes may use these properties to make other decisions about how to render themselves.
        For example, Tag treats Terminal the same Empty for HTML purposes (which makes sense because
        empty tags can't have any content at all).

        RenderStack uses these properties to determine what to do with existing renderers on the stack
        whenever a new render is pushed:

            Pushing anything onto a Terminal, closes that Terminal.
            Pushing a Nested onto a Linear, closes all the Linears up to but not including the last Nested.

        Also, keep in mind, things higher on the stack are "under" things lower on the stack,
        when rendered. The top of the stack represents the last thing that had Begin()
        called, and is the renderer currently nested the deepest. Can be confusing.     

        We keep the Stack, Push() and Pop() internal and provide Open() and Close() methods
        for implementors of custom RendererStacks.

        RendererStack is itself a Renderer, so RendererStacks can be pushed into other RendererStacks.
        However, because nesting RendererStacks could cause formatting and structure to get messed up,
        we enforce some rules:

            RendererStacks must declare thier own LineMode and StackMode.
            RendererStacks are never Terminal. They are designed to contain other renderers.
            The first renderer pushed onto the stack must match the RendererStack's StackMode.
            If the RendererStack is Linear, all pushed renderers must be Linear as well.
            If the RendererStack is Visible, you can't push Multiline renderers under non-Multiline.

        Essentially, these rules ensure that a nested RendererStack's content behaves the same way
        you have declared the RendererStack's behavior to behave. This is crucial for creating re-usable
        components that can be arbitrarily nested.


    */

    public abstract class RendererStack : Renderer
    {
        internal Stack<Renderer> Stack { private set; get; } = new Stack<Renderer>();

        public RendererStack(Context context, LineModes lineMode, StackModes stackMode, bool visible, bool indent) : base(context, lineMode, stackMode, false, visible, indent)
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
                if (Stack.Count > 0)
                {
                    return Stack.Peek();
                }
                else
                {
                    return null;
                }
            }
        }

        internal virtual void Push(Renderer renderer)
        {
            //we don't want nulls on the stack
            if (renderer != null)
            {

                if (Stack.Count == 0)
                {
                    //the first renderer on the stack must match the RendererStack's StackMode
                    if (StackMode != renderer.StackMode)
                    {
                        throw new InvalidOperationException($"The first Renderer's StackMode {renderer.StackMode} is incompatible with RendererStack's StackMode {StackMode}");
                    }
                }

                if (StackMode == StackModes.Linear && renderer.StackMode != StackModes.Linear)
                {
                    /*
                        StackModes for subsequently pushed renderers:
                            If we're Linear, all pushed renderers must be Linear.
                            If we're Nested, we don't care.
                    */

                    throw new InvalidOperationException($"Renderer's StackMode {renderer.StackMode} is incompatible with RendererStack's StackMode {StackMode}");
                }


                if (Visible)
                {
                    if ((LineMode == LineModes.None || LineMode == LineModes.Single) && (renderer.LineMode != LineModes.None && renderer.LineMode != LineModes.Single))
                    {
                        /*
                            LineModes for visible renderers:
                                If we're None or Single, all pushed renderers must be None.
                                If we're Multiple, we don't care.
                        */

                        throw new InvalidOperationException($"Renderer's LineMode {renderer.LineMode} is incompatible with RendererStack's LineMode {LineMode}");
                    }
                }

                if (Stack.Count > 0)
                {
                    Renderer top = Stack.Peek();

                    if (top.Terminal)
                    {
                        //anything under a terminal pops that terminal
                        Pop();
                    }
                    else if (top.StackMode == StackModes.Linear && renderer.StackMode == StackModes.Nested)
                    {
                        //pushing nested under a linear pops all linears up to but not including the last nested
                        PopLinears(false);
                    }
                }

                //begin it
                renderer.Begin();

                //push it
                Stack.Push(renderer);
            }
        }

        public virtual Renderer Open(Renderer renderer)
        {
            Push(renderer);
            return renderer;
        }

        internal virtual void Pop()
        {
            if (Stack.Count > 0)
            {
                Renderer top = Stack.Pop();

                //derived classes can always bypass Push()
                if (top != null)
                {
                    top.End();
                }
            }
        }

        internal virtual void PopLinears(bool includeOuterNested)
        {
            while (Stack.Count > 0)
            {
                Renderer top = Stack.Peek();

                if (top.StackMode == StackModes.Linear)
                {
                    Pop();
                }
                else if (top.StackMode == StackModes.Nested)
                {
                    if (includeOuterNested)
                    {
                        Pop();
                    }

                    break;
                }
            }
        }

        public virtual void Close()
        {
            Pop();
        }

        public virtual void Close(Renderer renderer)
        {
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

        protected virtual void CloseLinears(bool includeOuterNested)
        {
            PopLinears(includeOuterNested);
        }

        public virtual void CloseAll()
        {
            while (Stack.Count > 0)
            {
                Pop();
            }

        }

    }
}