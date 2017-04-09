using Android.App;
using Android.Content;
using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public class Screen
    {
        public static bool IsSmall()
        {
            return (Application.Context.Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask) == ScreenLayout.SizeSmall;
        }
        public static bool IsNormal()
        {
            return (Application.Context.Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask) == ScreenLayout.SizeNormal;
        }
        public static bool IsLarge()
        {
            return (Application.Context.Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask) == ScreenLayout.SizeLarge;
        }
        public static bool IsXLarge()
        {
            return (Application.Context.Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask)==ScreenLayout.SizeXlarge;
        }

        public static int GetWidth(Activity activity)
        {
            return activity.WindowManager.DefaultDisplay.Width;
        }

        public static int GetHeight(Activity activity)
        {
            return activity.WindowManager.DefaultDisplay.Height;
        }
    }
}
