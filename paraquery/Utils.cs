using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    public class Utils
    {
        internal static Exception Exception(string message)
        {
            //centralized exception point
            return new System.Exception(message);
        }

        public static Dictionary<string, string> ToDictionary(object attributes)
        {
            //the idea here is to accept
            //  a string, which is assumed to a list of classes (shorthand)
            //  an (anonymous) object
            //      containing either string properties
            //      or a nested (anonymous) object
            //
            //  the string properties will be used for name value pairs
            //  string properties on additional objects will be used as "defaults" (not added if they already exist)
            //      unless the property is "class" then it will be merged


            if (attributes != null)
            {
                var atts = new Dictionary<string, string>();

                if (attributes is string)
                {
                    //short hand for classes
                    atts.Add("class", (string)attributes);
                }
                else
                {
                    //some kind of object (probably anoymous)
                    var t = attributes.GetType();

                    foreach (var pi in t.GetProperties())
                    {
                        object value = pi.GetValue(attributes);

                        if (value != null)
                        {
                            //always lower case!
                            string name = pi.Name.ToLower();

                            if (pi.PropertyType == typeof(string))
                            {
                                //this is name value pair
                                atts.Add(name, (string)value);
                            }
                            else
                            {
                                //some other kind of object, let make a dictionary
                                var additional = ToDictionary(value);

                                if (additional != null)
                                {
                                    foreach (var key in additional.Keys)
                                    {
                                        if (key == "class")
                                        {
                                            if (atts.ContainsKey(key))
                                            {
                                                //TODO this could be more sophisticated (parse and don't duplicate class names)
                                                atts[key] = $"{atts["class"]} {additional[key]}";
                                            }
                                        }

                                        //we don't replace an existing with an additional attribute!
                                        //note if "class" didn't exist above it will get added now
                                        if (!atts.ContainsKey(key))
                                        {
                                            atts.Add(key, additional[key]);
                                        }

                                    }

                                }
                            }

                        }

                    }
                }

                return atts;
            }
            else
            {
                return null;
            }
        }

        public static string Parameters(string[] parameters)
        {
            string parameterList = "";

            if (parameters != null)
            {
                foreach (string parameter in parameters)
                {
                    if (parameterList.Length > 0) parameterList += ", ";
                    parameterList += parameter;
                }
            }

            return parameterList;
        }

        public static string Parameters(object data)
        {
            string parameter = "";

            if (data != null)
            {
                parameter = $"{Json.Serialize(data, true)}, ";
            }

            return parameter;

        }

        public static class Json
        {
            public static string Serialize(object value, bool ignoreLoops = true)
            {
                return com.paralib.Utils.Json.Serialize(value, ignoreLoops);
            }
        }

        public class Resources
        {
            public static string ReadManifestResouceString(System.Reflection.Assembly assembly, string name)
            {
                return com.paralib.Utils.Resources.ReadManifestResouceString(assembly, name);
            }

        }
    }
}
