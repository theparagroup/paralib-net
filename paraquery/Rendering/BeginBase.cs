using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{

    /*

        Provides "Begin semantics".

        The instatiator should call Begin(), or          

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

        protected virtual void OnPreContent()
        {

        }

        protected override void OnPreEnd()
        {
            OnPostContent();
            base.OnPreEnd();
        }

        protected virtual void OnPostContent()
        {

        }

    }
}
