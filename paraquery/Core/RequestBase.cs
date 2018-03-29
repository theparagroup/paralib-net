using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Core
{
    public abstract class RequestBase : IRequest
    {
        protected IContext _context;

        public RequestBase(IContext context)
        {
            _context = context;
        }

    }
}
