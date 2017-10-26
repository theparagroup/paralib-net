﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;

namespace com.paralib.Xandroid.Utils
{
    public static class PackageInfo
    {
        public static string GetVersion(Context context)
        {
            return context.PackageManager.GetPackageInfo(context.PackageName,0).VersionName;
        }

    }
}
