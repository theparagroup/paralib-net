using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Dal.Utils
{
    public class CSharpTypes
    {
        /*

            object : System.Object
            string : System.String
            bool : System.Boolean
            byte : System.Byte
            char : System.Char
            decimal : System.Decimal
            double : System.Double
            short : System.Int16
            int : System.Int32
            long : System.Int64
            sbyte : System.SByte
            float : System.Single
            ushort : System.UInt16
            uint : System.UInt32
            ulong : System.UInt64
            void : System.Void
        */

        public static string GetKeyword(Type type)
        {
            string keyword= GetKeyword(type.Name);
            return keyword ?? type.Name;
        }

        public static string GetKeyword(string type)
        {
            string name = type;
            string keyword = null;
            bool isArray = false;

            if (type.EndsWith("[]"))
            {
                isArray = true;
                name = type.Substring(0, type.Length - 2);
            }

            switch (name)
            {
                case "Object": keyword = "object"; break;
                case "String": keyword = "string"; break;
                case "Boolean": keyword = "bool"; break;
                case "Byte": keyword = "byte"; break;
                case "Char": keyword = "char"; break;
                case "Decimal": keyword = "decimal"; break;
                case "Double": keyword = "double"; break;
                case "Int16": keyword = "short"; break;
                case "Int32": keyword = "int"; break;
                case "Int64": keyword = "long"; break;
                case "SByte": keyword = "sbyte"; break;
                case "Single": keyword = "float"; break;
                case "UInt16": keyword = "ushort"; break;
                case "UInt32": keyword = "uint"; break;
                case "UInt64": keyword = "ulong"; break;
                case "Void": keyword = "void"; break;

                default: return null;
            }

            return $"{keyword}{(isArray?"[]":"")}";

        }

        public static bool HasNullable(Type type)
        {
            return HasNullable(type.Name);
        }

        public static bool HasNullable(string type)
        {
            if (type.EndsWith("[]"))
            {
                return false;
            }

            switch (type)
            {
                case "Boolean":
                case "Byte":
                case "Char":
                case "DateTime":
                case "Decimal":
                case "Double":
                case "Int16":
                case "Int32":
                case "Int64":
                case "SByte":
                case "Single":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                    return true;

                default: return false;
            }


        }


    }
}
