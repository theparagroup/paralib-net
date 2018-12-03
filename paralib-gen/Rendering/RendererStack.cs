using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Rendering
{
    public abstract class RendererStack:RendererStackBase
    {
        public RendererStack(LineModes lineMode) : base(lineMode)
        {
        }

        public virtual IRenderer Top
        {
            get
            {
                return Peek();
            }
        }

        public abstract IRenderer Open(IRenderer renderer);

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

        //remove these

        public new int Count
        {
            get
            {
                return base.Count;
            }
        }

        public virtual void Mark(Marker marker)
        {
            Open(marker);
        }

        public virtual void Close(string marker, Action<IRenderer> action = null)
        {
            while (Count > 0)
            {
                IRenderer top = Top;

                if (top is Marker)
                {
                    if (((Marker)top).Name == marker)
                    {
                        Pop();
                        return;
                    }
                }
                else
                {
                    if (action != null)
                    {
                        action(top);
                    }

                    Pop();
                }
            }

            throw new InvalidOperationException($"Marker {marker} doesn't exist");
        }

      
    }

}
