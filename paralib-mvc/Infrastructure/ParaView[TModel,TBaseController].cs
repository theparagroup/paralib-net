using System;
using System.Web.Mvc;

namespace com.paralib.Mvc.Infrastructure
{
    public abstract class ParaView<TModel, TBaseController> : WebViewPage<TModel> where TBaseController : ParaController
    {
        public ParaViewHelper<TModel> Para { private set; get; }
        private paraquery.pQuery _pQuery;

        public override void InitHelpers()
        {
            base.InitHelpers();

            if (!(ViewContext.Controller is TBaseController))
            {
                throw new ParalibException($"View '{((RazorView)ViewContext.View).ViewPath}' cannot be called from controller '{ViewContext.RouteData.Values["controller"]}' since it is not derived from {typeof(TBaseController).Name}.");
            }

            Para = new ParaViewHelper<TModel>(this);

        }

        public paraquery.pQuery pQuery
        {
            get
            {
                if (_pQuery == null)
                {
                    var context = new ParaQuery.Context(this);
                    var tag = new paraquery.Html.TagBuilder(context);

                    _pQuery = new paraquery.pQuery(context, tag);
                }

                return _pQuery;
            }
        }

    }



}