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
using com.paralib.Gen;

namespace com.parahtml
{
    public class Fragment<F> : FluentHtmlBase<F>, IHasContext, IDisposable where F : Fragment<F>
    {
        public Fragment() : base(new HtmlRendererStack(LineModes.Multiple))
        {
        }

        protected override void OnContext()
        {
            ((IHasContext)RendererStack).SetContext(Context);
        }

        public FluentCss Css()
        {
            var css = new FluentCss(Context, RendererStack);
            return css;
        }

        private void _Grid(Action<GridOptions> gridOptions, Action<IGrid> grid)
        {
            if (grid != null)
            {
                var marker = Guid.NewGuid().ToString();
                Mark(marker);
                var fluentGrid= new FluentGrid(Context, RendererStack, gridOptions);
                grid(fluentGrid);
                Close(marker);
            }

        }

        public void Grid(Action<GridOptions> gridOptions, Action<IGrid> grid)
        {
            _Grid(gridOptions, grid);
        }

        public void Grid(Action<IGrid> grid)
        {
            _Grid(null, grid);
        }

        public void Dispose()
        {
            CloseAll();
        }

    }
}
