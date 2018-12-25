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
    public abstract class MvcPage<M> : MvcBuilder, IPage, IHasModel<M>
    {
        public M Model { private set; get; }

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