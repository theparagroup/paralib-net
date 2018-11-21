﻿using System;
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
    public interface IPage : IHasContext<MvcContext> 
    {
        void Render(MvcContext context);
        void End();
    }
}