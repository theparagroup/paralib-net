﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.parahtml.Core;
using com.parahtml;
using com.paralib.Gen.Fluent;

namespace com.parahtml.Mvc
{
    /*

        Wrapper for IPage objects that implements MVC's IView interface 
        for use in MVC controllers.

    */
    public class ParaView<C, P> : IView where P : IPage<C> where C : MvcContext
    {
        protected P _page;

        public ParaView(P page)
        {
            _page = page;
        }

        public void Render(ViewContext viewContext, TextWriter textWriter)
        {
            var context = _page.CreateContext(viewContext, textWriter);
            _page.Render(context);
            _page.End();

        }


    }
}