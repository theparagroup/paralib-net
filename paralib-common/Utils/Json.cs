using System;

namespace com.paralib.Utils
{
    public static class Json
    {
        public static string Serialize(object value)
        {
            var settings = new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore };
            return Serialize(value, settings);
        }

        public static string Serialize(object value, Newtonsoft.Json.JsonSerializerSettings settings)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value, settings);
        }

        public static T DeSerialize<T>(string value)
        {
            return DeSerialize<T>(value, null);
        }

        public static T DeSerialize<T>(string value, Newtonsoft.Json.JsonSerializerSettings settings)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value, settings);
        }

        public static object DeSerialize(string value)
        {
            return DeSerialize(value, null);
        }

        public static object DeSerialize(string value, Newtonsoft.Json.JsonSerializerSettings settings)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(value, settings);
        }

    }
}
