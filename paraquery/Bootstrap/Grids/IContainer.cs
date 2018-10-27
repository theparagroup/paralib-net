﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;

namespace com.paraquery.Bootstrap.Grids
{
    public interface IContainer
    {
        IContainer SetClasses(IList<string> classes = null);

        IRow Row(Action<GlobalAttributes> attributes = null);

        IGrid EndGrid();

    }
}
