using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;

namespace com.paraquery.jQuery.Blocks
{
    public abstract class JsBlock : Block
    {
        public JsBlock(IContext context) : base(context)
        {
        }
    }
}
