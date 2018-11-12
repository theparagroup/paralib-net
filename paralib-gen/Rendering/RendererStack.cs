using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Rendering
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

            ContainerMode
            LineMode

        ContainerMode can have the following values:

            None:      may not contain any renderers
            Block:     may contain both Block and Inline renderers
            Inline:    may only contain other Inline renderers

        Note, we're talking about nesting renderers inside other renderers, not "content". Any
        ContainerMode may have "content" between the Begin() and End() calls, such as
        text or code. 

        Derived classes may use these properties to make other decisions about how to render themselves.
        For example, Tag treats None the as same Empty for HTML purposes (which makes sense because
        empty tags can't have any content at all).

        RenderStack uses these properties to determine what to do with existing renderers on the stack
        whenever a new render is pushed:

            Pushing anything onto a None, closes the None renderer.
            Pushing a non-Inline (None|Block) onto an Inline, closes all the Inlines up to but not 
                including the last Block.

        Also, keep in mind, things higher on the stack are "under" things lower on the stack,
        when rendered. The top of the stack represents the last thing that had Begin()
        called, and is the renderer currently nested the deepest. Can be confusing.     

        We keep the Stack, Push() and Pop() internal and provide Open() and Close() methods
        for implementors of custom RendererStacks.

        RendererStack is itself a Renderer, so RendererStacks can be pushed into other RendererStacks.
        However, because nesting RendererStacks could cause formatting and structure to get messed up,
        we enforce some rules:

            RendererStacks must declare thier own LineMode and ContainerMode.
            RendererStacks are never None. They are designed to contain other renderers.
            If the RendererStack is Inline, all pushed renderers must be Inline as well.
            If the RendererStack is Visible and LineMode is None|Single, all pushed renderers must be None.

        Essentially, these rules ensure that a nested RendererStack's content behaves the same way
        you have declared the RendererStack's behavior to behave. This is crucial for creating re-usable
        components that can be arbitrarily nested.

        Nested RendererStacks:

    */

    public abstract class RendererStack : Renderer
    {
        protected Stack<Renderer> Stack { private set; get; } = new Stack<Renderer>();

        public RendererStack(Context context, LineModes lineMode, ContainerModes containerMode, bool visible, bool indentContent) : base(context, lineMode, containerMode, visible, indentContent)
        {
            if (ContainerMode==ContainerModes.None)
            {
                throw new InvalidOperationException("RendererStack's ContainerMode cannot be None");
            }

        }

        protected override void DoEnd()
        {
            CloseAll();
            base.DoEnd();
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

        protected virtual void Push(Renderer renderer)
        {
            //we don't want nulls on the stack
            if (renderer != null)
            {
                /*
                    We ensure that if the RendererStack itself is Inline, it only contains Inlines

                    This is important if this RendererStack is itself pushed onto another stack.

                    The renderer before this stack may be inline, so pushing a Bbock or a none into
                    this (inline) stack would clear out any inlines in here, but do nothing about the 
                    inline outside, messing up the formatting.

                */
                if (ContainerMode == ContainerModes.Inline && renderer.ContainerMode != ContainerModes.Inline)
                {
                    throw new InvalidOperationException($"RendererStack is Inline and may only contain other Inline renderers");
                }

                /*
                    Here we don't want to push single or multiple line mode renders onto none or single, messing
                    up the formatting
                    
                    Rules for visible renderers:
                        If we're None or Single, all pushed renderers must be None.
                        If we're Multiple, we don't care.
                */
                if (Visible)
                {
                    if ((LineMode == LineModes.None || LineMode == LineModes.Single) && (renderer.LineMode!=LineModes.None))
                    {
                        throw new InvalidOperationException($"Renderer's LineMode {renderer.LineMode} is incompatible with RendererStack's LineMode {LineMode}");
                    }
                }

                if (Stack.Count > 0)
                {
                    Renderer top = Stack.Peek();

                    if (top.ContainerMode==ContainerModes.None)
                    {
                        //anything under a None pops that None
                        Pop();
                    }
                    else if (top.ContainerMode == ContainerModes.Inline && renderer.ContainerMode != ContainerModes.Inline)
                    {
                        //pushing a non-inline under an inline pops all inline up to but not including the last block
                        PopInlines(false);
                    }
                    else if (top is RendererStack)
                    {
                        //are we pushing a renderer onto another rendererstack?
                        //then we need to manipulate that rendererstack under certain conditions

                        var rs = (RendererStack)top;

                        if (rs.Top.ContainerMode==ContainerModes.None)
                        {
                            //if the last thing on the top stack is a None, close it
                            rs.Close();
                        }
                        else if (rs.Top.ContainerMode==ContainerModes.Inline && renderer.ContainerMode != ContainerModes.Inline)
                        {
                            //note: we can only get here for top stacks that are Blocks, as we would have popped an inline stack
                            //in the else block above

                            //just as we do above (for ourselves), we need to close any inlines on the top stack
                            //if we're pushing a non-inline.
                            rs.CloseInlines(false);
                        }
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

        protected virtual void Pop()
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

        protected virtual void PopInlines(bool includeOuterNested)
        {
            while (Stack.Count > 0)
            {
                Renderer top = Stack.Peek();

                if (top.ContainerMode == ContainerModes.Inline)
                {
                    Pop();
                }
                else if (top.ContainerMode == ContainerModes.Block)
                {
                    if (includeOuterNested)
                    {
                        Pop();
                    }

                    break;
                }
                else
                {
                    //we should never see a none 
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

        protected virtual void CloseInlines(bool includeOuterNested)
        {
            PopInlines(includeOuterNested);
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