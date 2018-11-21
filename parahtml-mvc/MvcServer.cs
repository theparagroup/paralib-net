using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.parahtml.Core;

namespace com.parahtml.Mvc
{
    public class MvcServer : Server
    {
        public override string Url(string url)
        {
            return url.Replace("~/", "/application/");
        }
    }
}

