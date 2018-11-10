using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using com.paraquery.Html.Tags.Attributes;

namespace com.paraquery.Html.Tags
{
    public static class AttributeBuilder
    {
       

        private static void BuildAttributeDictionary(AttributeDictionary dictionary, object attributes, Type type, bool caseSensive)
        {
            if (attributes != null)
            {
                //this includes inherited properties (see missing DeclaredOnly flag)
                //this also excludes interface properties implemented explictly
                BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

                //enumerate properties
                var pis = type.GetProperties(bindingFlags);

                foreach (var pi in pis)
                {
                    object v = null; //raw value
                    bool getValue = true;

                    //handle dynamic values
                    if (pi.GetCustomAttribute<DynamicValueAttribute>() != null)
                    {
                        if (typeof(IDynamicValueContainer).IsAssignableFrom(type))
                        {
                            getValue = ((IDynamicValueContainer)attributes).HasValue(pi.Name);
                        }
                    }

                    if (getValue)
                    {
                        v = pi.GetValue(attributes);
                    }

                    if (v != null)
                    {

                        //if you have two properties with same name but different case, we will enumerate both
                        //but the order is undefined. the last one will replace the first one in the dictionary
                        //if we are in case-insensitive mode. otherwise both will be added.

                        string name = null;
                        string value = null; //processed value

                        //see if the property has explicit specifications
                        var specifics = pi.GetCustomAttribute<AttributeAttribute>();
                        if (specifics != null)
                        {
                            name = specifics.Name;
                        }
                        else if(typeof(INestedAttribute).IsAssignableFrom(pi.PropertyType))
                        {
                            //if this is a "nested attribute" (meaning it doesn't render as a name/value
                            //pair but as just the value), we prefix the name with a bang, which is not
                            //a valid HTML attribute name. this is mainly used by Style to organize
                            //properties into nested classes (see Background). So if you are using this
                            //interface, you need to do something with these bangs.
                            name = $"!{pi.Name}";
                        }
                        else
                        {
                            //generally, we're always in case-insensitive mode, and by default attribute
                            //names are lower case. however, in certain cases such as Style, it is useful
                            //to build a dictionary of name/values with mixed case
                            if (caseSensive)
                            {
                                name = pi.Name;
                            }
                            else
                            {
                                name = pi.Name.ToLower();
                            }
                        }

                        if (typeof(IComplexAttribute).IsAssignableFrom(pi.PropertyType))
                        {
                            //if your implementation of value wants to call this Build method recursively on iteself (see Style),
                            //it's very important that IComplexAttribute is implemented explicitly or you will recurse endlessly.
                            //this is because the first time through, the class owning the property is enumerating it's properties,
                            //and the property will be enumerated. however, in the recursive call, the property class is enumerating,
                            //and we don't want to call Value again. the way we call GetProperties(), excplicity implemented interface
                            //properties are not enumerated.
                            value = ((IComplexAttribute)v).Value;
                        }
                        else if (pi.PropertyType == typeof(string))
                        {
                            value = (string)v;
                        }
                        else if (pi.PropertyType == typeof(int) || pi.PropertyType == typeof(int?))
                        {
                            value = v.ToString();
                        }
                        else if (pi.PropertyType == typeof(float) || pi.PropertyType == typeof(float?))
                        {
                            value = v.ToString();
                        }
                        else if (pi.PropertyType == typeof(bool) || pi.PropertyType == typeof(bool?))
                        {
                            //for booleans, we only add the key if true.
                            //attribute renderers such as tag should respect the "MinimizeBooleans" option

                            //this cast is okay since we know v isn't null (if nullable)
                            if (!(bool)v)
                            {
                                continue;
                            }

                            //if the attribute has a fixed value (xml:space="preserve") then use it instead of null
                            value = specifics?.Value;
                        }
                        else if ((pi.PropertyType.IsEnum) || (Nullable.GetUnderlyingType(pi.PropertyType)?.IsEnum ?? false))
                        {
                            value = v.ToString().ToLower();
                        }
                        else
                        {
                            //process unknown types recursively, looking for anything we can add to the dicitonary
                            BuildAttributeDictionary(dictionary, v, caseSensive);
                            continue;
                        }

                        //again, at this point, "v" isn't null, but "value" may be, indicating a boolean true

                        //special rules
                        if (value != null)
                        {
                            //merge classes
                            if (name == "class" && dictionary.ContainsKey("class"))
                            {
                                //note Union() will throw if one of the arrays is null
                                //we know value isn't null, but if you have a property Class=true,
                                //the what's in the dictionary could be
                                string[] oldclasses = dictionary["class"]?.Split(' ') ?? new string[] { };
                                string[] newClasses = value.Split(' ');

                                //this removes duplicates
                                string[] classes = oldclasses.Union(newClasses).ToArray();

                                value = string.Join(" ", classes);
                            }
                        }

                        //add it to the dictionary 
                        dictionary.Add(name, value);

                    }
                }

            }
        }

        public static void BuildAttributeDictionary(AttributeDictionary dictionary, object attributes, bool caseSensive)
        {
            BuildAttributeDictionary(dictionary, attributes, attributes.GetType(), caseSensive);
        }


        //public static void BuildAttributeDictionary<T>(AttributeDictionary dictionary, T attributes, bool caseSensive)
        //{
        //    //this is the public method
        //    //typeof() is ever so slightly faster than TypeOf()
        //    BuildAttributeDictionary(dictionary, attributes, typeof(T), caseSensive);
        //}


        public static AttributeDictionary Attributes(object attributes)
        {
            AttributeDictionary dictionary = new AttributeDictionary();
            BuildAttributeDictionary(dictionary, attributes, false);
            return dictionary;
        }

        public static AttributeDictionary Attributes<T>(Action<T> attributes = null, object additional = null) where T : GlobalAttributes, new()
        {
            //let's keep it simple if there is nothing to do
            if (attributes != null || additional != null)
            {
                //create a new dictionary to hold the name value pairs
                AttributeDictionary dictionary = new AttributeDictionary();

                //execute init action and build, if exists
                if (attributes != null)
                {
                    T a = new T();

                    attributes(a);

                    BuildAttributeDictionary(dictionary, a, typeof(T), false);
                }

                //merge any additional anonymous object-based attributes
                if (additional != null)
                {
                    BuildAttributeDictionary(dictionary, additional, false);
                }

                //return null if there are no attributes
                if (dictionary.Count > 0)
                {
                    return dictionary;
                }
            }

            return null;
        }

    }
}


/*

   case sensitivity

   CSS is case insensitive

   however, the following things are case sensitive:

       ids
       class names
       urls
       font names/families?


   in html, tag names are case insensitive

   tag names in html5?

   in xhtml, tag names are case sensitive




   anything else?


*/

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
