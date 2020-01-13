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
            last "modified" time, meaning last time modified flag was set

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

        private static string ToKey(Type type, long? id = null)
        {
            if (id.HasValue)
            {
                return $"{type.Name}:{id}";
            }
            else
            {
                return $"{type.Name}:null";
            }
        }

        public static void Clear()
        {
            using (var db = new SQLiteConnection(DbPath))
            {
                db.DeleteAll<CacheItem>();
            }
        }

        public static void Clear<T>(long? id = null)
        {

            using (var db = new SQLiteConnection(DbPath))
            {
                db.Delete<CacheItem>(ToKey(typeof(T), id));
            }
        }

        public static List<CacheItem<T>> List<T>()
        {
            try
            {
                using (var db = new SQLiteConnection(DbPath))
                {
                    return db.Query<CacheItem<T>>($"select * from cache_items where key like '{typeof(T).Name}:%'");
                }
            }
            catch { }

            return null;
        }

        public static CacheItem<T> Get<T>(long? id = null)
        {
            try
            {
                using (var db = new SQLiteConnection(DbPath))
                {
                    return db.Get<CacheItem<T>>(ToKey(typeof(T), id));
                }
            }
            catch { }

            return null;
        }

        public static CacheItem<T> Save<T>(T value, bool? dirty = null, long? id = null)
        {
            DateTime now = DateTime.Now;

            using (var db = new SQLiteConnection(DbPath))
            {
                if (value == null)
                {
                    db.Delete<CacheItem>(ToKey(typeof(T), id));
                    return null;
                }
                else
                {
                    var newItem = new CacheItem<T>();
                    newItem.Key = ToKey(typeof(T), id);
                    newItem.Value = value;
                    newItem.SavedOn = now;

                    var oldItem = Get<T>(id);

                    if (oldItem != null)
                    {
                        newItem.CreatedOn = oldItem.CreatedOn;
                        newItem.Dirty = oldItem.Dirty;
                        newItem.DirtiedOn = oldItem.DirtiedOn;
                        newItem.CleanedOn = oldItem.CleanedOn;
                    }
                    else
                    {
                        newItem.CreatedOn =now;
                    }

                    if (dirty.HasValue)
                    {
                        newItem.Dirty = dirty.Value;

                        if (dirty.Value)
                        {
                            newItem.DirtiedOn = now;
                        }
                        else
                        {
                            newItem.CleanedOn = now;
                        }
                    }


                    db.InsertOrReplace(newItem);

                    return newItem;
                }

            }
        }

        public static void SetModified<T>(bool dirty, long? id = null)
        {
            DateTime now = DateTime.Now;

            using (var db = new SQLiteConnection(DbPath))
            {
                var oldItem = Get<T>(id);

                if (oldItem != null)
                {
                    oldItem.Dirty = dirty;

                    if (dirty)
                    {
                        oldItem.DirtiedOn = now;
                    }
                    else
                    {
                        oldItem.CleanedOn = now;
                    }

                    db.Update(oldItem);
                }

            }
        }


        public static void SetSavedOn<T>(DateTime savedOn, long? id = null)
        {
            DateTime now = DateTime.Now;

            using (var db = new SQLiteConnection(DbPath))
            {
                var oldItem = Get<T>(id);

                if (oldItem != null)
                {
                    oldItem.SavedOn = savedOn;

                    db.Update(oldItem);
                }

            }
        }


        public static bool? IsDirty<T>(long? id = null)
        {
            using (var db = new SQLiteConnection(DbPath))
            {
                var oldItem = Get<T>(id);

                if (oldItem != null)
                {
                    return oldItem.Dirty;
                }
                else
                {
                    return null;
                }

            }
        }


    }
}