﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags.Attributes;
using com.paraquery.Rendering;
using com.paraquery.Html.Fluent;

namespace com.paraquery.Bootstrap.Grids
{
    public interface IColumn
    {

        IRow Row(Action<GlobalAttributes> attributes, IList<string> columnClasses = null);
        IRow Row(IList<string> columnClasses = null);

        IColumn Column(Action<GlobalAttributes> attributes = null);
        IColumn Column(string @class);

        IGrid Grid(Action<GridOptions> options=null);
        IGrid EndGrid();

        IColumn Html(Action<FluentHtml> fluentHtml);

    }
}
