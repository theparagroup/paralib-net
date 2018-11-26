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
    public abstract class ParaController<C> : Controller where C : MvcContext
    {
        protected ActionResult Page<P>(P page) where P : IPage<C>
        {
            return View(new ParaView<C, P>(page));
        }

        protected ActionResult Page<P,M>(P page, M model) where P : IPage<C>, IHasModel<M>
        {
            page.SetModel(model);
            return View(new ParaView<C, P>(page));
        }

    }
}