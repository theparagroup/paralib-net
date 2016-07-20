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
using com.paralib.Xandroid.Utils;
using System.IO;

namespace com.paralib.Xandroid.Cache
{
    public static class CacheManager
    {
        static CacheManager()
        {
            Init();
        }

        public static string DbPath
        {
            get
            {
                return FileSystem.GetPersonalPath("cache.db");
            }
        }

        private static void Init()
        {
            using (var db = new SQLiteConnection(DbPath))
            {
                db.CreateTable<CacheItem>();
            }
        }

        public static void DeleteDb()
        {
            File.Delete(DbPath);
            Init();
        }


        private static string ToKey(Type type, int? id=null)
        {
            if (id.HasValue)
            {
                return $"{type.Name}:{id}";
            }
            else
            {
                return $"{type.Name}";
            }
        }


        public static void Clear()
        {
            using (var db = new SQLiteConnection(DbPath))
            {
                db.DeleteAll<CacheItem>();
            }
        }

        public static void Free()
        {
            throw new NotImplementedException();

            //using (var db = new SQLiteConnection(DbPath))
            //{
            //    db.Execute("delete from cache_items where status != 2");
            //}
        }

        public static void Clear<T>(int? id=null)
        {

            using (var db = new SQLiteConnection(DbPath))
            {
                db.Delete<CacheItem>(ToKey(typeof(T),id));
            }
        }

        public static CacheItem<T> Get<T>(int? id=null)
        {
            try
            {
                using (var db = new SQLiteConnection(DbPath))
                {
                    return db.Get<CacheItem<T>>(ToKey(typeof(T),id));
                }
            }
            catch { }

            return null;
        }

        public static List<CacheItem<T>> List<T>()
        {
            try
            {
                using (var db = new SQLiteConnection(DbPath))
                {
                    return db.Query<CacheItem<T>>($"select * from cache_items where key like '{ToKey(typeof(T))}%'");
                }
            }
            catch { }

            return null;
        }


        public static CacheItem<T> Save<T>(T value, CacheStatuses cacheStatus, int? id=null)
        {
            using (var db = new SQLiteConnection(DbPath))
            {
                if (value == null)
                {
                    db.Delete<CacheItem>(ToKey(typeof(T),id));
                    return null;
                }
                else
                {
                    var oldItem = Get<T>(id);

                    var newItem = new CacheItem<T>();
                    newItem.Key = ToKey(typeof(T), id);
                    newItem.Value = value;
                    newItem.CacheStatus = cacheStatus;

                    var now = DateTime.Now;

                    if (oldItem!=null)
                    {
                        newItem.CreatedOn = oldItem.CreatedOn;
                        newItem.RetrievedOn = oldItem.RetrievedOn;
                        newItem.ModifiedOn = oldItem.ModifiedOn;
                        newItem.UploadedOn = oldItem.UploadedOn;
                    }
                    else
                    {
                        newItem.CreatedOn = now; ;
                    }

                    if (cacheStatus==CacheStatuses.Downloaded) newItem.RetrievedOn=now;
                    if (cacheStatus == CacheStatuses.Modified) newItem.ModifiedOn = now;
                    if (cacheStatus == CacheStatuses.Uploaded) newItem.UploadedOn = now;

                    db.InsertOrReplace(newItem);

                    return newItem;
                }

            }
        }



    }
}