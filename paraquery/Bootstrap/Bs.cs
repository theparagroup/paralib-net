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
        protected TagBuilder _tagBuilder;
        protected Context _context;

        public Bs(TagBuilder tagBuilder)
        {
            _tagBuilder = tagBuilder;
            _context = _tagBuilder.Context;
        }

        public IGrid Grid()
        {
            return new FluentGrid(_tagBuilder);
        }
    }
}
