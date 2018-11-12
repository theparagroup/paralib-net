using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml
{
    public abstract class Gradient
    {
        public List<ColorStop> ColorStops { set; get; }
        public bool Repeating { set; get; }



    }
}
