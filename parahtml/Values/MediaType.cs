using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;
using com.parahtml.Core;

namespace com.parahtml
{
    public class MediaType : IComplexValue<HtmlContext>
    {
        protected string _value;

        public MediaType(string value)
        {
            _value = value;
        }

        public string ToValue(HtmlContext context)
        {
            return context.Server.Url(_value);
        }
    }
}
