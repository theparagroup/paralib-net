using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    internal class Utils
    {
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
