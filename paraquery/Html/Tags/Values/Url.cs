﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html
{
    /*

        Placeholder for a more advanced Url class... 
        prevents us from using raw strings at the moment.

    */
    public class Url: IComplexAttribute
    {
        public string Value { private set; get; }

        public Url(string value)
        {
            Value = value;
        }

    }
}
