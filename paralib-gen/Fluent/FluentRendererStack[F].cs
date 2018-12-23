using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Fluent
{

    public abstract class FluentRendererStack<F> : FluentWriter<F> where F : FluentRendererStack<F>
    {

        public FluentRendererStack(ContentStack contentStack) : base(contentStack)
        {
        }

        public virtual IContent Top
        {
            get
            {
                return ContentStack.Top;
            }
        }

        public virtual F Open(Renderer renderer)
        {
            ContentStack.Open(renderer);
            return (F)this;
        }

        public virtual F CloseUp()
        {
            ContentStack.CloseUp();
            return (F)this;
        }

        public virtual F CloseBlock()
        {
            ContentStack.CloseBlock();
            return (F)this;
        }

        public virtual F CloseAll()
        {
            ContentStack.CloseAll();
            return (F)this;
        }

        public virtual F Close()
        {
            ContentStack.Close();
            return (F)this;
        }

        public virtual F Close(IContent renderer)
        {
            ContentStack.Close(renderer);
            return (F)this;
        }

        public virtual F Close(Func<IContent, bool> func)
        {
            ContentStack.Close(func);
            return (F)this;
        }

        public virtual F Open<R>(R renderer, Action<R> action) where R : Renderer
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
