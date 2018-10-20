using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.StringContext
{
    public class Request : Engines.Base.Request
    {
        public Request(IContext context) : base(context)
        {
            _form = new NameValueCollection();
            _queryString = new NameValueCollection();
        }
    }
}
