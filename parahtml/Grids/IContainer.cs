using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.parahtml.Html;

namespace com.parahtml.Grids
{
    public interface IContainer
    {
        IContainer Here(Action<IContainer> container);
        IContainer Html(Action<FluentHtml> html);

        IRow Row(Action<GlobalAttributes> attributes, string[] columnClassList = null);
        IRow Row(string[] columnClassList = null);

        IGrid CloseGrid();
    }
}
