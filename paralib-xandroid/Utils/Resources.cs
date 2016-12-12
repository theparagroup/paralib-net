using System;
using System.IO;
using Android.Content;
using Android.Graphics.Drawables;

namespace com.paralib.Xandroid.Utils
{
    public class Resources
    {
        public static Stream OpenResourceAsStream(Context context, int id)
        {
            var s = context.Resources.OpenRawResource(id);
            return s;
        }

    }
}
