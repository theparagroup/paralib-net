using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace com.paralib.Mvc.Infrastructure
{
    public class MvcUtils
    {
        private static Dictionary<string, ParaAction> _cache = new Dictionary<string, ParaAction>();

        public static string ToAbsoluteUrl(string relativeUrl)
        {
            if (relativeUrl?.StartsWith("~") ?? false)
            {
                var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                relativeUrl = urlHelper.Content(relativeUrl);
            }

            return relativeUrl;
        }

        public static bool ToRoute(string relativeUrl, out string action, out string controller, out string area)
        {
            string url = ToAbsoluteUrl(relativeUrl);

            if (_cache.ContainsKey(url))
            {
                action = _cache[url].Action;
                controller = _cache[url].Controller;
                area = _cache[url].Area;

                return true;
            }


            var request = new HttpRequest(null, $"http://host/{url}", null);
            var response = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(request, response);

            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            if (routeData!=null)
            {
                if (routeData.Values.ContainsKey("MS_DirectRouteMatches"))
                {
                    routeData = ((IEnumerable<System.Web.Routing.RouteData>)routeData.Values["MS_DirectRouteMatches"]).First();
                }

                action = routeData?.Values["action"]?.ToString();
                controller = routeData?.Values["controller"]?.ToString();
                area = routeData?.DataTokens["area"]?.ToString();

                _cache.Add(url, new ParaAction(action, controller, area));

                return true;
            }
            else
            {
                action = null;
                controller = null;
                area = null;

                return false;
            }




        }


    }
}
