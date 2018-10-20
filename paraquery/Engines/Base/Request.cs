using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Engines.Base
{
    public abstract class Request : IRequest
    {
        protected IContext _context;
        protected NameValueCollection _form;
        protected NameValueCollection _queryString;

        public Request(IContext context)
        {
            _context = context;
        }

        public NameValueCollection Form { get; protected set; }

        public NameValueCollection QueryString { get; protected set; }

    }
}
