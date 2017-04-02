using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace com.paralib.Mvc.Infrastructure
{
    public class ParaViewHelper<TModel>:ParaControllerHelper
    {
        private WebViewPage<TModel> _view;
        
        internal ParaViewHelper(WebViewPage<TModel> view):base(view.ViewContext.Controller)
        {
            _view = view;
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
