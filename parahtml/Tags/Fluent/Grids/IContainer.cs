using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;

namespace com.parahtml.Tags.Fluent.Grids
{
    public interface IContainer
    {
        IRow Here(Action<IContainer> container);

        IRow Row(Action<GlobalAttributes> attributes, IList<string> columnClasses = null);
        IRow Row(IList<string> columnClasses = null);

        IGrid EndGrid();

    }
}
