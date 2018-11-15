using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Fluent
{
    public interface IHasContext<C> where C: Context
    {
        void SetContext(C context);
    }
}
