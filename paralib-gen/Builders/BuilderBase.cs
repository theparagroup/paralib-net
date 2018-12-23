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
        private ContentStack _contentStack;

        public BuilderBase()
        {
            _contentStack = new ContentStack();
        }

        void ILazyContext.Initialize(Context context)
        {
            _context = context;
            _contentStack.Initialize(context);
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
            if (_contentStack.Top?.LineMode != LineModes.Multiple)
            {
                _contentStack.CloseUp();
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

        public virtual IContent Top
        {
            get
            {
                return _contentStack.Top;
            }
        }

        public virtual ICloseable Open(IContent content)
        {
            _contentStack.Open(content);
            return content;
        }

        public virtual void Close()
        {
            _contentStack.Close();
        }

        public virtual void Close(ICloseable closable)
        {
            if (closable is IContent)
            {
                _contentStack.Close((IContent)closable);
            }
            else
            {
                throw new InvalidOperationException("Can only close IContent");
            }
        }

        public virtual void CloseAll()
        {
            _contentStack.CloseAll();
        }

        public void With(IContent content, Action action)
        {
            var contentState = content.ContentState;

            if (contentState == ContentStates.New)
            {
                Open(content);
            }
            else if (contentState == ContentStates.Open)
            {
                //already open, do nothing
            }
            else if (contentState == ContentStates.Closed)
            {
                throw new InvalidOperationException("Can't call With() on a closed renderer");
            }

            if (action!=null)
            {
                action();
            }

            Close(content);
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

        ContentStack IContainer.ContentStack
        {
            get
            {
                return _contentStack;
            }
        }
    }



    


}
