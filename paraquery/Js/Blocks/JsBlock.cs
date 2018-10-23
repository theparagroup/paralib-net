using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Blocks;

namespace com.paraquery.Js.Blocks
{
    public abstract class JsBlock : Block
    {
        public JsBlock(IContext context) : base(context)
        {
        }

        protected override void Comment(string text)
        {
            _writer.Write($" // {text}");
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
