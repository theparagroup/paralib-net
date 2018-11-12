using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Mapping
{
    /*
    
        This is a marker interface for member properties that shouldn't generate
        a name value pair for rendering. Rather, just the value should be rendered.

        This is useful particularly in Style where we want nested property
        objects but they shouldn't have a name (see Background).            
        
         
    */

    public interface IValueContainer
    {
    }
}
