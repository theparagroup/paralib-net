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
    public abstract class RendererComponent<F,P> : HtmlComponentBase<F, P>, IHasRendererStack where F : RendererComponent<F, P> where P : Package, new()
    {
        protected LineModes _lineMode { private set; get; }
        protected ContainerModes _containerMode { private set; get; }

        public RendererComponent(HtmlContext context, LineModes lineMode, ContainerModes containerMode) : base(context, new RendererStack(lineMode==LineModes.None))
        {
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
            OnBegin();
        }

        protected abstract void OnBegin();

        void IRenderer.End()
        {
            CloseAll();
        }

        RendererStack IHasRendererStack.RendererStack
        {
            get
            {
                return _rendererStack;
            }
        }

    }
}
