using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace com.paralib.Mvc.Infrastructure.ParaQuery
{
    public class Context: paraquery.Html.HtmlContext
    {
        protected ViewContext _viewContext { get; private set; }
        protected UrlHelper _url { get; set; }

        public Context(ViewContext viewContext) : base(new Writer(viewContext.Writer))
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
