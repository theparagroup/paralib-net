﻿using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Xandroid.Utils
{
    public class ApplicationPreferences : IDisposable
    {
        const string DEFAULTNAME = "APPLICATION";
        protected static ISharedPreferences _sharedPreferences;
        protected ISharedPreferencesEditor _editor;

        public ApplicationPreferences()
        {
            _editor = SharedPreferences.Edit();
        }

        protected static ISharedPreferences SharedPreferences
        {
            get
            {
                if (_sharedPreferences == null)
                {
                    _sharedPreferences = Application.Context.GetSharedPreferences(DEFAULTNAME, FileCreationMode.Private);
                }
                return _sharedPreferences;
            }
        }


        public static T Get<T>(string key, T @default=default(T)) where T : IConvertible
        {

            if (typeof(T)==typeof(string))
            {
                string default2 = (string)Convert.ChangeType(@default, typeof(string));
                return (T)Convert.ChangeType(SharedPreferences.GetString(key, default2), typeof(T));
            }
            else if (typeof(T) == typeof(int))
            {
                int default2 = (int)Convert.ChangeType(@default, typeof(int));
                return (T)Convert.ChangeType(SharedPreferences.GetInt(key, default2), typeof(T));
            }
            else if (typeof(T) == typeof(bool))
            {
                bool default2 = (bool)Convert.ChangeType(@default, typeof(bool));
                return (T)Convert.ChangeType(SharedPreferences.GetBoolean(key, default2), typeof(T));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void Put<T>(string key, T value) where T : IConvertible
        {

            if (typeof(T) == typeof(string))
            {
                _editor.PutString(key,(string)Convert.ChangeType(value, typeof(string)));
            }
            else if (typeof(T) == typeof(int))
            {
                _editor.PutInt(key, (int)Convert.ChangeType(value, typeof(int)));
            }
            else if (typeof(T) == typeof(bool))
            {
                _editor.PutBoolean(key, (bool)Convert.ChangeType(value, typeof(bool)));
            }
            else
            {
                throw new NotImplementedException();
            }
        }


        public void Put(string key, string value)
        {
            Put<string>(key, value);
        }

        public void Put(string key, int value)
        {
            Put<int>(key, value);
        }

        public void Save()
        {
            _editor.Apply();
            _editor = null;
        }

        public void Dispose()
        {
            Save();
        }
    }
}
