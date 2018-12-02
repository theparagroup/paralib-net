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
    public abstract class ParaController : Controller
    {
        protected ActionResult Page<P>(P page) where P : IPage
        {
            return View(new ParaView<P>(page));
        }

        protected ActionResult Page<P,M>(P page, M model) where P : IPage, IHasModel<M>
        {
            page.SetModel(model);
            return View(new ParaView<P>(page));
        }

    }
}