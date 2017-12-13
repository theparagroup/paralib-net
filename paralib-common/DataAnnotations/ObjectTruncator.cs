using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using NET = System.ComponentModel.DataAnnotations;

namespace com.paralib.DataAnnotations
{
    public static class ObjectTruncator
    {
        public enum TruncateTargets
        {
            Properties,
            Fields,
            Both
        }

        private static string Truncate(string value, int maxLength)
        {
            return value?.Substring(0, Math.Min(value.Length, maxLength));
        }

        public static void Truncate(object obj, TruncateTargets truncateTarget)
        {
            bool truncateProperties = (truncateTarget == TruncateTargets.Properties) || (truncateTarget == TruncateTargets.Both);
            bool truncateFields = (truncateTarget == TruncateTargets.Fields) || (truncateTarget == TruncateTargets.Both);


            if (obj != null)
            {
                var objectType = obj.GetType();
                var metadataType = objectType;

                var mta = objectType.GetCustomAttribute<NET.MetadataTypeAttribute>();

                if (mta != null)
                {
                    metadataType = mta.MetadataClassType;
                }

                var mmis = metadataType.GetMembers().Where(mi => Attribute.IsDefined(mi, typeof(ParaTypeAttribute), true));

                foreach (var mmi in mmis)
                {
                    var pta = mmi.GetCustomAttribute<ParaTypeAttribute>();

                    //support for parastring (inherits?), [StringLength]
                    if (pta.ParaType is Data.StringType)
                    {
                        int maxLength = ((Data.StringType)pta.ParaType).MaximumLength;

                        //match the metadata member with the object member 
                        //(this code should work for properties and fields which can't be overloaded)
                        var omis = objectType.GetMember(mmi.Name);

                        foreach (var omi in omis)
                        {
                            if (truncateProperties && omi.MemberType == MemberTypes.Property)
                            {
                                var pi = (PropertyInfo)omi;
                                if (pi.PropertyType == typeof(string))
                                {
                                    var value = (string)pi.GetValue(obj);
                                    value = Truncate(value, maxLength);
                                    pi.SetValue(obj, value);
                                }

                            }
                            else if (truncateFields && omi.MemberType == MemberTypes.Field)
                            {
                                var fi = (FieldInfo)omi;
                                if (fi.FieldType == typeof(string))
                                {
                                    var value = (string)fi.GetValue(obj);
                                    value = Truncate(value, maxLength);
                                    fi.SetValue(obj, value);
                                }

                            }

                        }

                    }
                }

            }
        }
    }
}
