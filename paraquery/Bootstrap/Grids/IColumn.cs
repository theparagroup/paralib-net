using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;
using com.paraquery.Rendering;
using com.paraquery.Html.Fluent;

namespace com.paraquery.Bootstrap.Grids
{
    public interface IColumn
    {
        IColumn SetClasses(IList<string> classes = null);

        IRow Row(Action<GlobalAttributes> attributes = null);

        IColumn Column(string classes);
        IColumn Column(Action<GlobalAttributes> attributes = null);

        IGrid Grid();
        IGrid EndGrid();

        IColumn Open(Renderer renderer);
        IColumn Close();

        IColumn Html(Action<FluentHtml> content);


        //shorten/remove these?
        IColumn Write(string content);
        IColumn WriteLine(string content);

        //remove these?
        IColumn Block(string name, Action<GlobalAttributes> attributes = null, bool empty = false);
        IColumn Inline(string name, Action<GlobalAttributes> attributes = null, bool empty = false);
        IColumn Div(Action<GlobalAttributes> attributes = null);
        IColumn Span(Action<GlobalAttributes> attributes = null);
        IColumn Hr(Action<HrAttributes> attributes = null);

    }
}
