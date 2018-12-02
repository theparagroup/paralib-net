﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.parahtml.Core;
using com.parahtml.Html;

namespace com.parahtml.Grids
{
    public interface IRow
    {
        IRow Here(Action<IRow> row);
        IRow Html(Action<FluentHtml> html);

        IRow Row(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IRow Row(string @class, string[] columnClasses = null);
        IRow Row(string[] columnClasses = null);

        IColumn Column(Action<GlobalAttributes> attributes = null);
        IColumn Column(string @class);
    }
}
