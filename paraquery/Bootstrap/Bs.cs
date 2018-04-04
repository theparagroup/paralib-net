using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Bootstrap.Grids;
using com.paraquery.Html;

namespace com.paraquery.Bootstrap
{
    public class Bs
    {
        protected IContext _context { private set; get; }
        protected TagBuilder _tagBuilder { private set; get; }

        public Bs(IContext context, TagBuilder tagBuilder)
        {
            _context = context;
            _tagBuilder = tagBuilder;
        }

        public IGrid Grid()
        {
            return new FluentGrid(_context, _tagBuilder);
        }
    }
}
