using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using com.paraquery;

namespace com.paralib.Mvc.Infrastructure.ParaQuery
{
    public class Server : paraquery.Engines.Base.Server
    {
        protected WebViewPage _view { get; private set; }

        public Server(IContext context, WebViewPage view) :base(context)
        {
            _view = view;
        }

        public override string UrlPrefix(string url)
        {
            return _view.Url.Content(url);
        }

    }
}
