using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;

namespace com.paraquery.jQuery.Blocks
{
    public abstract class jQueryBlock : BlockRenderer, ICommentator
    {
        public jQueryBlock(IContext context) : base(context, true)
        {
        }

        public void Comment(string text)
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
