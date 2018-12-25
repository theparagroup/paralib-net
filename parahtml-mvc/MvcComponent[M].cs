using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Builders;

namespace com.parahtml.Mvc
{
    public abstract class MvcComponent<M> : IComponent
    {
        protected MvcPage<M> MvcPage { private set; get; }
        protected Helpers<M> Helpers { private set; get; }

        public MvcComponent(MvcPage<M> mvcPage)
        {
            MvcPage = mvcPage;
            Helpers = new Helpers<M>(MvcPage.Context.ViewContext, MvcPage.Model);
        }

        void IComponent.Open()
        {
            OnOpen();
        }

        protected abstract void OnOpen();

        void IComponent.Close()
        {
            OnClose();
        }

        protected abstract void OnClose();




    }
}
