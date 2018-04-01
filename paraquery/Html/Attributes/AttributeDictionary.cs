using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace com.paraquery.Html.Attributes
{
    public class AttributeDictionary:Dictionary<string,string>
    {
        public static AttributeDictionary Build(string @class, object attributes=null)
        {
            return Build(new {@class=@class, attributes=attributes });
        }

        public static AttributeDictionary Build(object attributes)
        {
            //examples
            // "class1 class2"
            // new { id="div1", @class="class1 class2"}
            // new { id="div1", defaults= new { @class="class1 class2"}}


            //the idea here is to accept
            //  a string, which is assumed to a list of classes (shorthand)
            //  a dictionary of name/value pairs
            //  an (anonymous) object
            //      containing either string properties
            //      or a nested (anonymous) object
            //
            //  the string properties will be used for name value pairs
            //  string properties on additional objects will be used as "defaults" (not added if they already exist)
            //      unless the property is "class" then it will be merged
            //      TODO and perhaps style?


            if (attributes != null)
            {
                var atts = new AttributeDictionary();

                if (attributes is string)
                {
                    //short hand for classes
                    atts.Add("class", (string)attributes);
                }
                else if (attributes is Dictionary<string,string>)
                {
                    //TODO this could be handy. will it match our AttributeDictionary? should
                    throw new NotImplementedException();
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
                            string name = pi.Name;

                            if (name.ToLower()=="style")
                            {
                                //TODO go through the object and build dictionary

                                //mixed case should be replaced with hyphens (but not first letter)
                                name = Regex.Replace(name, @"([a-z])([A-Z])", "$1-$2").ToLower();
                            }
                            else
                            {
                                name = name.ToLower();
                            }



                            if (pi.PropertyType == typeof(string))
                            {
                                //this is name value pair
                                atts.Add(name, (string)value);
                            }
                            else if (pi.PropertyType == typeof(int?))
                            {
                                int? i = (int?)value;
                                if (i.HasValue)
                                {
                                    //this is name value pair
                                    atts.Add(name, i.ToString());
                                }
                            }
                            else if (pi.PropertyType == typeof(bool))
                            {
                                //this is name value pair
                                atts.Add(name, null);
                            }
                            else if (pi.PropertyType.IsEnum)
                            {
                                //this is name value pair
                                atts.Add(name, value.ToString().ToLower());
                            }
                            else
                            {
                                //some other kind of object, let make a dictionary
                                var additional = Build(value);

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


    }
}
