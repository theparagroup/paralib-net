using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Migrations.CodeGen
{
    public interface IClassWriter
    {
        void Start(string className);
        void Write(string text);
        void WriteLine(string text=null);
        void End();
    }
}
