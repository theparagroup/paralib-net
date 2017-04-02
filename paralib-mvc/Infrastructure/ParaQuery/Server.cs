using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;
using System.Web.Mvc;

namespace com.paralib.Mvc.Infrastructure.ParaQuery
{
    public class Server : IServer
    {
        protected WebViewPage _view;

        public Server(WebViewPage view)
        {
            _view = view;
        }

        public string UrlPrefix(string url)
        {
            return _view.Url.Content(url);
        }
    }
}
