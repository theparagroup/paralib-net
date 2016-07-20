using Android.App;
using Android.Content;
using Android.Telephony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public class Phone
    {
        public static bool? _enabled;

        //ACTION_CALL requires?:
        //<uses-permission android:name="android.permission.CALL_PHONE" />
        //(ACTION_DIAL does not)
        public static void CallNumber(Context context, string number)
        {
            //Intent.ACTION_VIEW
            var telUri = Android.Net.Uri.Parse(string.Format("tel:{0}", number));
            var mapIntent = new Intent(Intent.ActionDial, telUri);
            context.StartActivity(mapIntent);
        }

        public static bool Enabled
        {
            get
            {
                if (_enabled.HasValue) return _enabled.Value;

                TelephonyManager tm = (TelephonyManager)Application.Context.GetSystemService(Context.TelephonyService);
                return tm != null && tm.SimState == SimState.Ready;
            }

            set
            {
                _enabled = value;
            }

        }

    }
}
