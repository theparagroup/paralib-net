using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using com.paraquery.Html.Attributes;

namespace com.paraquery.Html.Tags
{
    //examples
    // "class1 class2"
    // new { id="div1", @class="class1 class2"}
    // new { id="div1", defaults= new { @class="class1 class2"}}

    /*

        the idea here is to accept
          a string, which is assumed to a list of classes (shorthand)
          a dictionary of name/value pairs
          an (anonymous) object
              containing either string properties
              or a nested (anonymous) object

          the string, int, bool, enum, etc properties will be used for name value pairs
          properties on additional objects will be used as "defaults" (not added if they already exist)
              unless the property is "class" then it will be merged

          properties can be IComplexAttribute

          recursion and complex handling can be toggled

          property names are lower cased by default but can be converted from camel case to hypenated


    */

    /*
  public void Attributes(object attributes = null)
  {
      //namespace state should be over in context

      //TODO allow for namespaces
      //TODO allow for namespace-vars (class="[admin:foo-bar]" -> class="nsvar-foo-bar") 
      //TODO allow for name substitutions (clazz->class, classes->class)
      //TODO allow for symbol replacement (_,-) in names ( data_value -> data-value, data__value -> data_value)
      //TODO allow for variables? (class="{debug}" -> class="debug-verbose")
      //TODO allow for expansions? new { id="foo", style=new {background_color="green"}, margin=new { border_style="solid" } } -> id="foo" style="backgound-color:green;" margin="border-style:solid;"

      // id="foo-bar" -> id="foo-bar" (no change)
      // id="[foo-bar]" -> id="ns-foo-bar" (ns prefixing)
      // id="[blah:admin:foo-bar]" -> id="blah-admin-foo-bar" (if admin not an nsvar)
      // id="[:blah:admin:foo-bar]" -> id="ns-blah-admin-foo-bar" (with current ns)
      // id="[blah:admin:foo-bar]" -> id="blah-nsvar-foo-bar" (if admin is an nsvar)

      var atts = AttributeDictionary.Build(attributes);

      if (atts != null)
      {
          if (atts.ContainsKey("id"))
          {
              //TODO process namespaces for ids
              Attribute("id", atts["id"]);
          }

          if (atts.ContainsKey("class"))
          {
              //TODO process namespaces for classes
              Attribute("class", atts["class"]);
          }

          foreach (var key in atts.Keys)
          {
              if (key != "id" && key != "class")
              {
                  Attribute(key, atts[key]);
              }
          }

      }
  }
  */

    public class AttributeDictionary : Dictionary<string, string>
    {

        public new string this[string index]
        {
            set
            {
                if (ContainsKey(index))
                {
                    base[index]=value;
                }
                else
                {
                    Add(index, value);
                }
            }
            get
            {
                if (ContainsKey(index))
                {
                    return base[index];
                }
                else
                {
                    return null;
                }
            }
        }

        public static void BuildAttributeDictionary<T>(AttributeDictionary dictionary, T attributes) where T : GlobalAttributes
        {
            BuildAttributeDictionary(dictionary, attributes, typeof(T));
        }

        public static void BuildAttributeDictionary(AttributeDictionary dictionary, object attributes, Type type)
        {
            if (type == null)
            {
                type = attributes.GetType();
            }

            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

            //this includes inherited properties
            foreach (var pi in type.GetProperties(bindingFlags))
            {
                object v = pi.GetValue(attributes);

                if (v != null)
                {
                    //if you have two properties with same name but different case, results are undefined
                    string name = pi.Name.ToLower();
                    string value = null;

                    if (typeof(IComplexAttribute).IsAssignableFrom(pi.PropertyType))
                    {
                        //very important that this interface is implemented explicitly or you will recurse endlessly
                        value = ((IComplexAttribute)v).Value;
                    }
                    else if (pi.PropertyType == typeof(string))
                    {
                        value = (string)v;
                    }
                    else if (pi.PropertyType == typeof(int?))
                    {
                        int? i = (int?)v;
                        if (i.HasValue)
                        {
                            value = i.ToString();
                        }
                    }
                    else if (pi.PropertyType == typeof(bool?))
                    {
                        bool? b = (bool?)v;
                        if (b.HasValue)
                        {
                            value = b.ToString(); //tolower?
                        }
                    }
                    else if (Nullable.GetUnderlyingType(pi.PropertyType)?.IsEnum ?? false)
                    {
                        //this is name value pair
                        dictionary.Add(name, v.ToString().ToLower());
                    }
                    else
                    {
                        //ignore unknown types
                    }

                    if (value != null)
                    {
                        if (!dictionary.ContainsKey(name))
                        {
                            dictionary.Add(name, value);
                        }
                        else
                        {
                            dictionary[name] = value;
                        }
                    }

                }
            }

            //process "additional" attributes
            {
                var pi = type.GetProperty("Attributes", bindingFlags);

                if (pi == null)
                {
                    pi = type.GetProperty("attributes", bindingFlags);
                }

                if (pi != null)
                {
                    object v = pi.GetValue(attributes);
                    BuildAttributeDictionary(dictionary, v, null);
                }
            }
        }

        public static AttributeDictionary Attributes<T>(Action<T> init = null, object additional = null) where T : GlobalAttributes, new()
        {
            T attributes = new T();

            if (init != null)
            {
                init(attributes);
            }

            AttributeDictionary dictionary = new AttributeDictionary();

            BuildAttributeDictionary<T>(dictionary, attributes);

            if (additional != null)
            {
                BuildAttributeDictionary(dictionary, additional, null);
            }

            if (dictionary.Count > 0)
            {
                return dictionary;
            }
            else
            {
                return null;
            }

        }

    }
    

}
