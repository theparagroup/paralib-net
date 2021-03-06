﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Mapping
{
    /*
        This is used to create member properties that can be processed by DictionaryBuilder,
        but are more complicated and may have different naming conventions than a string or bool.

        Style is a good example.
    */

    public interface IComplexValue<C> where C:Context
    {
        string ToValue(C context);
    }
}
