using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.parahtml.Html;
using com.parahtml.Core;

namespace com.parahtml.Grids
{
    public interface IColumn<C> where C : HtmlContext
    {
        IColumn<C> Here(Action<IColumn<C>> column);
        IColumn<C> Html(Action<FluentHtml<C>> html);

        IColumn<C> Column(Action<GlobalAttributes> attributes = null);
        IColumn<C> Column(string @class);

        IRow<C> Row(Action<GlobalAttributes> attributes, string[] columnClassList = null);
        IRow<C> Row(string[] columnClassLists = null);

        IContainer<C> Container(Action<GlobalAttributes> attributes, string[] columnClassList = null);
        IContainer<C> Container(string[] columnClassList = null);

        IGrid<C> Grid(Action<GridOptions> options = null);
        IGrid<C> CloseGrid();
    }
}
