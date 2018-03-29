using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Core
{
    public interface IContext
    {
        IServer Server { get; }
        IRequest Request { get; }
        IResponse Response { get; }

        
    }
}
