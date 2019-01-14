using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml.Packages
{
    /*



    */

    public abstract class Package
    {

        public virtual List<Dependency> Dependencies
        {
            get
            {
                return null;
            }
        }


    }
}
