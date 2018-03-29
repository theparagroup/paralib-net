using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Core
{
    public abstract class ContextBase:IContext
    {
        public IServer Server { protected set; get; }
        public IRequest Request { protected set; get; }
        public IResponse Response { protected set; get; }
        //namespace stack
        //namespace vars

        public ContextBase(string @namespace, Dictionary<string, string> namespaceVars)
        {
            //derived classes create the other components

            //add initial namespace

        }

        


    }
}
