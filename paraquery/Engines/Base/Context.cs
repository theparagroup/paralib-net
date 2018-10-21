using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Engines.Base
{
    public abstract class Context:IContext
    {
        public IServer Server { protected set; get; }
        public IRequest Request { protected set; get; }
        public IWriter Writer { protected set; get; }
        public Options Options { get; set; } = new Options();

        //namespace stack
        //namespace vars

        public Context(string @namespace, Dictionary<string, string> namespaceVars)
        {
            Options.DebugSourceFormatting = true;

            //derived classes create the other components

            //add initial namespace

        }


    }
}
