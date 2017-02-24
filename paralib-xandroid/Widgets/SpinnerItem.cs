using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Widgets
{
    public class SpinnerItem : ISpinnerItem
    {
        public string Key { get; set; }
        public string Display { get; set; }
        public string ButtonDisplay { get; set; }
        public string DropdownDisplay { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            //SpinnerItemAdapter makes this uneccessary...
            return Key;
        }

    }
}
