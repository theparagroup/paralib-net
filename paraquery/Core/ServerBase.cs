using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Core
{
    public abstract class ServerBase : IServer
    {
        protected IContext _context;

        public ServerBase(IContext context)
        {
            _context = context;
        }

        public abstract string UrlPrefix(string url);

    }
}
