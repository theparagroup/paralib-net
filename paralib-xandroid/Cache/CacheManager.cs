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
    /*

        Essentially we want to track:
        
            initial creation time
            last saved time
            last "modified" time, where the application detirmines the modified state

        TODO Refactor status to just be a boolean, or some applicaation-defined integer status value
        TODO perhaps change modified column to "status_changed_on"

    */


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

        public static CacheStatuses GetStatus<T>(int? id = null)
        {
            using (var db = new SQLiteConnection(DbPath))
            {
                var oldItem = Get<T>(id);

                if (oldItem!=null)
                {
                    return oldItem.CacheStatus;
                }
                else
                {
                    return CacheStatuses.NotFound;
                }

            }
        }

        public static void UpdateStatus<T>(bool modified, int? id = null)
        {
            using (var db = new SQLiteConnection(DbPath))
            {
                var oldItem = Get<T>(id);

                if (oldItem!=null)
                {
                    if (modified)
                    {
                        oldItem.CacheStatus = CacheStatuses.Modified;
                        oldItem.ModifiedOn = DateTime.Now;
                    }
                    else
                    {
                        oldItem.CacheStatus = CacheStatuses.Cached;
                        oldItem.CachedOn = DateTime.Now;
                    }

                    db.Update(oldItem);
                }


            }
        }

        public static CacheItem<T> Save<T>(T value, bool modified=false, int? id = null)
        {
            using (var db = new SQLiteConnection(DbPath))
            {
                if (value == null)
                {
                    db.Delete<CacheItem>(ToKey(typeof(T), id));
                    return null;
                }
                else
                {
                    var oldItem = Get<T>(id);

                    var newItem = new CacheItem<T>();
                    newItem.Key = ToKey(typeof(T), id);
                    newItem.Value = value;

                    if (oldItem != null)
                    {
                        newItem.CreatedOn = oldItem.CreatedOn;
                        newItem.CachedOn = oldItem.CachedOn;
                        newItem.ModifiedOn = oldItem.ModifiedOn;
                    }
                    else
                    {
                        newItem.CreatedOn = DateTime.Now;
                    }


                    if (modified)
                    {
                        newItem.CacheStatus = CacheStatuses.Modified;
                        newItem.ModifiedOn = DateTime.Now;
                    }
                    else
                    {
                        newItem.CacheStatus = CacheStatuses.Cached;
                        newItem.CachedOn = DateTime.Now;
                    }

                    db.InsertOrReplace(newItem);

                    return newItem;
                }

            }
        }



    }
}