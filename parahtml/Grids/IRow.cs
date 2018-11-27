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
    public interface IRow<C> where C:HtmlContext
    {
        IRow<C> Here(Action<IRow<C>> row);
        IRow<C> Html(Action<FluentHtml<C>> html);

        IRow<C> Row(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IRow<C> Row(string @class, string[] columnClasses = null);
        IRow<C> Row(string[] columnClasses = null);

        IColumn<C> Column(Action<GlobalAttributes> attributes = null);
        IColumn<C> Column(string @class);
    }
}
