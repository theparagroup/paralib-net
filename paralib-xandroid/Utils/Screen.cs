using Android.App;
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
        public static bool IsXLarge()
        {
            return (Application.Context.Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask)==ScreenLayout.SizeXlarge;
        }
    }
}
