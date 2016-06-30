using System;
using System.Data;

namespace com.paralib.Dal
{
    public static class ExtensionMethods
    {
        public static T GetValue<T>(this IDataReader reader, string name)
        {
            object value = reader[name];

            if (value == System.DBNull.Value) return default(T);

            return (T)value;
        }

    }
}
