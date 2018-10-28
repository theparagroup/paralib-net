using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Rendering;

namespace com.paraquery.Bootstrap.Grids
{
    public class Grid : BlockRenderer
    {
        public Grid(IContext context) : base(context, true, true)
        {
        }

        protected override void OnBegin()
        {
            _writer.Write("<!-- grid start -->");
        }

        protected override void OnEnd()
        {
            _writer.Write("<!-- grid end -->");
        }
    }
}
