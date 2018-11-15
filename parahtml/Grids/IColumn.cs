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
        IColumn Html(Action<FluentHtml> html);

        IRow Row(Action<GlobalAttributes> attributes, IList<string> columnClasses = null);
        IRow Row(IList<string> columnClasses = null);

        IColumn Column(Action<GlobalAttributes> attributes = null);
        IColumn Column(string @class);

        IGrid Grid(Action<GridOptions> options=null);
        IGrid Grid(IList<string> columnClasses = null);
        IGrid EndGrid();
    }
}
