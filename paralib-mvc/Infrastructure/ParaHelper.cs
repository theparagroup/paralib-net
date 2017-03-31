using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace com.paralib.Mvc.Infrastructure
{
    public class ParaHelper<TModel>
    {
        private WebViewPage<TModel> _view;

        internal ParaHelper(WebViewPage<TModel> view)
        {
            _view = view;
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
                return _view.ViewContext.RouteData.GetRequiredString("action");
            }
        }

        public string ControllerName
        {
            get
            {
                return _view.ViewContext.RouteData.GetRequiredString("controller");
            }
        }

        public string AreaName
        {
            get
            {
                return _view.ViewContext.RouteData.DataTokens["area"]?.ToString();
            }
        }

        public MvcHtmlString Action(string url, object values = null)
        {
            return Action(new ParaAction(url), values);
        }

        public MvcHtmlString Action(ParaAction paraAction, object values=null)
        {
            var rvd = new RouteValueDictionary(values);
            if (paraAction.Area!=null) rvd.Add("area", paraAction.Area);
            return _view.Html.Action(paraAction.Action, paraAction.Controller, rvd);
        }

        public void RenderAction(string url, object values = null)
        {
            RenderAction(new ParaAction(url), values);
        }

        public void RenderAction(ParaAction paraAction, object values = null)
        {
            var rvd = new RouteValueDictionary(values);
            if (paraAction.Area != null) rvd.Add("area", paraAction.Area);
            _view.Html.RenderAction(paraAction.Action, paraAction.Controller, rvd);
        }

    }
}
