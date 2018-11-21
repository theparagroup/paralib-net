using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.parahtml.Core;

namespace com.parahtml.Mvc
{
    /*

        Base class for MVC controllers that simplifies using Page objects.

    */
    public class ParaController:Controller
    {
        //this should be protected or routes.MapRoute() might pick it up
        protected ActionResult Page<P>(P page) where P: IPage
        {
            return View(new ParaView<P>(page));
        }
    }
}