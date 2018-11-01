using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;

namespace com.paraquery.Bootstrap.Grids
{
    public class RowTag : Tag
    {
        public RowTag(Context context, AttributeDictionary attributes) : base(context, "div", true, false, attributes)
        {
        }
    }
}
