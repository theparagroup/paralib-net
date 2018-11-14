using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen
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

    public abstract class Context
    {
        public Writer Writer { protected set; get; }
        public Options Options { protected set; get; }

        protected Context (Writer writer)
        {
            Writer = writer;
        }

        public Context(Writer writer, Action<Options> options=null)
        {
            Options = new Options();

            if (options!=null)
            {
                options(Options);                
            }

            Writer = writer;
        }

        public abstract void Comment(string text);

        public virtual string UrlPrefix(string url)
        {
            throw new NotImplementedException("need to refactor UrlPrefix out of context");
        }

    }
}
