﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen
{
    public interface ILazyContext
    {
        void Initialize(Context context);
    }
}
