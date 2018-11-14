using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Fluent
{

    public abstract class FluentRendererStack<C, F>: FluentWriter<C,F>, IFluentRendererStack<F> where C : Context where F : FluentRendererStack<C, F>
    {
        public FluentRendererStack(C context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }

        public virtual IRenderer Top
        {
            get
            {
                return _rendererStack.Top;
            }
        }

        public virtual F Open(IRenderer renderer)
        {
            _rendererStack.Open(renderer);
            return (F)this;
        }

        public virtual F CloseUp()
        {
            _rendererStack.CloseUp();
            return (F)this;
        }

        public virtual F CloseBlock()
        {
            _rendererStack.CloseBlock();
            return (F)this;
        }

        public virtual F CloseAll()
        {
            _rendererStack.CloseAll();
            return (F)this;
        }

        public virtual F Close()
        {
            _rendererStack.Close();
            return (F)this;
        }

        public virtual F Close(IRenderer renderer)
        {
            _rendererStack.Close(renderer);
            return (F)this;
        }

        public virtual F Open<R>(R renderer, Action<R> action) where R : IRenderer
        {
            Open(renderer);
            action(renderer);
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
