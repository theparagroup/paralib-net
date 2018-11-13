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
            //we prevent strange stuff from happening, like:
            //  pushing stack2 onto stack1 (stack2 begins)
            //  poping stack1 (stack2 ends)
            //  pushing onto stack2 (don't do this)
            if (!_begun)
            {
                throw new Exception("Can't use RendererStack without calling Begin()");
            }

            //we never want nulls on the stack
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
                    throw new InvalidOperationException($"RendererStack '{Name}' cannot contain Renderer '{renderer.Name}' because it is not Inline");
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
                        throw new InvalidOperationException($"RendererStack '{Name}' cannot container Renderer {renderer.Name} because its LineMode '{renderer.LineMode}' is incompatible");
                    }
                }


                /*
                    Before we Begin() and Push() this new renderer, we want to clean up the stack first.

                    This means:

                        If we're pushing onto a None, Pop() it. According to the rules, the next renderer after
                        the None should be a Block, so that's all we need to do.

                        Otherwise, if we're a Block, we need to Pop() any Inlines starting at the top of the 
                        stack and working down till we hit the next Block, and then stop. If we run into any
                        RendererStacks on the way, treat their contents as if they were on our stack (that is,
                        we stop when we hit a block on a nested rendererstack, and if we empty a rendererstack,
                        we pop it off our stack as well).
                
                */
                if (Stack.Count > 0)
                {
                    Renderer top = Stack.Peek();
                    if (top.ContainerMode == ContainerModes.None)
                    {
                        Pop();
                    }
                    else if (renderer.ContainerMode == ContainerModes.Block)
                    {
                        PopInlines(false);
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

        protected virtual bool PopInlines(bool popBlock)
        {
            //pop Nones/Inlines till we hit a block
            //we follow renderstacks and pop them if empty

            while (Stack.Count > 0)
            {
                Renderer top = Stack.Peek();

                if (top is RendererStack)
                {
                    /*
                        pop rs till we hit a block on its stack
                        note if rs is inline, we'll empty it, just
                        as if we popped the rs itself (End->CloseAll),
                        it's the same thing, so we just ignore container
                        mode here
                    */
                    var rs = (RendererStack)top;
                    var blockfound=rs.PopInlines(popBlock);

                    //if we emptied rs, pop rs too
                    if (rs.Stack.Count == 0)
                    {
                        Pop();
                    }

                    //in case the last thing on rs was a block
                    //then we're done
                    if (blockfound)
                    {
                        return true;
                    }
                }
                else
                {
                    if (top.ContainerMode == ContainerModes.None)
                    {
                        Pop();
                    }
                    else if (top.ContainerMode == ContainerModes.Inline)
                    {
                        Pop();
                    }
                    else if (top.ContainerMode == ContainerModes.Block)
                    {
                        if (popBlock)
                        {
                            Pop();
                        }

                        return true;
                    }
                }

            }

            return false;
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

        public void CloseUp()
        {
            PopInlines(false);
        }

        public void CloseBlock()
        {
            PopInlines(true);
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