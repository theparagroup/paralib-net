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
        //Environment.SpecialFolder.LocalApplicationData -> $HOME/.local/share -> /data/data/@PACKAGE_NAME@/files/.local/share
        //Environment.SpecialFolder.ApplicationData -> $HOME/.config -> /data/data/@PACKAGE_NAME@/files/.config
        //Application.Context.GetDatabasePath(dbFileName) -> /data/data/@PACKAGE_NAME@/databases/

        public static string GetPersonalPath(string fileName)
        {
            //Environment.SpecialFolder.Personal -> $HOME -> /data/data/@PACKAGE_NAME@/files
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);
        }

    }
}
