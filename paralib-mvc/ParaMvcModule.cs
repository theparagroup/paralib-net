using System;
using System.Web;

namespace com.paralib.mvc
{
    public class ParaMvcModule : IHttpModule
    {
        //private static ILog _logger = LogManager.GetLogger(typeof(HelloController));

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
           // throw new NotImplementedException();
        }
    }
}
