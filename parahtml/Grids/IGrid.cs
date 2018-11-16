using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.parahtml.Html;

namespace com.parahtml.Grids
{
    public interface IGrid
    {
        IGrid Here(Action<IGrid> grid);
        IGrid Html(Action<FluentHtml> html);

        IContainer Container(Action<GlobalAttributes> attributes, IList<string> columnClasses = null);
        IContainer Container(IList<string> columnClasses = null);

        IRow Row(Action<GlobalAttributes> attributes, IList<string> columnClasses = null);
        IRow Row(IList<string> columnClasses = null);

        IGrid CloseGrid();
    }
}
