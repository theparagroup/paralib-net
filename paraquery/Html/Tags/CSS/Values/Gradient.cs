using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html
{
    public abstract class Gradient:BackgroundImage
    {
        public List<ColorStop> ColorStops { set; get; }
        public bool Repeating { set; get; }

    }
}
