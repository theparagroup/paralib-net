﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Mapping
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ExplicitNameAttribute : Attribute
    {
        public string Name { private set; get; }

        public ExplicitNameAttribute(string name)
        {
            Name = name;
        }

    }

}
