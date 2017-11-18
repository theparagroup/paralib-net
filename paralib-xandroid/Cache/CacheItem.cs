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

        [MaxLength(2000)]
        [Column("json")]
        public string Json { get; set; }

        [Column("dirty")]
        public bool Dirty { get; set; }

        [Column("created_on")]
        public DateTime CreatedOn { get; set; }

        [Column("saved_on")]
        public DateTime SavedOn { get; set; }

        [Column("dirtied_on")]
        public DateTime DirtiedOn { get; set; }

        [Column("cleaned_on")]
        public DateTime CleanedOn { get; set; }

    }
}