using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.parahtml.Core;

namespace com.parahtml.Mvc
{
    /*
        Base class for Razor views.

        Configure in web.config in Views fold:
        
            <system.web.webPages.razor>
                <pages pageBaseType="com.parahtml.Mvc.ParaWebViewPage">


        With optional models (object is default if not specified):

            @model foo.MyModel
            @model int
            @model dynamic


        Note you can use a concrete class or an expando object with dynamic, but not
        an anonymous type.


        Instead of useing @model, use @inherits:

            @inherits com.parahtml.Mvc.ParaWebViewPage<object>
            @inherits com.parahtml.Mvc.ParaWebViewPage<dynamic>
            @inherits com.parahtml.Mvc.ParaWebViewPage<foo.MyModel>


        Or with custom base classes:

            public class MyView : ParaWebViewPage<object>
            {
            }

            public class MyView : ParaWebViewPage<dynamic>
            {
            }

            public class MyView<M> : ParaWebViewPage<M>
            {
            }


    */

    public abstract class ParaWebViewPage<M> : WebViewPage<M>, ICreateContext
    {
        public abstract MvcContext CreateContext(ViewContext viewContext, TextWriter textWriter);

        protected Fragment Fragment()
        {
            var fragment = new Fragment();
            var context = new MvcContext(ViewContext, ViewContext.Writer);
            ((IFluentHtmlBase)fragment).SetContext(context);
            return fragment;
        }

    }
}