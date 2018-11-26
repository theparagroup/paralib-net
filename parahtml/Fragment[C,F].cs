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
    public class Fragment<C, F> : FluentHtmlBase<C, F>, IDisposable where C : HtmlContext where F : Fragment<C, F>
    {
        public Fragment(C context) : base(context, new RendererStack(false))
        {
        }

        public FluentCss Css()
        {
            var cssContext = new Css.CssContext(Context);
            var css = new FluentCss(cssContext, RendererStack);
            return css;
        }

        public IGrid<C> Grid(Action<GridOptions> options = null)
        {
            return new FluentGrid<C>(Context, RendererStack, options);
        }

        public void Dispose()
        {
            CloseAll();
        }

    }
}
