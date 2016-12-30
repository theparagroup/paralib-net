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
    [Table("cache_items")]
    public abstract class CacheItem
    {

        [PrimaryKey]
        [MaxLength(256)]
        [Column("key")]
        public  string Key { get; set; }

        [Column("status")]
        public CacheStatuses CacheStatus { get; set; }

        [MaxLength(2000)]
        [Column("json")]
        public string Json { get; set; }

        [Column("created_on")]
        public DateTime CreatedOn { get; set; }

        [Column("cached_on")]
        public DateTime CachedOn { get; set; }

        [Column("modified_on")]
        public DateTime ModifiedOn { get; set; }


    }
}