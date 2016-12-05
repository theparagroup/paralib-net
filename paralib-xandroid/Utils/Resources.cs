using System;
using Android.Content;
using System.IO;

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
