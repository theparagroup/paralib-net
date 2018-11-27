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
            var css = new FluentCss(Context, RendererStack);
            return css;
        }

        private void _Grid(Action<GridOptions> gridOptions, Action<IGrid<C>> grid)
        {
            if (grid != null)
            {
                var marker = Guid.NewGuid().ToString();
                Mark(marker);
                var fluentGrid= new FluentGrid<C>(Context, RendererStack, gridOptions);
                grid(fluentGrid);
                Close(marker);
            }

        }

        public void Grid(Action<GridOptions> gridOptions, Action<IGrid<C>> grid)
        {
            _Grid(gridOptions, grid);
        }

        public void Grid(Action<IGrid<C>> grid)
        {
            _Grid(null, grid);
        }

        public void Dispose()
        {
            CloseAll();
        }

    }
}
