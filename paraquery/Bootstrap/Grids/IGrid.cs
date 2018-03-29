using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Bootstrap.Grids
{
    public interface IGrid:IDisposable
    {
        IGrid SetClasses(IList<string> classes = null);

        IContainer Container(object attributes = null, bool fluid = false);
        IRow Row(object attributes = null);
    }
}
