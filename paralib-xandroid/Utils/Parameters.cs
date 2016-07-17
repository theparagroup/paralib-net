using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public class Parameters
    {
        //public static ViewGroup.LayoutParams ViewGroup(float width, float height)
        //{
        //    return new ViewGroup.LayoutParams(Convert.ToInt32(width), Convert.ToInt32(height));
        //}

        //public static ViewGroup.LayoutParams ViewGroup(ParameterValues width, ParameterValues height)
        //{
        //    return new ViewGroup.LayoutParams((int)width, (int)height);
        //}

        //public static ViewGroup.LayoutParams ViewGroup(ParameterValues width, float height)
        //{
        //    return new ViewGroup.LayoutParams((int)width, Convert.ToInt32(height));
        //}

        //public static ViewGroup.LayoutParams ViewGroup(float width, ParameterValues height)
        //{
        //    return new ViewGroup.LayoutParams(Convert.ToInt32(width), (int)height);
        //}


        public static LinearLayout.LayoutParams Linear(float width, float height, float weight = 0)
        {
            return new LinearLayout.LayoutParams(Convert.ToInt32(width), Convert.ToInt32(height), weight);
        }

        public static LinearLayout.LayoutParams Linear(ParameterValues width, ParameterValues height, float weight = 0)
        {
            return new LinearLayout.LayoutParams((int)width, (int)height, weight);
        }

        public static LinearLayout.LayoutParams Linear(float width, ParameterValues height, float weight = 0)
        {
            return new LinearLayout.LayoutParams(Convert.ToInt32(width), (int)height, weight);
        }

        public static LinearLayout.LayoutParams Linear(ParameterValues width, float height, float weight = 0)
        {
            return new LinearLayout.LayoutParams((int)width, Convert.ToInt32(height), weight);
        }
    }
}
