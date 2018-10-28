using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;

namespace com.paraquery.Bootstrap.Grids
{
    public partial class FluentGrid 
    {
        IColumn IColumn.Write(string content)
        {
            Write(content);
            return this;
        }

        IColumn IColumn.WriteLine(string content)
        {
            Write(content);
            return this;
        }

        IColumn IColumn.Div(Action<GlobalAttributes> attributes)
        {
            Div(attributes);
            return this;
        }

        IColumn IColumn.Span(Action<GlobalAttributes> attributes)
        {
            Span(attributes);
            return this;
        }



    }
}
