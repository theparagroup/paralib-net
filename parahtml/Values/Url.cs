﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;
using com.parahtml.Core;

namespace com.parahtml
{
    /*

        Placeholder for a more advanced Url class... 
        prevents us from using raw strings at the moment.

    */
    public class Url: IComplexValue<HtmlContext>
    {
        protected string _value;

        public Url(string value)
        {
            _value = value;
        }

        public string ToValue(HtmlContext context)
        {
            return context.Server.Url(_value);
        }
    }
}
