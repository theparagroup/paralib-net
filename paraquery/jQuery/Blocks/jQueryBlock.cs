using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.jQuery.Blocks
{
    public abstract class jQueryBlock : Block
    {
        public jQueryBlock(IContext context) : base(context)
        {
        }

        protected override void OnPreBegin()
        {
            if (!_response.IsSpaced)
            {
                _response.NewLine();
            }

            base.OnPreBegin();
        }

        protected override void OnPostEnd()
        {
            base.OnPostEnd();

            if (!_response.IsSpaced)
            {
                _response.NewLine();
            }
        }

    }
}
