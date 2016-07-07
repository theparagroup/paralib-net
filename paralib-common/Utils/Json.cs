using System;

namespace com.paralib.Utils
{
    public static class Json
    {

        public static string Serialize(object value, bool ignoreLoops = true)
        {
            new Newtonsoft.Json.JsonSerializerSettings();

            var settings = new Newtonsoft.Json.JsonSerializerSettings();

            if (ignoreLoops)
            {
                settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }

        public static T DeSerialize<T>(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }

        public static object DeSerialize(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(value);
        }

    }
}
