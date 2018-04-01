using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery;

namespace com.paraquery
{
    public static class IResponseExtensions
    {
        /* 
            
            these are useful for razor
          
            pQuery.Write(@<span></span>)
        
            pQuery.Write(Html.TextBox("emp_id",99, new { @class = "ttb-input-name" }));     

        */

        public static void Write(this IResponse response, Func<dynamic, System.Web.WebPages.HelperResult> content, bool indent = true)
        {
            response.Write(content(null).ToHtmlString(), indent);
        }

        public static void WriteLine(this IResponse response, Func<dynamic, System.Web.WebPages.HelperResult> content, bool indent = true)
        {
            response.WriteLine(content(null).ToHtmlString(), indent);
        }

        public static void Write(this IResponse response, System.Web.Mvc.MvcHtmlString content, bool indent = true)
        {
            response.Write(content.ToHtmlString(), indent);
        }

        public static void WriteLine(this IResponse response, System.Web.Mvc.MvcHtmlString content, bool indent = true)
        {
            response.WriteLine(content.ToHtmlString(), indent);
        }

    }

}
