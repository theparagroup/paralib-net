using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{

    /*

       Provides "Begin semantics". It is up to the instatiator or subclass to call Begin().

   */

    public abstract class BeginBase : EndBase
    {

        public void Begin()
        {
            _begin();
        }

        protected virtual void _begin()
        {
            OnPreBegin();
            OnBegin();
            OnPostBegin();
        }

        protected virtual void OnPreBegin() { }

        protected abstract void OnBegin();

        protected virtual void OnPostBegin() { }


    }
}
