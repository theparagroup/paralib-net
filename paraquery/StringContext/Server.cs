using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;


namespace com.paraquery.StringContext
{
    public class Server : IServer
    {
        protected string _urlPrefix;

        public Server(string urlPrefix)
        {
            if (!(urlPrefix??"/").StartsWith("/"))
            {
                throw Utils.Exception($"Url Prefix '{urlPrefix}' must start with a '/'");
            }

            _urlPrefix = urlPrefix;
        }

        public string UrlPrefix(string url)
        {
            // prefix= "/prefix", "~/foobar" -> "/prefix/foobar"
            // prefix= null, "~/foobar" -> "/foobar"

            if (url?.StartsWith("~")??false)
            {
                return url?.Replace("~", _urlPrefix??"");
            }
            else
            {
                return url;
            }
        }
    }
}
