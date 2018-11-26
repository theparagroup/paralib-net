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
    public abstract class ParaController : ParaController<MvcContext> 
    {
    }
}