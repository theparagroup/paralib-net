using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.parahtml.Core;
using com.paralib.Gen;


namespace com.parahtml.Mvc
{
    /*
        Interface for use with the ParaView and ParaController.
    
    */
    public interface IPage: IHasContext<HtmlContext>
    {
        void Render(HtmlContext context);
        void End();
    }
}