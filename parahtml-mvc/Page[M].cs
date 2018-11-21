using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using com.parahtml.Core;
using com.parahtml;

namespace com.parahtml.Mvc
{
    /*

        Base class that makes a Fragment-derived class an IPage, but with a 
        Model.

    */


    public abstract class Page<M> : Page
    {
        protected M Model { private set; get; }

        public Page(M model) 
        {
            Model = model;
        }

    }
}