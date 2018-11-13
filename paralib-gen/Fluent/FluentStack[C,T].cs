using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Fluent
{

    public abstract class FluentStack<C, T> : FluentWriter<C,T>, IFluentStack<T> where C : Context where T : FluentStack<C, T>
    {
        public FluentStack(C context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }

        public virtual T Open(Renderer renderer)
        {
            _rendererStack.Open(renderer);
            return (T)this;
        }

        public virtual T CloseUp()
        {
            _rendererStack.CloseUp();
            return (T)this;
        }

        public virtual T CloseBlock()
        {
            _rendererStack.CloseBlock();
            return (T)this;
        }

        public virtual T CloseAll()
        {
            _rendererStack.CloseAll();
            return (T)this;
        }

        public virtual T Close()
        {
            _rendererStack.Close();
            return (T)this;
        }

        public virtual T Close(Renderer renderer)
        {
            _rendererStack.Close(renderer);
            return (T)this;
        }

        public virtual Renderer Top
        {
            get
            {
                return _rendererStack.Top;
            }
        }

        public virtual T Open<R>(R renderer, Action<R> action) where R : Renderer
        {
            Open(renderer);
            action(renderer);
            return (T)this;
        }

        public virtual T Here(Action<T> action)
        {
            if (action != null)
            {
                action((T)this);
            }

            return (T)this;
        }

      

    }


}
