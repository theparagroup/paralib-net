using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;
using System.Web.Mvc;

namespace com.paralib.Mvc.Infrastructure.ParaQuery
{
    public class Request : RequestBase
    {
        protected WebViewPage _view;

        public Request(IContext context, WebViewPage view):base(context)
        {
            _view = view;
        }

    }
}
