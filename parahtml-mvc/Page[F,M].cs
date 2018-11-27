﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using com.paralib.Gen;
using System.Web.Mvc;

namespace com.parahtml.Mvc
{
    /*

        Concrete implementation of Mvc Page.

    */
    public abstract class Page<F, M> : Page<MvcContext, F, M> where F : Page<F, M>
    {
        public override MvcContext CreateContext(ViewContext viewContext, TextWriter textWriter)
        {
            return new MvcContext(viewContext, textWriter);
        }

    }
}