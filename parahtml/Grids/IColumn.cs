using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.parahtml.Html;

namespace com.parahtml.Grids
{
    public interface IColumn
    {
        IColumn Here(Action<IColumn> column);
        IColumn Html(Action<FluentHtml> html);

        IColumn Column(Action<GlobalAttributes> attributes = null);
        IColumn Column(string @class);

        IRow Row(Action<GlobalAttributes> attributes, string[] columnClassList = null);
        IRow Row(string[] columnClassLists = null);

        IContainer Container(Action<GlobalAttributes> attributes, string[] columnClassList = null);
        IContainer Container(string[] columnClassList = null);

        IGrid Grid(Action<GridOptions> options=null);
        IGrid CloseGrid();
    }
}
