using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Mvc.Infrastructure.ParaQuery.ExtensionMethods
{
    public static class pQueryExtensions
    {
        /* 
            
            these are useful for razor
          
            pQuery.Write(@<span></span>)
        
            pQuery.Write(Html.TextBox("emp_id",99, new { @class = "ttb-input-name" }));     

        */

        public static void Write(this paraquery.pQuery pQuery, Func<dynamic, System.Web.WebPages.HelperResult> content)
        {
            pQuery.Write(content(null).ToHtmlString());
        }

        public static void WriteLine(this paraquery.pQuery pQuery, Func<dynamic, System.Web.WebPages.HelperResult> content)
        {
            pQuery.WriteLine(content(null).ToHtmlString());
        }

        public static void Write(this paraquery.pQuery pQuery, System.Web.Mvc.MvcHtmlString content)
        {
            pQuery.Write(content.ToHtmlString());
        }

        public static void WriteLine(this paraquery.pQuery pQuery, System.Web.Mvc.MvcHtmlString content)
        {
            pQuery.WriteLine(content.ToHtmlString());
        }

    }

}
