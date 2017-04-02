using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace com.paralib.Mvc.Infrastructure
{
    public class ParaController: Controller
    {
        public ParaControllerHelper Para { private set; get; }

        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            //Called before the action method is invoked.
            base.OnActionExecuting(ctx);
            Para = new ParaControllerHelper(this);
        }

    }
}
