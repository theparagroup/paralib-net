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

        IRow<C> Row(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IRow<C> Row(string @class, string[] columnClasses = null);
        IRow<C> Row(string[] columnClasses = null);

        IContainer<C> Container(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IContainer<C> Container(string @class, string[] columnClasses = null);
        IContainer<C> Container(string[] columnClasses = null);

        IColumn<C> Grid(Action<GridOptions> gridOptions, Action<IGrid<C>> grid);
        IColumn<C> Grid(Action<IGrid<C>> grid);

    }
}
