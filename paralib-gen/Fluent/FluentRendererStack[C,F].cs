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
        protected Stack<Marker> _markers;

        public FluentRendererStack(C context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }

        protected class Marker
        {
            public string Name { private set; get; }
            public IRenderer Renderer { private set; get; }

            public Marker(string name, IRenderer renderer)
            {
                Name = name;
                Renderer = renderer;
            }
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

        public virtual F Mark(string name)
        {
            if (_markers==null)
            {
                _markers = new Stack<Marker>();
            }

            _markers.Push(new Marker(name, Top));

            return (F)this;
        }

        public virtual F Close(string name)
        {
            while (_markers?.Count > 0)
            {
                var marker = _markers.Pop();

                if (marker.Name == name)
                {
                    return Close(marker.Renderer);
                }
            }

            throw new InvalidOperationException($"Marker {name} doesn't exist");
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
