using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;

namespace com.paraquery.Js.Blocks
{
    public abstract class JsBlock : Renderer
    {
        public JsBlock(Context context) : base(context, LineModes.Multiple, StackModes.Nested, false, true, true)
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
