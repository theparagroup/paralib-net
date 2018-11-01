using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Bootstrap.Grids;
using com.paraquery.Html.Tags;

namespace com.paraquery.Bootstrap
{
    public class Bs
    {
        protected Context _context;

        public Bs(Context context)
        {
            _context = context;
        }

        public IGrid Grid()
        {
            return new FluentGrid(_context);
        }
    }
}
