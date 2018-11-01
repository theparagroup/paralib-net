using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    public class Context
    {
        public Writer Writer { private set; get; }

        public Options Options { get; set; }=new Options();

        //namespace stack
        //namespace vars

        public Context(Writer writer, Action<Options> init=null)
        {
            if (init!=null)
            {
                init(Options);                
            }

            Writer = writer;

            //derived classes create the other components

            //add initial namespace

        }

        public virtual string UrlPrefix(string url)
        {
            throw new NotImplementedException("need to refactor UrlPrefix out of context");
        }

    }
}
