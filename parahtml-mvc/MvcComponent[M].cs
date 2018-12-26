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
        protected MvcFragment<M> Fragment { private set; get; }
        protected Helpers<M> Helpers { private set; get; }

        public MvcComponent(MvcFragment<M> mvcFragment)
        {
            Fragment = mvcFragment;
            Helpers = new Helpers<M>(Fragment.Context.ViewContext, Fragment.Model);
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
