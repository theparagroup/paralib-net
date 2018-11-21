using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.parahtml;
using com.parahtml.Core;

namespace com.parahtml.Mvc
{
    /*
        Base class for Razor views.

        Configure in web.config in Views fold:
        
            <system.web.webPages.razor>
                <pages pageBaseType="com.parahtml.Mvc.ParaWebViewPage">

    */

    public abstract class ParaWebViewPage<M> : WebViewPage<M>
    {
        protected Fragment<MvcContext> Fragment()
        {
            var context = new MvcContext(ViewContext.Controller, ViewContext.Writer);
            return new Fragment<MvcContext>(context);
        }

    }
}