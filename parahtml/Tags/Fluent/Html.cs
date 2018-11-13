using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Tags.Fluent
{
    /*

        FluentHtml must either be in Block or Inline mode because....

        Since adding HTML elements under an empty elment doesn't make any sense, we don't have
        that concept (like Tag does) and never go into the "Single" LineMode.

    */
    public partial class Html
    {
        protected RendererStack _rendererStack;
        protected HtmlContext Context { private set; get; }

        public Html(HtmlContext context, RendererStack stack)
        {
            _rendererStack = stack;
            Context = context;
        }

        protected HtmlBuilder HtmlBuilder
        {
            get
            {
                return Context.HtmlBuilder;
            }
        }

        public Html Open(Renderer renderer)
        {
            _rendererStack.Open(renderer);
            return this;
        }

        public Html CloseUp()
        {
            _rendererStack.CloseUp();
            return this;
        }

        public Html CloseBlock()
        {
            _rendererStack.CloseBlock();
            return this;
        }

        public Html CloseAll()
        {
            _rendererStack.CloseAll();
            return this;
        }

        public Html Close()
        {
            _rendererStack.Close();
            return this;
        }

        public Html Close(Renderer renderer)
        {
            _rendererStack.Close(renderer);
            return this;
        }

      

    }
}