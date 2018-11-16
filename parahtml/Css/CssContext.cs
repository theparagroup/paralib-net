using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen;

namespace com.parahtml.Css
{
    public class CssContext:HtmlContext
    {
        public CssContext(HtmlContext context) : base(context.Writer, context.Server, context.Options)
        {
        }

        public override void Comment(string text)
        {
            Writer.Write($"/* {text} */");
        }

    }
}
