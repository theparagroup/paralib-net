using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using com.parahtml.Core;
using System.Web.Mvc;


namespace com.parahtml.Mvc
{
    public class MvcContext : HtmlContext
    {
        public ViewContext ViewContext { private set; get; }

        public MvcContext(ViewContext viewContext, TextWriter textWriter) : base(new MvcWriter(textWriter), new MvcServer(), GetOptions())
        {
            ViewContext = viewContext;
        }

        protected static HtmlOptions GetOptions()
        {
            //TODO integrate with config system

            var o = new HtmlOptions();

            //o.DebugSourceFormatting = true;
            //o.SelfClosingEmptyTags = false;
            o.MinimizeBooleans = true;
            o.EscapeAttributeValues = false;
            o.CssFormat = CssFormats.Readable;

            o.Debug |= DebugFlags.EndTag;
            o.Debug |= DebugFlags.Grids;

            return o;
        }

        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        public string AreaName
        {
            get
            {
                return ViewContext.Controller.ControllerContext.RouteData.DataTokens["area"]?.ToString();
            }
        }

        public string ControllerName
        {
            get
            {
                return ViewContext.Controller.ControllerContext.RouteData.GetRequiredString("controller");
            }
        }

        public string ActionName
        {
            get
            {
                return ViewContext.Controller.ControllerContext.RouteData.GetRequiredString("action");
            }
        }

    }
}