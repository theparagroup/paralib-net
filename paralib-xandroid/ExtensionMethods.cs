using Android.Graphics.Drawables;
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
    public static class ExtensionMethods
    {
        public static View XSetPadding(this View view, float padding)
        {
            int p = Dip.ToInt32(padding);
            view.SetPadding(p, p, p, p);
            return view;
        }

        public static TextView XSetPadding(this TextView view, float? left = null, float? top = null, float? right = null, float? bottom = null)
        {
            return (TextView) XSetPadding((View)view, left, top, right, bottom);
        }

        public static View XSetPadding(this View view, float? left=null, float? top  = null, float? right = null, float? bottom = null)
        {
            view.SetPadding(Dip.ToInt32(left ?? 0), Dip.ToInt32(top ?? 0), Dip.ToInt32(right ?? 0), Dip.ToInt32(bottom ?? 0));
            return view;
        }

        public static Drawable XSetBounds(this Drawable drawable, float? left = null, float? top = null, float? right = null, float? bottom = null)
        {
            drawable.SetBounds(Dip.ToInt32(left ?? 0), Dip.ToInt32(top ?? 0), Dip.ToInt32(right ?? drawable.IntrinsicWidth), Dip.ToInt32(bottom ?? drawable.IntrinsicWidth));
            return drawable;
        }


    }
}
