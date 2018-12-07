using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.parahtml.Packages;
using com.paralib.Gen;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Html
{
    /*
        TagType is an HtmlComponent that is also an IRenderer and has its own stack.

        ContainerType should be Inline|Block, as a Component with no content makes no sense.
        You can do it but you should just create a custom HtmlRenderer instead.

        The base component itself isn't visible but can be pushed and popped according
        to the usual rules.

    */
    public abstract class HtmlRendererComponent<F,P> : HtmlComponentBase<F, P>  where F : HtmlRendererComponent<F, P> where P : Package, new()
    {
        private Renderer _renderer;
        protected LineModes _lineMode { private set; get; }
        protected ContainerModes _containerMode { private set; get; }
        public object Data { set; get; }

        public HtmlRendererComponent(LineModes lineMode, ContainerModes containerMode, bool indentContent) : base(new RendererStack(lineMode))
        {
            _renderer = new Renderer(lineMode, containerMode, indentContent);

            _renderer.Opening += _renderer_Opening;
            _renderer.Closing += _renderer_Closing;

            _lineMode = lineMode;
            _containerMode = containerMode;

        }

        public void Initialize(Context context)
        {
            if (context is HtmlContext)
            {
                ((IFluentHtmlBase)this).SetContext((HtmlContext)context);
                ((IHtmlRendererStack)RendererStack).SetContext((HtmlContext)context);
            }
            else
            {
                throw new InvalidOperationException("HtmlRendererComponent requires an HtmlContext");
            }
        }
        private void _renderer_Opening(object sender, EventArgs e)
        {
            OnBeginContent();
        }

        protected abstract void OnBeginContent();


        private void _renderer_Closing(object sender, EventArgs e)
        {
            OnEndContent();
        }

        protected abstract void OnEndContent();

    }
}
