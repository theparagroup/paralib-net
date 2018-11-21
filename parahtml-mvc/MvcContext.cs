using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using com.parahtml.Core;
using com.parahtml;

namespace com.parahtml.Mvc
{
    public class MvcContext:HtmlContext
    {
        public MvcContext(TextWriter textWriter):base(new MvcWriter(textWriter), new MvcServer(), GetOptions())
        {

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
    }
}