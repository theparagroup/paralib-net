using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery;
using com.paraquery.Html.Fluent;

namespace com.paraquery
{
    public static class FluentHtmlExtensions
    {
        /* 
            
            these are useful for razor
          
            pQuery.Write(@<span></span>)
        
            pQuery.Write(Html.TextBox("emp_id",99, new { @class = "ttb-input-name" }));     

        */

        public static FluentHtml Write(this FluentHtml fluentHtml, Func<dynamic, System.Web.WebPages.HelperResult> content, bool? indent = null)
        {
            fluentHtml.Write(content(null).ToHtmlString(), indent);
            return fluentHtml;
        }

        public static FluentHtml WriteLine(this FluentHtml fluentHtml, Func<dynamic, System.Web.WebPages.HelperResult> content, bool? indent = null)
        {
            fluentHtml.WriteLine(content(null).ToHtmlString(), indent);
            return fluentHtml;
        }

        public static FluentHtml Write(this FluentHtml fluentHtml, System.Web.Mvc.MvcHtmlString content, bool? indent = null)
        {
            fluentHtml.Write(content?.ToHtmlString(), indent);
            return fluentHtml;
        }

        public static FluentHtml WriteLine(this FluentHtml fluentHtml, System.Web.Mvc.MvcHtmlString content, bool? indent = null)
        {
            fluentHtml.WriteLine(content?.ToHtmlString(), indent);
            return fluentHtml;
        }

    }

}
