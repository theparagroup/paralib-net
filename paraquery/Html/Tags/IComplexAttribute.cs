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
    
        Note: this must be implmented explicitly or you will recurse
    */

    public interface IComplexAttribute
    {
        string Value { get; }
    }
}
