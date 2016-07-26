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
using SQLite;
using Utils=com.paralib.Xandroid.Utils;

namespace com.paralib.Xandroid.Cache
{
    public class CacheItem<T>: CacheItem
    {
        private T _value;

        [Ignore]
        public T Value
        {
            get
            {
                if (_value==null)
                {
                    _value= Utils.Json.DeSerialize<T>(Json);
                }

                return _value;
            }

            set
            {
                _value = value;
                Json = Utils.Json.Serialize(value);
            }

        }

    }
}