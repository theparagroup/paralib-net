using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Bootstrap.Grids;

namespace com.paraquery
{
    public static class FluentGridExtensions
    {

        public static IColumn Write(this IColumn column, Func<dynamic, System.Web.WebPages.HelperResult> content)
        {
            column.Write(content(null).ToHtmlString());
            return column;
        }

        public static IColumn WriteLine(this IColumn column, Func<dynamic, System.Web.WebPages.HelperResult> content)
        {
            column.WriteLine(content(null).ToHtmlString());
            return column;
        }

        public static IColumn Write(this IColumn column, System.Web.Mvc.MvcHtmlString content)
        {
            column.Write(content?.ToHtmlString());
            return column;
        }

        public static IColumn WriteLine(this IColumn column, System.Web.Mvc.MvcHtmlString content)
        {
            column.WriteLine(content?.ToHtmlString());
            return column;
        }

    }

}
