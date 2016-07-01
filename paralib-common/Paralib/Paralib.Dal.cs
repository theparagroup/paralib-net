using System;
using com.paralib.Ado;
using com.paralib.Configuration;

namespace com.paralib
{
    public partial class Paralib
    {

        public static class Dal
        {
            public const string ColumnMetadataTable = "paralib_column_metadata";

            public static DatabaseDictionary Databases { get; internal set; }

            public static Database Database
            {
                get
                {
                    return Databases?[null];
                }
            }

        }

    }
}
