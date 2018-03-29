using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Bootstrap.Grids
{
    public interface IRow
    {
        IRow SetClasses(IList<string> classes = null);

        IRow Row(object attributes = null);
        IColumn Column(object attributes = null);
    }
}
