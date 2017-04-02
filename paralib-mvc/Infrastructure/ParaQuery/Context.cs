using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace com.paralib.Mvc.Infrastructure.ParaQuery
{
    public class Context: com.paraquery.Core.Context
    {
        public Context(WebViewPage view, string @namespace=null, Dictionary<string, string> namespaceVars=null) : base(new Server(view), null, new Response(view), @namespace, namespaceVars)
        {

        }
    }
}
