using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen;

namespace com.parahtml.Mvc
{
    public abstract class MvcFragment: MvcBuilder, IPage
    {
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
