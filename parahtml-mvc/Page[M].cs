using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using com.paralib.Gen;


namespace com.parahtml.Mvc
{
    /*

        Concrete implementation of Mvc Page.

    */
    public abstract class Page<M> : Page<Page<M>, M> 
    {
    }
}