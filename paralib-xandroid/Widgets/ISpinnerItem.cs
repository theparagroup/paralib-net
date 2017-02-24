using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Widgets
{
    public interface ISpinnerItem
    {
        string Key { get; set; }
        string Display { get; set; }
        string ButtonDisplay { get; set; }
        string DropdownDisplay { get; set; }
        object Value { get; set; }
    }
}
