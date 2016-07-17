using Android.App;
using Android.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public class Dip
    {
        public static int ToInt(float dips)
        {
            return Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, dips, Application.Context.Resources.DisplayMetrics));
        }
    }
}
