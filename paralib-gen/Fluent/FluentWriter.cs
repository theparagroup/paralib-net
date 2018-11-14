﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Fluent
{
    public class FluentWriter<C> : FluentWriter<C, FluentWriter<C>> where C:Context
    {
        public FluentWriter(C context, RendererStack rendererStack) : base(context, rendererStack)
        {
        }
    }
}
