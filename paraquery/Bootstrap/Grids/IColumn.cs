using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;

namespace com.paraquery.Bootstrap.Grids
{
    public interface IColumn
    {
        IColumn SetClasses(IList<string> classes = null);

        IRow Row(object attributes = null);
        IRow Row(Action<GlobalAttributes> attributes, object additional = null);

        IColumn Column(object attributes = null);
        IColumn Column(Action<GlobalAttributes> attributes, object additional = null);

        IGrid Grid();

        IGrid EndGrid();

        IColumn Write(string content);
        IColumn WriteLine(string content);

        IColumn Div(object additional = null);
        IColumn Div(Action<GlobalAttributes> attributes, object additional = null);

        IColumn Span(object additional = null);
        IColumn Span(Action<GlobalAttributes> attributes, object additional = null);

    }
}
