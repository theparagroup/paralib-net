using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.parahtml.Html;
using com.parahtml.Css;
using com.parahtml.Grids;

namespace com.parahtml
{
    public class Fragment : FluentHtmlBase<Fragment>, IDisposable
    {
        public Fragment(HtmlContext context) : base(context, new RendererStack(false))
        {
        }

        public FluentCss Css()
        {
            var cssContext = new Css.CssContext(Context);
            var css = new FluentCss(cssContext, _rendererStack);
            return css;
        }

        public FluentDocument Document()
        {
            return new FluentDocument(Context, _rendererStack);
        }

        public FluentHtml Html()
        {
            return new FluentHtml(Context, _rendererStack);
        }

        public IGrid Grid(Action<GridOptions> options = null)
        {
            return new FluentGrid(Context, _rendererStack, options);
        }


        public void Dispose()
        {
            CloseAll();
        }

    }
}
