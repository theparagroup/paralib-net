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

        IContainer Container(Action<GlobalAttributes> attributes = null, bool fluid = false);

        IRow Row(Action<GlobalAttributes> attributes = null);

        IGrid EndGrid();
    }
}
