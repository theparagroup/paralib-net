using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    public interface IRequest
    {
        NameValueCollection Form { get; }
        NameValueCollection QueryString { get; }

        //HttpCookieCollection Cookies { get; }
        //NameValueCollection ServerVariables { get; }

        //Url
    }
}
