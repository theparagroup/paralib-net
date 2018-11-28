using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Security.Principal;

namespace com.parahtml.Mvc
{
    /*

        https://daveaglick.com/posts/getting-an-htmlhelper-for-an-alternate-model-type

    */
    public class Helpers<M>
    {
        public UrlHelper Url { get; protected set; }
        public AjaxHelper<M> Ajax { get; private set; }
        public HtmlHelper<M> Html { get; private set; }
        public IPrincipal User { get; private set; }


        public Helpers(ViewContext viewContext, M model)
        {
            Url = new UrlHelper(viewContext.RequestContext, System.Web.Routing.RouteTable.Routes);
            User = viewContext.HttpContext.User;

            var viewData = new ViewDataDictionary(viewContext.ViewData) { Model = model };
            var viewDataContainer = new ViewDataContainer(viewData);

            Ajax = new AjaxHelper<M>(viewContext, viewDataContainer, System.Web.Routing.RouteTable.Routes);
            Html = new HtmlHelper<M>(viewContext, viewDataContainer, System.Web.Routing.RouteTable.Routes);

        }

    }
}
