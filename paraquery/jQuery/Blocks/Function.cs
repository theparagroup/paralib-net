﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;

namespace com.paraquery.jQuery.Blocks
{
    /*
        $(document).ready(function() {
        ...
        );
    */
    public class Function : JsBlock
    {
        protected string _name;
        protected string[] _parameters;

        public Function(Context context, string name, params string[] parameters) : base(context)
        {
            _name = name;
            _parameters = parameters;
            Begin();
        }

        protected override void OnBegin()
        {
            Context.Response.WriteLine($"function {_name}({Utils.Parameters(_parameters)}) {{");
        }

        protected override void OnEnd()
        {
            Context.Response.WriteLine($"}} //end {_name}");
        }

    }
}