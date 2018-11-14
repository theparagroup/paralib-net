﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;

namespace com.parahtml.Tags.Fluent.Grids
{
    public interface IRow
    {
        IRow Here(Action<IColumn> row);

        IRow Row(Action<GlobalAttributes> attributes, IList<string> columnClasses = null);
        IRow Row(IList<string> columnClasses = null);

        IColumn Column(Action<GlobalAttributes> attributes = null);
        IColumn Column(string @class);

        IGrid EndGrid();

    }
}