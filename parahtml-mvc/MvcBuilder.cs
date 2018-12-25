using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Html2;
using com.paralib.Gen.Rendering;

namespace com.parahtml.Mvc
{
    public class MvcBuilder:HtmlBuilder2
    {
        public MvcBuilder():base(new ContentStack())
        {
        }

        public MvcBuilder(ContentStack contentStack):base(contentStack)
        {
        }

        public MvcBuilder(MvcBuilder mvcBuilder):base(mvcBuilder)
        {
        }

        public new MvcContext Context
        {
            get
            {
                return (MvcContext)base.Context;
            }
        }
    }
}
