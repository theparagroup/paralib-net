using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using com.paralib.Xandroid.Widgets;

namespace com.paralib.Xandroid
{
    public class XLayout
    {

        public static ScrollView ScrollView(Context context, ViewGroup.LayoutParams layoutParams, View view)
        {
            var scrollView = new ScrollView(context) { LayoutParameters = layoutParams };
            scrollView.AddView(view);
            return scrollView;
        }

        public static LinearLayout Linear(Context context, ViewGroup.LayoutParams layoutParams, Orientation orientation=Orientation.Horizontal, Color? backgroundColor = null, GravityFlags gravity = GravityFlags.NoGravity, int? id = null)
        {
            var layout= new LinearLayout(context) { LayoutParameters = layoutParams, Orientation = orientation };
            if (backgroundColor.HasValue) layout.SetBackgroundColor(backgroundColor.Value);
            layout.SetGravity(gravity);
            if (id.HasValue) layout.Id = id.Value;

            return layout;
        }

        public static FrameLayoutWithFooter FrameWithFooter(Context context, ViewGroup.LayoutParams layoutParams, int footerHeight=0, Color? backgroundColor = null, GravityFlags gravity = GravityFlags.NoGravity, int? id = null)
        {
            var frame = new FrameLayoutWithFooter(context, footerHeight) { LayoutParameters = layoutParams };
            if (backgroundColor.HasValue) frame.SetBackgroundColor(backgroundColor.Value);
            frame.SetForegroundGravity(gravity);
            if (id.HasValue) frame.Id = id.Value;

            return frame;
        }

        public static FrameLayout Frame(Context context, ViewGroup.LayoutParams layoutParams, Color? backgroundColor = null, GravityFlags gravity = GravityFlags.NoGravity, int? id = null)
        {
            var layout = new FrameLayout(context) { LayoutParameters = layoutParams};
            if (backgroundColor.HasValue) layout.SetBackgroundColor(backgroundColor.Value);
            layout.SetForegroundGravity(gravity);
            if (id.HasValue) layout.Id = id.Value;
            return layout;
        }


    }
}
