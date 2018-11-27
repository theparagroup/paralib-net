using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Fluent
{

    public abstract class FluentRendererStack<C, F> : FluentWriter<C, F> where C : Context where F : FluentRendererStack<C, F>
    {
        

        public FluentRendererStack(C context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }

       

        public virtual IRenderer Top
        {
            get
            {
                return RendererStack.Top;
            }
        }

        public virtual F Open(IRenderer renderer)
        {
            RendererStack.Open(renderer);
            return (F)this;
        }

        public virtual F CloseUp()
        {
            RendererStack.CloseUp();
            return (F)this;
        }

        public virtual F CloseBlock()
        {
            RendererStack.CloseBlock();
            return (F)this;
        }

        public virtual F CloseAll()
        {
            RendererStack.CloseAll();
            return (F)this;
        }

        public virtual F Close()
        {
            RendererStack.Close();
            return (F)this;
        }

        public virtual F Close(IRenderer renderer)
        {
            RendererStack.Close(renderer);
            return (F)this;
        }

        public virtual F Mark(string marker)
        {
            RendererStack.Mark(new Marker(marker));
            return (F)this;
        }

        public virtual F Close(string marker, Action<IRenderer> action = null)
        {
            RendererStack.Close(marker, action);
            return (F)this;
        }

        public virtual F Close(Func<IRenderer, bool> func)
        {
            RendererStack.Close(func);
            return (F)this;
        }

        public virtual F Open<R>(R renderer, Action<R> action) where R : IRenderer
        {
            Open(renderer);

            if (action!=null)
            {
                action(renderer);
            }

            Close(renderer);
            return (F)this;
        }

        public virtual F Here(Action<F> action)
        {
            if (action != null)
            {
                action((F)this);
            }

            return (F)this;
        }

      

    }


}
