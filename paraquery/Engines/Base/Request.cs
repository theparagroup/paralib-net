using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Engines.Base
{
    public abstract class Request : IRequest
    {
        protected IContext _context;

        public Request(IContext context)
        {
            _context = context;
        }

    }
}
