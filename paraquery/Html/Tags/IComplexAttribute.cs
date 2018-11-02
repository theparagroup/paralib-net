using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Tags
{
    /*
        This is used to create attributes that can be processed by AttributeDictionary,
        but are more complicated and may have different naming conventions than a string or bool.

        Style is a good example.

        We have a HasValue property so that AttributeBuilder can determine if there is a value
        before using reflection to get the actual value. This allow you to have dynamically
        created backing stores for properties (for example, in a fluent interface) that will save 
        memory if not needed.

        If AttributeBuilder just read the value directly, it would always create the backing store.

    
        Note: this must be implmented explicitly or you will recurse
    */

    public interface IComplexAttribute
    {
        string Value { get; }
    }
}
