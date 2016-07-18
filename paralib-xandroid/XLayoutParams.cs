using Android.Views;
using Android.Widget;
using com.paralib.Xandroid.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid
{
    public class XLayoutParams
    {
        public const int MATCH = -1;
        public const int WRAP = -2;

        public static ViewGroup.LayoutParams ViewGroup(float width, float height)
        {
            return new ViewGroup.LayoutParams(Dip.ToInt32(width), Dip.ToInt32(height));
        }

        public static LinearLayout.LayoutParams Linear(float width, float height, float weight = 0)
        {
            return new LinearLayout.LayoutParams(Dip.ToInt32(width), Dip.ToInt32(height), weight);
        }

    }
}
