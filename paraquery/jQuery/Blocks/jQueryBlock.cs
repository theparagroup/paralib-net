using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;

namespace com.paraquery.jQuery.Blocks
{
    public abstract class jQueryBlock : Renderer
    {
        public jQueryBlock(IContext context) : base(context, RenderModes.Block)
        {
        }

        protected override void Comment(string text)
        {
            Writer.Write($" // {text}");
        }

        protected override void OnPreBegin()
        {
            Writer.Space();

            base.OnPreBegin();
        }

        protected override void OnPostEnd()
        {
            base.OnPostEnd();

            Writer.Space();
        }

    }
}
