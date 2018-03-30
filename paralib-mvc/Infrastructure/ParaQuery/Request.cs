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
        protected WebViewPage _view;

        public Request(IContext context, WebViewPage view):base(context)
        {
            _view = view;
        }

    }
}
