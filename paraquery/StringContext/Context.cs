using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.StringContext
{
    public class Context:Core.Context
    {
        public Context(Server server, Request request, Response response, string @namespace, Dictionary<string, string> namespaceVars):base (server,request,response,@namespace,namespaceVars)
        {

        }

        public Context(string urlPrefix, string @namespace, Dictionary<string, string> namespaceVars) : base(new Server(urlPrefix), new Request(), new Response(), @namespace, namespaceVars)
        {

        }

    }
}
