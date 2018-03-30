using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Engines.Base
{
    public abstract class Server : IServer
    {
        protected IContext _context;

        public Server(IContext context)
        {
            _context = context;
        }

        public abstract string UrlPrefix(string url);

    }
}
