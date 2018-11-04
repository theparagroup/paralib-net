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
        void Begin();

        IContainer Container(Action<GlobalAttributes> attributes, IList<string> columnClasses = null);
        IContainer Container(IList<string> columnClasses = null);

        IRow Row(Action<GlobalAttributes> attributes, IList<string> columnClasses = null);
        IRow Row(IList<string> columnClasses = null);


        IGrid EndGrid();
    }
}
