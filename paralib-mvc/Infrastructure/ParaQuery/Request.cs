using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;
using System.Web.Mvc;

namespace com.paralib.Mvc.Infrastructure.ParaQuery
{
    public class Request : IRequest
    {
        protected WebViewPage _view;

        public Request(WebViewPage view)
        {
            _view = view;
        }

    }
}
