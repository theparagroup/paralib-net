using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Mapping
{
    /*

        We implement this interface on our AttributeCollection objects when we want to not create
        backing stores for properties if they are not needed (e.g., not touched in fluent calls).

        We also need to mark up any properties with the [DynamicValue] attribute.

        The HasValue() method is called in AttributeBuilder for marked properties to determine if 
        there is a backing value before using reflection to get the actual value. 

        If AttributeBuilder just read the value directly, it would always create the backing store.

        See GlobalAttributes and the Style property for example usage.

    */

    public interface IDynamicValueContainer
    {
        bool HasValue(string propertyName);
    }

}
