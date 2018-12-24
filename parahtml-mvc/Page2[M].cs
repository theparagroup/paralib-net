using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using com.paralib.Gen;
using System.Web.Mvc;
using com.parahtml.Html2;

namespace com.parahtml.Mvc
{
    public abstract class Page2<M> : Fragment2, IPage, IHasModel<M>
    {
        protected M Model { private set; get; }

        protected new MvcContext Context
        {
            get
            {
                return (MvcContext)base.Context;
            }
        }

        void IHasModel<M>.SetModel(M model)
        {
            Model = model;
        }

        void IPage.Render(MvcContext context)
        {
            ((ILazyContext)this).Initialize(context);
            OnRender();
        }

        protected abstract void OnRender();

        void IPage.End()
        {
            CloseAll();
        }


    }
}