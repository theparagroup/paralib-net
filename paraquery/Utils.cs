using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Utils;

namespace com.paraquery
{
    public class Utils
    {
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
    }
}
