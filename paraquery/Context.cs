using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    /*
        Since paraquery is essentially an HTML/JavaScript generator (but could be
        used for any structured content), our Context object doesn't contain any
        web server or outside environment data or functionality (Request or Response
        objects).

        Paraquery doesn't know about Urls, QueryStrings, or how to Redirect, for example.

        Paraquery applications should derive from Context to provide these types of
        features to custom components and renderers.
    */

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

        public bool IsDebug(DebugFlags debugFlags)
        {
            return (Options.Debug & debugFlags) != 0;
        }

        public virtual string UrlPrefix(string url)
        {
            throw new NotImplementedException("need to refactor UrlPrefix out of context");
        }

    }
}
