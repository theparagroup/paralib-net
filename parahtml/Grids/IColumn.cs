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

    public interface IColumn
    {
        IColumn Here(Action<IColumn> column);
        IColumn Html(Action<FluentHtml> html);

        IColumn Column(Action<GlobalAttributes> attributes = null);
        IColumn Column(string @class);

        IRow Row(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IRow Row(string @class, string[] columnClasses = null);
        IRow Row(string[] columnClasses = null);

        IContainer Container(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IContainer Container(string @class, string[] columnClasses = null);
        IContainer Container(string[] columnClasses = null);

        IColumn Grid(Action<GridOptions> gridOptions, Action<IGrid> grid);
        IColumn Grid(Action<IGrid> grid);

    }
}
