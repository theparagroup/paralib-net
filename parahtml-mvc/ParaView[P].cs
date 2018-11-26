using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.parahtml.Core;
using com.parahtml;
using com.paralib.Gen.Fluent;

namespace com.parahtml.Mvc
{
    /*


    */
    public class ParaView<P> : ParaView<MvcContext, P> where P: IPage<MvcContext>
    {
        public ParaView(P page):base(page)
        {
            _page = page;
        }
    }
}