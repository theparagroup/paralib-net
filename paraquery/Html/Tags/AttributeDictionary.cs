using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Tags
{
    /*

         Contains a set of name value pairs that represents attributes on a tag.

         Null values indicate true boolean values, and should be rendered according to the MinimizeBooleans option.


        Attribute Names
            http://www.w3.org/TR/2000/REC-xml-20001006#NT-Name

            First character         => letter | _ | :
            Additional characters   => letter | digit | underscore | colon | period | dash, or a “CombiningChar” or “Extender” character, which I believe allows Unicode attributes names.


     */

    public class AttributeDictionary : NameValuePairs
    {
       

    }


}




