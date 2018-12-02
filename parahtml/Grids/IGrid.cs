using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.parahtml.Html;
using com.parahtml.Core;

namespace com.parahtml.Grids
{

    public interface IGrid
    {
        IGrid Here(Action<IGrid> grid);
        IGrid Html(Action<FluentHtml> html);

        IContainer Container(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IContainer Container(string @class, string[] columnClasses = null);
        IContainer Container(string[] columnClasses = null);

        IRow Row(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IRow Row(string @class, string[] columnClasses = null);
        IRow Row(string[] columnClasses = null);
    }
}
