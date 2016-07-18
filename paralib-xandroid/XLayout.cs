using Android.Content;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid
{
    public class XLayout
    {
       

        public static LinearLayout Linear(Context context, ViewGroup.LayoutParams layoutParams, Orientation orientation=Orientation.Horizontal, GravityFlags gravity = GravityFlags.NoGravity, int? id = null)
        {
            var layout= new LinearLayout(context) { LayoutParameters = layoutParams, Orientation = orientation };
            layout.SetGravity(gravity);
            if (id.HasValue) layout.Id = id.Value;
            return layout;
        }

        public static FrameLayout Frame(Context context, ViewGroup.LayoutParams layoutParams, GravityFlags gravity = GravityFlags.NoGravity, int? id = null)
        {
            var layout = new FrameLayout(context) { LayoutParameters = layoutParams};
            layout.SetForegroundGravity(gravity);
            if (id.HasValue) layout.Id = id.Value;
            return layout;
        }


    }
}
