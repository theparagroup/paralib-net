using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Rendering
{
    public partial class RendererStack
    {

        public virtual IRenderer Top
        {
            get
            {
                return Peek();
            }
        }

        public virtual void Open(IRenderer renderer)
        {
            if (renderer.RenderState == RenderStates.New)
            {
                Push(renderer);
            }
            else
            {
                throw new InvalidOperationException("Renderer is already open");
            }
        }

        public virtual void Close()
        {
            Pop();
        }

        public virtual void CloseUp()
        {
            PopToBlock(false);
        }

        public virtual void CloseBlock()
        {
            PopToBlock(true);
        }

        public virtual void CloseAll()
        {
            PopAll();
        }

        public virtual void Close(IRenderer renderer)
        {
            PopRenderer(renderer);
        }

        public virtual void Close(Func<IRenderer, bool> func)
        {
            Pop(func);
        }
      
    }

}
