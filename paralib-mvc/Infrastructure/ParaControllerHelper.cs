using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace com.paralib.Mvc.Infrastructure
{
    public class ParaControllerHelper
    {
        private Controller _controller;

        internal ParaControllerHelper(ControllerBase controller)
        {
            _controller = (Controller)controller;
        }

        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        public string ActionName
        {
            get
            {
                return _controller.RouteData.GetRequiredString("action");
            }
        }

        public string ControllerName
        {
            get
            {
                return _controller.RouteData.GetRequiredString("controller");
            }
        }

        public string AreaName
        {
            get
            {
                return _controller.RouteData.DataTokens["area"]?.ToString();
            }
        }

    }
}
