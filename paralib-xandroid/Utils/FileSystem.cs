using Android.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public static class FileSystem
    {
        public static string GetPersonalPath(string fileName)
        {
            //$HOME -> /data/data/@PACKAGE_NAME@/files
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);
        }

    }
}
