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
        protected ControllerBase _controller;

        public MvcContext(ControllerBase controller, TextWriter textWriter) : base(new MvcWriter(textWriter), new MvcServer(), GetOptions())
        {
            _controller = controller;
        }

        protected static HtmlOptions GetOptions()
        {
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
                return _controller.ControllerContext.RouteData.DataTokens["area"]?.ToString();
            }
        }

        public string ControllerName
        {
            get
            {
                return _controller.ControllerContext.RouteData.GetRequiredString("controller");
            }
        }

        public string ActionName
        {
            get
            {
                return _controller.ControllerContext.RouteData.GetRequiredString("action");
            }
        }

    }
}