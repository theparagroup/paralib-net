using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Tags
{
    /*
    
        This is a marker interface for properties that shouldn't have an actual
        "name", but rather just render the value as-is.

        This is useful particularly in Style where we want nested property
        objects but they shouldn't have a name (see Background).            
        
         
    */

    public interface INestedAttribute
    {
    }
}
