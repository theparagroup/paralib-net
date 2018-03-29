using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Bootstrap.Grids
{
    public interface IColumn
    {
        IRow Row(object attributes = null);
        IColumn Column(object attributes = null);

        IGrid Grid();
        IGrid End();

        IColumn WriteLine(string content);
        IColumn Write(string content);

    }
}
