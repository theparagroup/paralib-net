using Android.Content;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public class Layouts
    {
       

        public static LinearLayout Linear(Context context, LinearLayout.LayoutParams layoutParams, Orientation orientation=Orientation.Vertical, GravityFlags gravity = GravityFlags.NoGravity)
        {
            var layout= new LinearLayout(context) { LayoutParameters = layoutParams, Orientation = orientation };
            layout.SetGravity(gravity);
            return layout;
        }



    }
}
