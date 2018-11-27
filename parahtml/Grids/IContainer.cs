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
    public interface IContainer<C> where C:HtmlContext
    {
        IContainer<C> Here(Action<IContainer<C>> container);
        IContainer<C> Html(Action<FluentHtml<C>> html);

        IRow<C> Row(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IRow<C> Row(string @class, string[] columnClasses = null);
        IRow<C> Row(string[] columnClasses = null);
    }
}
