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

        IContainer Container(Action<GlobalAttributes> attributes, string[] columnClassList = null);
        IContainer Container(string[] columnClassList = null);

        IRow Row(Action<GlobalAttributes> attributes, string[] columnClassList = null);
        IRow Row(string[] columnClassList = null);

        IGrid CloseGrid();
    }
}
