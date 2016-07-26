using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace com.paralib.Xandroid.Cache
{
    /*
        These have no meaning to the cache subsystem, the application defines these.
    */
    public enum CacheStatuses
    {
        NotCached = 0,
        Downloaded = 1,
        New = 2,
        Modified = 3,
        Cached = 4,
        Uploaded = 5
    }
}