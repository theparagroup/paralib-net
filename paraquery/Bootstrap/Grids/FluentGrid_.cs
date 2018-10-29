using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;

namespace com.paraquery.Bootstrap.Grids
{
    /*

        Note: because we are excplitly implementing this interface, we don't need to include
                optional parameters here. They are optional in the IColumn interface, however.


    */

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

        IColumn IColumn.Block(string name, Action<GlobalAttributes> attributes, bool empty)
        {
            return Push(_tagBuilder.Block(name, attributes, empty));
        }

        IColumn IColumn.Inline(string name, Action<GlobalAttributes> attributes, bool empty)
        {
            return Push(_tagBuilder.Inline(name, attributes, empty));
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

        IColumn IColumn.Hr(Action<HrAttributes> attributes)
        {
            Hr(attributes);
            return this;
        }


    }
}
