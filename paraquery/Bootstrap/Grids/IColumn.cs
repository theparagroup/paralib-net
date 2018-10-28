using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;
using com.paraquery.Rendering;

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

        IColumn Write(string content);
        IColumn WriteLine(string content);

        IColumn Div(Action<GlobalAttributes> attributes = null);
        IColumn Span(Action<GlobalAttributes> attributes = null);

    }
}
