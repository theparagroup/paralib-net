using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;

namespace com.paraquery.Bootstrap.Grids
{
    public interface IGrid : IDisposable
    {
        IGrid SetClasses(IList<string> classes = null);

        IContainer Container(bool fluid = false);
        IContainer Container(object attributes, bool fluid = false);
        IContainer Container(Action<GlobalAttributes> attributes, object additional = null, bool fluid = false);

        IRow Row(object attributes = null);
        IRow Row(Action<GlobalAttributes> attributes, object additional = null);

        IGrid EndGrid();
    }
}
