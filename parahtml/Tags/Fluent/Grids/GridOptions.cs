using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml.Tags.Fluent.Grids
{
    public class GridOptions
    {
        public IList<string> GridColumnClasses { set; get; }
        public string ContainerClass { set; get; }
        public string RowClass { set; get; }
        public string ColumnClass { set; get; }
    }
}
