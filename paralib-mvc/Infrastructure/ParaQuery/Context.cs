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
        protected ViewContext _viewContext { get; private set; }

        public Context(ViewContext viewContext, string @namespace=null, Dictionary<string, string> namespaceVars=null) : base(@namespace, namespaceVars)
        {
            _viewContext = viewContext;

            Server = new Server(this, viewContext);
            Request = new Request(this, viewContext);
            Writer = new Writer(this, viewContext.Writer);

        }


    }
}
