using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery;

namespace com.paraquery
{
    public static class IWriterExtensions
    {
        /* 
            
            these are useful for razor
          
            pQuery.Write(@<span></span>)
        
            pQuery.Write(Html.TextBox("emp_id",99, new { @class = "ttb-input-name" }));     

        */

        public static void Write(this IWriter writer, Func<dynamic, System.Web.WebPages.HelperResult> content)
        {
            writer.Write(content(null).ToHtmlString());
        }

        public static void WriteLine(this IWriter writer, Func<dynamic, System.Web.WebPages.HelperResult> content)
        {
            writer.WriteLine(content(null).ToHtmlString());
        }

        public static void Write(this IWriter writer, System.Web.Mvc.MvcHtmlString content)
        {
            writer.Write(content?.ToHtmlString());
        }

        public static void WriteLine(this IWriter writer, System.Web.Mvc.MvcHtmlString content)
        {
            writer.WriteLine(content?.ToHtmlString());
        }

    }

}
