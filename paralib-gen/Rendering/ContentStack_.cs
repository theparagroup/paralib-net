using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Rendering
{
    public partial class ContentStack
    {

        public virtual IContent Top
        {
            get
            {
                return Peek();
            }
        }

        public virtual void Open(IContent content)
        {
            if (content.ContentState == ContentStates.New)
            {
                Push(content);
            }
            else
            {
                throw new InvalidOperationException("Renderer is already open");
            }
        }

        public virtual void Close()
        {
            Pop();
        }

        public virtual void CloseUp()
        {
            PopToBlock(false);
        }

        public virtual void CloseBlock()
        {
            PopToBlock(true);
        }

        public virtual void CloseAll()
        {
            PopAll();
        }

        public virtual void Close(IContent content)
        {
            Pop(content);
        }

        public virtual void Close(Func<IContent, bool> func)
        {
            Pop(func);
        }
      
    }

}
