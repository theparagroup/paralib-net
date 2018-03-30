using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace com.paralib.Mvc.Infrastructure.ParaQuery
{
    public class Context: paraquery.Engines.Base.Context
    {
        protected WebViewPage _view { get; private set; }

        public Context(WebViewPage view, string @namespace=null, Dictionary<string, string> namespaceVars=null) : base(@namespace, namespaceVars)
        {
            _view = view;

            Server = new Server(this, view);
            Request = new Request(this, view);
            Response = new Response(this, view);

        }

    }
}
