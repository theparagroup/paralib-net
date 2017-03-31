using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Mvc.Infrastructure
{
    public class ParaAction
    {
        public ParaAction(string action, string controller, string area=null)
        {
            Area = area;
            Controller = controller;
            Action = action;
        }

        public ParaAction(string url)
        {
            string action;
            string controller;
            string area;

            if (MvcUtils.ToRoute(url,out action,out controller,out area))
            {
                Action = action;
                Controller = controller;
                Area = area;
            }
            else
            {
                throw new Exception($"Can't resolve {url}");
            }
        }

        public string Area { private set; get; }
        public string Controller { private set; get; }
        public string Action { private set; get; }
    }
}
