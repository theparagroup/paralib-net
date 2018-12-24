using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.parahtml.Mvc
{
    /*
        Interface for use with the ParaView and ParaController.

        If for some reason it is not desirable to derive from Page, this
        interface can be directly implemented.
    
    */
    public interface IPage
    {
        void Render(MvcContext context);
        void End();
    }
}