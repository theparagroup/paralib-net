using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml.Mvc
{
    public interface IHasModel<M>
    {
        void SetModel(M model);
    }
}
