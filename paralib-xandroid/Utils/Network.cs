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
        private static bool? _networkConnected = null;
        
        //you need:
        //              <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
        public static bool Connected
        {
            get
            {
                if (_networkConnected.HasValue) return _networkConnected.Value;

                ConnectivityManager cm = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
                return cm.ActiveNetworkInfo != null;
            }

            set
            {
                //TODO currently this isn't called anywhere - clearly it is a way to override the getter... do we need this?
                _networkConnected = value;
            }

        }

    }
}


/*
    private boolean isNetworkConnected() {
ConnectivityManager cm = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);

return cm.getActiveNetworkInfo() != null;
}


    <uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />


    public boolean isInternetAvailable() {
try {
    InetAddress ipAddr = InetAddress.getByName("google.com"); //You can replace it with your name
    return !ipAddr.equals("");

} catch (Exception e) {
    return false;
}

}

*/
