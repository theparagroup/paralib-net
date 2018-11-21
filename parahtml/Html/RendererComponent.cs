using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.parahtml.Packages;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Html
{
    /*
        TagType is an HtmlComponent that is also an IRenderer and has its own stack.

        TagTypes are Inline or Block, we don't support empty (None) elements as it
        doesn't make any sense (just create a custom HtmlRenderer instead).

        The component itself isn't visible but can be pushed and popped according
        to the usual rules.

    */
    public abstract class RendererComponent<C,F,P> : HtmlComponentBase<C,F, P>, IHasRendererStack where F : RendererComponent<C,F, P> where P : Package, new() where C:HtmlContext
    {
        private Renderer _renderer;
        protected LineModes _lineMode { private set; get; }
        protected ContainerModes _containerMode { private set; get; }
        public object Data { set; get; }

        public RendererComponent(C context, LineModes lineMode, ContainerModes containerMode, bool indentContent) : base(context, new RendererStack(lineMode==LineModes.None))
        {
            _renderer = new Renderer(context, lineMode, containerMode, indentContent);
            _lineMode = lineMode;
            _containerMode = containerMode;
        }

        LineModes IRenderer.LineMode
        {
            get
            {
                return _lineMode;
            }
        }

        ContainerModes IRenderer.ContainerMode
        {
            get
            {
                return _containerMode;
            }
        }

        void IRenderer.Begin()
        {
            DoBegin();
        }

        protected virtual void DoBegin()
        {
            _renderer.OnPreBegin();
            OnBegin();
            _renderer.OnPostBegin();

            OnBeginContent();
        }

        void IRenderer.End()
        {
            DoEnd();
        }

        protected virtual void DoEnd()
        {
            OnEndContent();

            _renderer.OnPreEnd();
            OnEnd();
            _renderer.OnPostEnd();
        }

        protected abstract void OnBegin();
        protected abstract void OnBeginContent();
        protected abstract void OnEndContent();
        protected abstract void OnEnd();



        RendererStack IHasRendererStack.RendererStack
        {
            get
            {
                return RendererStack;
            }
        }

    }
}
