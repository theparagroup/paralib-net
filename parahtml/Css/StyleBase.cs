using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;
using com.paralib.Gen;
using com.parahtml.Core;

namespace com.parahtml.Css
{
    public abstract class StyleBase //: IComplexValue<HtmlContext>
    {
        public object Properties { set; get; }

       

        //protected virtual string GetProperties(HtmlContext context)
        //{
        //    //var properties = context.PropertyBuilder.Properties(this);
        //    //return context.PropertyBuilder.ToDeclaration(properties);
        //    return "";
        //}

        //public string ToValue(HtmlContext context)
        //{
        //    return GetProperties(context);
        //}

    }
}
