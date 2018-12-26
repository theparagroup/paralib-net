using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace com.parahtml.Mvc
{
    public abstract class MvcFragment<M> : MvcFragment, IHasModel<M>
    {
        public M Model { private set; get; }

        void IHasModel<M>.SetModel(M model)
        {
            Model = model;
        }



    }
}