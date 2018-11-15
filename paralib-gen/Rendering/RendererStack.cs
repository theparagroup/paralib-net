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

            Pushing anything onto a None, pops the None renderer.
            Pushing a non-Inline (None|Block) onto an Inline, pops all the Inlines up to but not 
                including the Block.

        Note: there should never be an open none above any renderers, that is, Nones only exist 
        on top of the stack.

        Also, keep in mind, things higher on the stack are "under" things lower on the stack,
        when rendered. The top of the stack represents the last thing that had Begin()
        called, and is the renderer currently nested the deepest. Can be confusing.     

        We keep the Stack, Push() and Pop() protected and provide Open() and Close() methods
        for instantiators of RendererStacks.

        RendererStack can be implmented  itself a Renderer, so RendererStacks can be pushed into other 
        RendererStacks. 

        When the we (as the outer stack) sees a rendererstack on our stack, we want to treat the
        contents as if they belonged to us. 

        This is the basic scenario:

                        B1
                        I1
                        RS -> Ia Ib Ic
                        I2
            push B2 ->  _   

        When the last block (B2) is pushed, we need to pop the first inline (I2) we see, then 
        start popping inlines off the rendererstack (RS), Ic, Ib, Ia.

        If we run into a block on RS, we stop there.

        If we empty RS, we pop it and continue up our stack, popping inlines till we hit a block.

    */

    public class RendererStack 
    {
        protected Stack<IRenderer> Stack { private set; get; } = new Stack<IRenderer>();
        protected bool NoLineBreaks { private set; get; }

        public RendererStack(bool noLineBreaks)
        {
            NoLineBreaks = noLineBreaks;
        }

        public virtual int Count
        {
            get
            {
                return Stack.Count;
            }
        }

        public virtual IRenderer Top
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

        protected static IRenderer FindTop(RendererStack rendererStack)
        {
            if (rendererStack.Top is IHasRendererStack)
            {
                return FindTop(((IHasRendererStack)rendererStack.Top).RendererStack);
            }
            else
            {
                return rendererStack.Top;
            }
        }

        protected virtual void Push(IRenderer renderer)
        {

            //we never want nulls on the stack
            if (renderer != null)
            {
                /*

                */

                if (NoLineBreaks && renderer.LineMode != LineModes.None)
                { 
                    throw new InvalidOperationException($"Renderer's LineMode {renderer.LineMode} is not allowed when RendererStack is in NoLineBreaks mode");
                }


                /*
                    Before we Begin() and Push() this new renderer, we want to clean up the stack first.

                    This means:

                        If we're pushing onto a renderer that has it's own renderer stack, we need to walk
                        that stack and close

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
                    IRenderer top = Stack.Peek();


                    if (top.ContainerMode == ContainerModes.None)
                    {
                        Pop();
                    }
                    else
                    {
                        //since we always pop nones first when pushing,
                        //we know that the top is either a block or inline
                        //unless it is a nested rendererstack, in which case
                        //we need it's "top"

                        if (top is IHasRendererStack)
                        {
                            var rs = ((IHasRendererStack)top).RendererStack;
                            top = FindTop(rs);
                        }

                        if (top.ContainerMode==ContainerModes.None || (top.ContainerMode==ContainerModes.Inline && renderer.ContainerMode != ContainerModes.Inline))
                        {
                            //if the top is a None,
                            //or if we're pushing a non-inline onto an inline, 
                            //clear inlines to last block
                            PopToBlock(false);
                        }
                    }
                }

                //begin it
                renderer.Begin();

                //push it
                Stack.Push(renderer);
            }
        }

        protected virtual void Pop()
        {
            if (Stack.Count > 0)
            {
                IRenderer top = Stack.Pop();

                //derived classes can always bypass Push()
                if (top != null)
                {
                    top.End();
                }
            }
        }

        protected virtual bool PopToBlock(bool popBlock)
        {
            //pop Nones/Inlines till we hit a block
            //we follow renderstacks and pop them if empty

            while (Stack.Count > 0)
            {
                IRenderer top = Stack.Peek();

                //The order is important here(renderer stacks first).

                if (top is IHasRendererStack)
                {
                    /*
                        we can implement IRenderer on a custom rendererstack
                        but that will require some extra effort on our part
                        to handle such a scenario
                    
                        pop rs till we hit a block on its stack

                        if we empty the rs, pop it

                        note: if rs is inline, we'll empty it, just
                            as if we popped the rs itself (End->CloseAll),
                            it's the same thing, so we just ignore container
                            mode here
                    */
                    var rs = ((IHasRendererStack)top).RendererStack;
                    var blockfound=rs.PopToBlock(popBlock);

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
                        break;
                    }
                    else if (top.ContainerMode == ContainerModes.Inline)
                    {
                        Pop();
                        continue;
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


        public virtual IRenderer Open(IRenderer renderer)
        {
            Push(renderer);
            return renderer;
        }

        public virtual void Close()
        {
            Pop();
        }

        public virtual void Close(IRenderer renderer)
        {
            while (Stack.Count > 0)
            {
                IRenderer top = Stack.Peek();

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
            PopToBlock(false);
        }

        public void CloseBlock()
        {
            PopToBlock(true);
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