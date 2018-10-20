using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using com.paraquery;

namespace com.paralib.Mvc.Infrastructure.ParaQuery
{
    public class Request : paraquery.Engines.Base.Request
    {
        protected ViewContext _viewContext;

        public Request(IContext context, ViewContext viewContext):base(context)
        {
            _viewContext = viewContext;

            _form = _viewContext.RequestContext.HttpContext.Request.Form;
            _queryString = _viewContext.RequestContext.HttpContext.Request.QueryString;

        }

    }
}
