using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.parahtml.Tags.Fluent;

namespace com.paraquery.Bootstrap.Grids
{
    public interface IColumn
    {

        IRow Row(Action<GlobalAttributes> attributes, IList<string> columnClasses = null);
        IRow Row(IList<string> columnClasses = null);

        IColumn Column(Action<GlobalAttributes> attributes = null);
        IColumn Column(string @class);

        IGrid Grid(Action<GridOptions> options=null);
        IGrid EndGrid();

        IColumn Html(Action<Html> html, bool inline=false);

    }
}
