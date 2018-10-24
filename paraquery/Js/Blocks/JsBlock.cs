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
        public JsBlock(IContext context) : base(context, RendererTypes.Block, false)
        {
        }

        protected override void Debug(string message)
        {
            _writer.Write($" // {message}");
        }

        protected override void OnPreBegin()
        {
            _writer.Space();

            base.OnPreBegin();
        }

        protected override void OnPostEnd()
        {
            base.OnPostEnd();

            _writer.Space();
        }

    }
}
