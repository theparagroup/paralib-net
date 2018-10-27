using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;

namespace com.paraquery.Bootstrap.Grids
{
    public interface IRow
    {
        IRow SetClasses(IList<string> classes = null);

        IRow Row(object attributes = null);
        IRow Row(Action<GlobalAttributes> attributes, object additional = null);

        IColumn Column(object attributes = null);
        IColumn Column(Action<GlobalAttributes> attributes, object additional = null);

        IGrid EndGrid();

    }
}
