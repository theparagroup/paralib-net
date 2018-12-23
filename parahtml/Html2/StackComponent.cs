using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen;
using com.paralib.Gen.Builders;
using com.paralib.Gen.Rendering;
using com.parahtml.Html;

namespace com.parahtml.Html2
{
    public abstract class StackComponent : IComponent, IContent, IHasContentStack
    {
        protected IContainer _container { private set; get; }
        protected LineModes _lineMode { private set; get; }
        protected ContainerModes _containerMode { private set; get; }
        protected ContentStack _contentStack { private set; get; }
        protected ContentStates _contentState { private set; get; } = ContentStates.New;

        public StackComponent(LineModes lineMode, ContainerModes containerMode)
        {
            _lineMode = lineMode;
            _containerMode = containerMode;
            _contentStack = new ContentStack();
        }

        void IComponent.Open(IContainer container)
        {
            _container = container;
            _contentStack.Initialize(container.Context);
            _container.ContentStack.Open(this);
        }

        void IComponent.Close()
        {
            _container.ContentStack.Close(this);
        }

        ContentStack IHasContentStack.ContentStack
        {
            get
            {
                return _contentStack;
            }
        }

        ContainerModes IContent.ContainerMode
        {
            get
            {
                return _containerMode;
            }
        }

        LineModes IContent.LineMode
        {
            get
            {
                return _lineMode;
            }
        }


        ContentStates IContent.ContentState
        {
            get
            {
                return _contentState;
            }
        }

        void IContent.Open(Context context)
        {
            OnRender();
            _contentState = ContentStates.Open;
        }

        protected abstract void OnRender();

        void IContent.Close()
        {
            _contentStack.CloseAll();
            _contentState = ContentStates.Closed;
        }

    }



}
