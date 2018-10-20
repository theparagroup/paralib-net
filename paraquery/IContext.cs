using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    public interface IContext
    {
        IServer Server { get; }
        IRequest Request { get; }
        IWriter Writer { get; }

    }
}
