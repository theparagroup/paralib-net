using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using com.paraquery;

namespace com.paralib.Mvc.Infrastructure.ParaQuery
{
    public class Server : paraquery.Engines.Base.Server
    {
        protected ViewContext _viewContext;
        protected UrlHelper _url { get; set; }

        public Server(Context context, ViewContext viewContext) :base(context)
        {
            _viewContext = viewContext;
            _url = new UrlHelper(viewContext.RequestContext, System.Web.Routing.RouteTable.Routes);
        }

        public override string UrlPrefix(string url)
        {
            return _url.Content(url);
        }

    }
}
