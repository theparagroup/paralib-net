using System;
using System.Web.Mvc;

namespace com.paralib.Mvc.Infrastructure
{
    public abstract class ParaView<TModel, TBaseController> : WebViewPage<TModel> where TBaseController:ParaController
    {
        public ParaHelper<TModel> Para { private set; get; }

        public override void InitHelpers()
        {
            base.InitHelpers();

            if (!(ViewContext.Controller is TBaseController))
            {
                throw new ParalibException($"View '{((RazorView)ViewContext.View).ViewPath}' cannot be called from controller '{ViewContext.RouteData.Values["controller"]}' since it is not derived from {typeof(TBaseController).Name}.");
            }

            Para = new ParaHelper<TModel>(this);

        }



    }
}