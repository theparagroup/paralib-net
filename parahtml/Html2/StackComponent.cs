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
    public abstract class StackComponent : IComponent, IRenderer, IHasRendererStack
    {
        protected IContainer _container { private set; get; }
        protected LineModes _lineMode { private set; get; }
        protected ContainerModes _containerMode { private set; get; }
        protected RendererStack _rendererStack { private set; get; }
        protected RenderStates _renderState { private set; get; } = RenderStates.New;

        public StackComponent(LineModes lineMode, ContainerModes containerMode)
        {
            _lineMode = lineMode;
            _containerMode = containerMode;
            _rendererStack = new RendererStack(_lineMode);
        }

        void IComponent.Begin(IContainer container)
        {
            _container = container;
            _rendererStack.Initialize(container.Context);
            _container.RendererStack.Open(this);
        }

        void IComponent.End()
        {
            _container.RendererStack.Close(this);
        }

        RendererStack IHasRendererStack.RendererStack
        {
            get
            {
                return _rendererStack;
            }
        }

        ContainerModes IRenderer.ContainerMode
        {
            get
            {
                return _containerMode;
            }
        }

        LineModes IRenderer.LineMode
        {
            get
            {
                return _lineMode;
            }
        }


        RenderStates IRenderer.RenderState
        {
            get
            {
                return _renderState;
            }
        }

        void IRenderer.Open(Context context)
        {
            OnRender();
            _renderState = RenderStates.Open;
        }

        protected abstract void OnRender();

        void IRenderer.Close()
        {
            _rendererStack.CloseAll();
            _renderState = RenderStates.Closed;
        }

    }



}
