using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen
{
    public class Utils
    {
        /*

            Currently trying to minimize direct coupling to other libraries or 
            the paralib-common assembly.

            At some point we should feel more comfortable coupling to paralib-common, 
            but not yet. Perhaps when config and logging have been refactored.

        */

        public static Exception Exception(string message)
        {
            //centralized exception point
            return new System.Exception(message);
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
                parameter = $"{Json.Serialize(data)}, ";
            }

            return parameter;

        }

        public static string Template(string name)
        {
            //TODO razor option
            //TODO error checking and caching
            return Resources.ReadManifestResouceString(System.Reflection.Assembly.GetCallingAssembly(), name);
        }

        public static class Json
        {
            public static string Serialize(object value)
            {
                return com.paralib.Utils.Json.Serialize(value);
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
