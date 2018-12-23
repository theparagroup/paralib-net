using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Builders
{
    
    public abstract class BuilderBase : ILazyContext, IContainer
    {
        private Context _context;
        private RendererStack _rendererStack;

        public BuilderBase(LineModes lineMode)
        {
            _rendererStack = new RendererStack(lineMode);
        }

        void ILazyContext.Initialize(Context context)
        {
            _context = context;
            _rendererStack.Initialize(context);
        }

        public Context Context
        {
            get
            {
                if (_context != null)
                {
                    return _context;
                }
                else
                {
                    throw new InvalidOperationException("Builder Context cannot be null");
                }

            }
        }

        private void OnBeforeNewLine()
        {
            if (_rendererStack.Top?.LineMode != LineModes.Multiple)
            {
                _rendererStack.CloseUp();
            }
        }

        public void Write(string content)
        {
            Context.Writer.Write(content);
        }

        public void WriteLine(string content)
        {
            OnBeforeNewLine();
            Context.Writer.WriteLine(content);
        }

        public void NewLine()
        {
            OnBeforeNewLine();
            Context.Writer.NewLine();
        }

        public void Space()
        {
            OnBeforeNewLine();
            Context.Writer.Space();
        }

        public virtual IRenderer Top
        {
            get
            {
                return _rendererStack.Top;
            }
        }

        public virtual IRenderer Open(IRenderer renderer)
        {
            _rendererStack.Open(renderer);
            return renderer;
        }

        public virtual void Close()
        {
            _rendererStack.Close();
        }

        public virtual void Close(IRenderer renderer)
        {
            _rendererStack.Close(renderer);
        }

        public virtual void CloseAll()
        {
            _rendererStack.CloseAll();
        }

        public void With(IRenderer renderer, Action action)
        {
            var renderState = renderer.RenderState;

            if (renderState == RenderStates.New)
            {
                Open(renderer);
            }
            else if (renderState == RenderStates.Open)
            {
                //already open, do nothing
            }
            else if (renderState==RenderStates.Closed)
            {
                throw new InvalidOperationException("Can't call With() on a closed renderer");
            }

            if (action!=null)
            {
                action();
            }

            Close(renderer);
        }

        public void With<T>(T component, Action<T> action) where T:IComponent 
        {
            component.Open(this);

            if (action!=null)
            {
                action(component);
            }

            component.Close();
        }

        public void With<T,F>(T component, Action<F> action) where T : IComponent, F where F: class
        {
            component.Open(this);

            if (action != null)
            {
                action(component);
            }

            component.Close();
        }

        //public void With2<F,T>(T component, Action<F> action) where T : IComponent, F where F : class
        //{
        //    component.Open(this);

        //    if (action != null)
        //    {
        //        action(component);
        //    }

        //    component.Close();
        //}

        //public void With<F>(IComponent component, Action<F> action) where F : IComponent
        //{
        //    component.Open(this);

        //    if (action != null)
        //    {
        //        if (component is F)
        //        {
        //            action((F)component);
        //        }
        //        else
        //        {
        //            throw new InvalidCastException($"Component {component.GetType().Name} is not {typeof(F).Name}");
        //        }

        //    }

        //    component.Close();
        //}


        Context IContainer.Context
        {
            get
            {
                return Context;
            }
        }

        RendererStack IContainer.RendererStack
        {
            get
            {
                return _rendererStack;
            }
        }
    }



    


}
