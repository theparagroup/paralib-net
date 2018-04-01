using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Blocks
{
    public abstract class Block : SimpleBlock
    {
        private bool _format;

        protected Block(IContext context, bool format=true):base(context)
        {
            _format = format;
        }


        protected virtual void Begin()
        {
            OnPreBegin();
            OnBegin();
            OnPostBegin();
        }

        protected virtual void OnPreBegin()
        {
        }

        protected abstract void OnBegin();

        protected virtual void OnPostBegin()
        {
            if (_format)
            {
               _response.NewLine();
               _response.Indent();
            }
        }

        protected override void OnPreEnd()
        {
            if (_format)
            {
                //this should be conditional on if newline was called last
                if (!_response.IsNewLine)
                {
                    _response.Tab();
                    _response.Write("<!-- newlined -->",false);
                    _response.NewLine();
                }

                _response.Dedent();
            }
        }

        protected override void OnPostEnd()
        {
            if (_format)
            {
                _response.NewLine();
            }
        }


    }
}
