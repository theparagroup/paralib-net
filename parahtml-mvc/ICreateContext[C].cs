using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace com.parahtml.Mvc
{
    public interface ICreateContext<C> where C:MvcContext
    {
        C CreateContext(ViewContext viewContext, TextWriter textWriter);
    }
}
