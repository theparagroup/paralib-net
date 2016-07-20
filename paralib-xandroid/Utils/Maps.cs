using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public class Maps
    {
        public static void MapAddress(Context context, string address)
        {
            var geoUri = Android.Net.Uri.Parse(string.Format("geo:0,0?q={0}", WebUtility.UrlEncode(address)));
            var mapIntent = new Intent(Intent.ActionView, geoUri);
            context.StartActivity(mapIntent);
        }
    }
}
