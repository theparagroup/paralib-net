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

        RenderStack is itself a Renderer, so RendererStacks can be pushed into other RendererStacks.

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

            StructureMode
            Empty

        StructureMode can have the following values:

            Inline:    may have nested renderers, unless explicitly marked "Empty"
            Line:      does not have nested renderers, always implicitly "empty"
            Block:     may have nested renderers, but considered implicitly "not empty"

        Note, we're talking about nesting renderers inside other renderers, not "content". Any
        StructureMode may have nested "content" between the Begin() and End() calls, such as
        text or code. This is different from 

        Empty, obviously, only makes sense for Inline renderers, which may or may not have content.
        The other two types either always have content, or never have content. as they and the only
        class that cares about it for structure purposes is the RendererStack (although other
        classes may use Empty for non-structural reasons, like how "Tag" renders start/end tags).

        By combining StructureMode with Empty and FormatMode, various formatting effects can be
        achieved by simply pushing things onto the stack and calling End().

        Generally, StructureModes and FormatModes should match on a Renderer like this:

            Inline -> None
            Line -> Line
            Block -> Block
            
        If they don't, you can screw up the formatting and get blank lines with no output. We
        don't enforce these kinds of rules (or things like StructureMode=Block and Empty=true,
        which makes no sense in our world), because if you are developing a custom renderer,
        you should understand this (or shortly will). 

        Here's an example ("empty-inline" means empty and "inline" means non-empty):

        This sequence:

            new RendererStack()

            Push(block1)        //begin block1
            Push(block2)        //nest under block1, begin block2
            Pop()               //end block2
            Push(line)          //nest under block1, begin line
            Push(inline1)       //end line, nest under block1, start inline1
            Push(inline2)       //nest inside inline1, begin inline2
            Pop()               //end inline2
            Pop()               //end inline1
            Push(inline3)       //nest under block1, begin inline3
            Push(empty-inline)  //nest under inline3, begin empty-inline
            Push(inline4)       //end empty-inline, nest under block1, begin inline4
            Push(inline5)       //nest inside inline4, begin inline5
            Push(block3)        //end all inlines up to last block/line, begin block3

            RenderStack.End()   //end block3, end block1 (pop pop)

        Would render:

   		    <block1>
                <block2>
                </block2>
			    <line />
			    <inline1>foo<inline2>bar</inline2></inline1>
			    <inline3><empty-inline /></inline3>
                <inline4><inline5></inline5></inline4>
                <block3>
                </block3>
		    </block1>


        We implement these rules in the Push() method. We consider the following cases:

            Anything under a Block:
                Push(block)
                Push(inline | empty-inline | line | block) -> don't do anything (nest under block)

            Anything under a Line:
                Push(line)
                Push(inline | empty-inline | line | block) -> always pop the line

            Non-Inline under an Inline (shown with extra stuff on stack):
                Push(block | line)
                Push(inline)
                Push(inline | empty-inline)
                Push(line | block)  -> pop all the inlines up to but not including the last non-inline

            Inline Under Inline

                Push(inline)
                Push(inline | empty-inline)  -> don't do anything (nest under inline)

                or

                Push(empty-inline)
                Push(inline | empty-inline)  -> close the empty-inline

        Keep in mind, Push() calls Begin(), and Pop() calls End().

        Again, we simply ignore the Empty property for Lines and Blocks.

        Also, keep in mind, things higher on the stack are "under" things lower on the stack,
        when rendered. The top of the stack represents the last thing that had Begin()
        called, and is the renderer currently nested the deepest.      

        We keep the Stack, Push() and Pop() internal and provide Open() and Close() methods
        for implementors of custom RendererStacks.

    */

    public abstract class RendererStack : Renderer
    {
        internal Stack<Renderer> Stack { private set; get; } = new Stack<Renderer>();

        public RendererStack(Context context, FormatModes formatMode, StructureModes structureMode) : base(context, formatMode, structureMode)
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

                    if (top.StructureMode == StructureModes.Inline)
                    {
                        //anything under an inline

                        if (renderer.StructureMode == StructureModes.Line || renderer.StructureMode == StructureModes.Block)
                        {
                            //line or block under an inline
                            
                            //close all inlines up to but not including last line or block
                            CloseInline();
                        }
                        else if(renderer.StructureMode == StructureModes.Inline)
                        {
                            //inline under inline

                            if (top.Empty)
                            {
                                //inline under an empty inline
                                Close();
                            }
                        }
                    }
                    else if (top.StructureMode == StructureModes.Line)
                    {
                        //anything under a line
                        Close();
                    }
                    else if (top.StructureMode == StructureModes.Block)
                    {
                        //do nothing, nest
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

                if (top.StructureMode == StructureModes.Inline)
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

                if (top.StructureMode != StructureModes.Inline)
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