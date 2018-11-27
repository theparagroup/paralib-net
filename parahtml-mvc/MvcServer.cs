using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.parahtml.Core;
using System.Web.Mvc;

namespace com.parahtml.Mvc
{
    public class MvcServer : Server
    {
        protected UrlHelper _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

        public override string Url(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                return _urlHelper.Content(url);
            }
            else
            {
                return null;
            }
        }
    }
}

