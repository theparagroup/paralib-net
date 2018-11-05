using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{

    /*

        Provides "Begin semantics".

        The instatiator should call Begin(), or a derived class could call it
        in a constructor.
        
        You can call Begin() multiple times, but DoBegin() is only called once.
        
        Sequence looks like this:

            using(var b=new Thing())
            {
                b.Begin()

                //begin fires this sequence:
                    //OnPreBegin
                    //OnBegin
                    //OnPostBegin
                    //OnPreContent

                //anything done here would be considered "content"                

            }
                //the dispose fires this sequence:
                    //OnPostContent
                    //OnPreEnd
                    //OnEnd
                    //OnPostEnd


        After you call End(), we reset and you can call Begin() again.

        This is important if you want to re-use BeginBase objects.


   */

    public abstract class BeginBase : EndBase
    {
        internal bool _begun;

        public void Begin()
        {
            if (!_begun)
            {
                _begun = true;
                DoBegin();
            }
        }

        protected virtual void DoBegin()
        {
            OnPreBegin();
            OnBegin();
            OnPostBegin();
            OnPreContent();
        }

        protected virtual void OnPreBegin() { }

        protected abstract void OnBegin();

        protected virtual void OnPostBegin() { }

        protected virtual void OnPreContent() { }

        internal override void DoDispose()
        {
            if (_begun)
            {
                base.DoDispose();

                //reset
                _begun = false;
                _disposed = false;

            }
            else
            {
                throw new InvalidOperationException($"Can't End() Component '{GetType().Name}' without Begin()");
            }
        }

        protected override void OnPreEnd()
        {
            OnPostContent();
            base.OnPreEnd();
        }

        protected virtual void OnPostContent() { }

    }
}
