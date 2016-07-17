using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public class Views
    {

        public static TextView SmallText(Context context, ViewGroup.LayoutParams layoutParams, string text = null, GravityFlags gravity = GravityFlags.NoGravity)
        {
            return Text(context, layoutParams, text, Android.Resource.Style.TextAppearanceSmall);
        }

        public static TextView MediumText(Context context, ViewGroup.LayoutParams layoutParams, string text = null, GravityFlags gravity = GravityFlags.NoGravity)
        {
            return Text(context, layoutParams, text, Android.Resource.Style.TextAppearanceMedium);
        }

        public static TextView LargeText(Context context, ViewGroup.LayoutParams layoutParams, string text = null, GravityFlags gravity = GravityFlags.NoGravity)
        {
            return Text(context, layoutParams, text, Android.Resource.Style.TextAppearanceLarge);
        }

        public static TextView Text(Context context, ViewGroup.LayoutParams layoutParams, string text = null, int? textAppearance=null ,  GravityFlags gravity=GravityFlags.NoGravity)
        {
            var view= new TextView(context) { LayoutParameters = layoutParams, Gravity = gravity };
            if (textAppearance.HasValue) view.SetTextAppearance(context, textAppearance.Value);
            view.Text = text;
            return view;
        }

        public static TextView Line(Context context, ParameterValues width, float height, Color color, GravityFlags gravity = GravityFlags.NoGravity)
        {
            var view = new TextView(context) { LayoutParameters = Parameters.Linear(width, height), Gravity = gravity };
            view.SetBackgroundColor(Color.Black);
            return view;
        }


    }
}
