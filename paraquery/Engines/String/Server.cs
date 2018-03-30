using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.StringContext
{
    public class Server : Engines.Base.Server
    {
        protected string _urlPrefix;

        public Server(IContext context, string urlPrefix) : base(context)
        {
            if (!(urlPrefix ?? "/").StartsWith("/"))
            {
                throw Utils.Exception($"Url Prefix '{urlPrefix}' must start with a '/'");
            }

            _urlPrefix = urlPrefix;
        }

        public override string UrlPrefix(string url)
        {
            // prefix= "/prefix", "~/foobar" -> "/prefix/foobar"
            // prefix= null, "~/foobar" -> "/foobar"

            if (url?.StartsWith("~") ?? false)
            {
                return url?.Replace("~", _urlPrefix ?? "");
            }
            else
            {
                return url;
            }
        }
    }
}
