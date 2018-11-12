using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;
using com.parahtml.Core;

namespace com.parahtml.Css.Fluent
{
    /*


    */
    public class CssDebugBlock : DebugRenderer
    {
        public CssDebugBlock(HtmlContext context, string name, bool debug, bool indentContent) : base(context, name, LineModes.Multiple, ContainerModes.Block, debug, indentContent)
        {
        }

        protected new HtmlContext Context
        {
            get
            {
                return (HtmlContext)base.Context;
            }
        }

        protected override bool CanDebug
        {
            get
            {
                return true;
            }
        }

        protected override void OnDebug(string text)
        {
            Writer.Write($"/* {text} */");
        }

    }
}
