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

        Base class that makes a Fragment-derived class an IPage.

    */
    public abstract class Page : Fragment<MvcContext>, IPage 
    {
        public Page() : base(null)
        {
        }

        void IPage.Render(MvcContext context)
        {
            ((IPage)this).SetContext(context);
            OnRender();
        }

        protected abstract void OnRender();

        void IPage.End()
        {
            Dispose();    
        }

       

    }
}