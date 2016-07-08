using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace com.paralib.Mvc.Infrastructure.WebApi2
{
    public class ParaDirectRouteProvider : DefaultDirectRouteProvider
    {
        /*
            Our GetActionRouteFactories override let's you do this:

                public class BaseController : ApiController
                {
                    [Route("{id:int}")]
                    public string Get(int id)
                    {
                        return "Success:" + id;
                    }
                }

                [RoutePrefix("api/values")]
                public class ValuesController : BaseController
                {
                }

        */
        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(true);
        }


        /*

            Our GetRoutePrefix override let's you do this:


            [RoutePrefix("api/values")]
            public class BaseController : ApiController
            {
            }

            public class ValuesController : BaseController
            {
                [Route("{id:int}")]
                public string Get(int id)
                {
                    return "Success:" + id;
                }
            }

        */

        protected override string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            string routePrefix = "";

            Type currentType = controllerDescriptor.ControllerType;

            while (currentType != null)
            {
                var routePrefixAttribute = currentType.GetCustomAttribute<RoutePrefixAttribute>(false);

                if (routePrefixAttribute != null)
                {
                    if (routePrefix != "") routePrefix = "/"+routePrefix;
                    routePrefix = routePrefixAttribute.Prefix+routePrefix;
                }

                currentType = currentType.BaseType;

            }

            return routePrefix;
        }

    }
}
