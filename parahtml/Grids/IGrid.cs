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
    public interface IGrid<C> where C:HtmlContext
    {
        IGrid<C> Here(Action<IGrid<C>> grid);
        IGrid<C> Html(Action<FluentHtml<C>> html);

        IContainer<C> Container(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IContainer<C> Container(string @class, string[] columnClasses = null);
        IContainer<C> Container(string[] columnClasses = null);

        IRow<C> Row(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IRow<C> Row(string @class, string[] columnClasses = null);
        IRow<C> Row(string[] columnClasses = null);

        IGrid<C> CloseGrid();
    }
}
