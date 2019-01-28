using System;

namespace com.paralib.Utils
{
    public static class Json
    {

        public static string Serialize(object value, bool ignoreLoops = true, Newtonsoft.Json.JsonSerializerSettings settings = null)
        {

            if (settings==null)
            {
                settings = new Newtonsoft.Json.JsonSerializerSettings();
            }

            if (ignoreLoops)
            {
                settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }

        public static T DeSerialize<T>(string value, Newtonsoft.Json.JsonSerializerSettings settings = null)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value, settings);
        }

        public static object DeSerialize(string value, Newtonsoft.Json.JsonSerializerSettings settings = null)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(value, settings);
        }

    }
}
