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
    public class Fragment<F> : FluentHtmlBase<F>, IDisposable where F : Fragment<F>
    {
        public Fragment() : base(new ContentStack())
        {
        }

        public FluentCss Css()
        {
            var css = new FluentCss(Context, ContentStack);
            return css;
        }

        //private void _Grid(Action<GridOptions> gridOptions, Action<IGrid> grid)
        //{
        //    if (grid != null)
        //    {
        //        var marker = Guid.NewGuid().ToString();
        //        Mark(marker);
        //        var fluentGrid= new FluentGrid(Context, RendererStack, gridOptions);
        //        grid(fluentGrid);
        //        Close(marker);
        //    }

        //}

        //public void Grid(Action<GridOptions> gridOptions, Action<IGrid> grid)
        //{
        //    _Grid(gridOptions, grid);
        //}

        //public void Grid(Action<IGrid> grid)
        //{
        //    _Grid(null, grid);
        //}

        public void Dispose()
        {
            CloseAll();
        }

    }
}
