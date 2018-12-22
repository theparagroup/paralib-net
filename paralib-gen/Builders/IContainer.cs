﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Builders
{
    public interface IContainer
    {
        Context Context { get; }
        RendererStack RendererStack { get; }
    }
}
