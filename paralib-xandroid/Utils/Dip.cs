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
        public static int ToInt32(float dips)
        {
            //match, wrap
            if (dips < 0) return Convert.ToInt32(dips);

            return Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, dips, Application.Context.Resources.DisplayMetrics));
        }
        public static float FromPixels(int px)
        {
            return px/ Application.Context.Resources.DisplayMetrics.Density;
        }

    }
}
