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
        public jQueryBlock(Context context) : base(context, RenderModes.Block)
        {
        }

        protected override void OnDebug(string text)
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
