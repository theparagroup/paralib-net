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

        public static IColumn Write(this IColumn column, Func<dynamic, System.Web.WebPages.HelperResult> content, bool? indent = null)
        {
            column.Write(content(null).ToHtmlString(), indent);
            return column;
        }

        public static IColumn WriteLine(this IColumn column, Func<dynamic, System.Web.WebPages.HelperResult> content, bool? indent = null)
        {
            column.WriteLine(content(null).ToHtmlString(), indent);
            return column;
        }

        public static IColumn Write(this IColumn column, System.Web.Mvc.MvcHtmlString content, bool? indent = null)
        {
            column.Write(content?.ToHtmlString(), indent);
            return column;
        }

        public static IColumn WriteLine(this IColumn column, System.Web.Mvc.MvcHtmlString content, bool? indent = null)
        {
            column.WriteLine(content?.ToHtmlString(), indent);
            return column;
        }

    }

}
