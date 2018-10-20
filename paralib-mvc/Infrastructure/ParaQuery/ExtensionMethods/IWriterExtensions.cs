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

        public static void Write(this IWriter writer, Func<dynamic, System.Web.WebPages.HelperResult> content, bool indent = true)
        {
            writer.Write(content(null).ToHtmlString(), indent);
        }

        public static void WriteLine(this IWriter writer, Func<dynamic, System.Web.WebPages.HelperResult> content, bool indent = true)
        {
            writer.WriteLine(content(null).ToHtmlString(), indent);
        }

        public static void Write(this IWriter writer, System.Web.Mvc.MvcHtmlString content, bool indent = true)
        {
            writer.Write(content?.ToHtmlString(), indent);
        }

        public static void WriteLine(this IWriter writer, System.Web.Mvc.MvcHtmlString content, bool indent = true)
        {
            writer.WriteLine(content?.ToHtmlString(), indent);
        }

    }

}
