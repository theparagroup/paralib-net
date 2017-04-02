using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html
{
    public class Attribute
    {
        public string Name { set; get; }
        public string Value { set; get; }

        public Attribute(string name,string value)
        {
            Name = name;
            Value = value;
        }

        public static Dictionary<string, string> ToDictionary(object attributes)
        {
            if (attributes != null)
            {
                var atts = new Dictionary<string, string>();

                var t = attributes.GetType();

                foreach (var pi in t.GetProperties())
                {
                    object value = pi.GetValue(attributes);

                    if (value != null)
                    {

                        if (pi.PropertyType == typeof(string))
                        {
                            atts.Add(pi.Name.ToLower(), (string)value);
                        }
                        else if (pi.PropertyType == typeof(Attribute))
                        {
                            atts.Add(((Attribute)value).Name.ToLower(), ((Attribute)value).Value);
                        }
                        else
                        {
                            var additional = ToDictionary(value);

                            if (additional != null)
                            {
                                foreach (var key in additional.Keys)
                                {
                                    //we don't replace an existing with an additional attribute!
                                    if (!atts.ContainsKey(key))
                                    {
                                        atts.Add(key.ToLower(), additional[key]);
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
    }
}
