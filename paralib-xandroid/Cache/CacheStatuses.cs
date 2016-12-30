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
    public enum CacheStatuses
    {
        NotFound = 0,
        Cached = 1,
        Modified = 2,
    }
}