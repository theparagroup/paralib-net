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

        public static readonly ViewGroup.LayoutParams MatchMatch = new ViewGroup.LayoutParams(MATCH, MATCH);
        public static readonly ViewGroup.LayoutParams MatchWrap = new ViewGroup.LayoutParams(MATCH, WRAP);
        public static readonly ViewGroup.LayoutParams WrapMatch = new ViewGroup.LayoutParams(WRAP, MATCH);
        public static readonly ViewGroup.LayoutParams WrapWrap = new ViewGroup.LayoutParams(WRAP, WRAP);


        public static ViewGroup.LayoutParams ViewGroup(float width, float height)
        {
            var layoutParams = new ViewGroup.LayoutParams(Dip.ToInt32(width), Dip.ToInt32(height));
            return layoutParams;
        }

        public static LinearLayout.LayoutParams Linear(float width, float height, float weight = 0, GravityFlags? gravity=null, float? leftMargin = null, float? topMargin = null, float? rightMargin = null, float? bottomMargin = null)
        {
            var layoutParams= new LinearLayout.LayoutParams(Dip.ToInt32(width), Dip.ToInt32(height), weight);
            if (gravity.HasValue) layoutParams.Gravity = gravity.Value;
            layoutParams.SetMargins(Dip.ToInt32(leftMargin ?? 0), Dip.ToInt32(topMargin ?? 0), Dip.ToInt32(rightMargin ?? 0), Dip.ToInt32(bottomMargin ?? 0));

            return layoutParams;

        }

    }
}
