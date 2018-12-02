using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.parahtml.Core;
using com.parahtml.Html;

namespace com.parahtml.Grids
{
    public interface IContainer
    {
        IContainer Here(Action<IContainer> container);
        IContainer Html(Action<FluentHtml> html);

        IRow Row(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IRow Row(string @class, string[] columnClasses = null);
        IRow Row(string[] columnClasses = null);
    }
}
