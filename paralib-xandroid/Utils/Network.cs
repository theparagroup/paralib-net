using Android.App;
using Android.Content;
using Android.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public class Network
    {
        //you need:
        //              <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
        public static bool Connected
        {
            get
            {
                ConnectivityManager cm = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);

                return cm.ActiveNetworkInfo != null;

            }

        }

    }
}
