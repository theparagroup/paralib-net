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


        /*  -------------------------------- DIV -------------------------------------- */

        IColumn IColumn.Div(object additional)
        {
            return ((IColumn)this).Div(null, additional);
        }

        IColumn IColumn.Div(Action<GlobalAttributes> attributes, object additional)
        {
            Div(attributes, additional);
            return this;
        }

        /*  -------------------------------- SPAN -------------------------------------- */

        IColumn IColumn.Span(object additional)
        {
            return ((IColumn)this).Span(null, additional);
        }

        IColumn IColumn.Span(Action<GlobalAttributes> attributes, object additional)
        {
            Span(attributes, additional);
            return this;
        }



    }
}
