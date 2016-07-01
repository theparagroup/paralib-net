using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Migrations.CodeGen
{
    public class ClassOptions
    {
        public string Namespace { get; set; }
        public string SubNamespace { get; set; }
        public string Implements { get; set; }
        public string Ctor { get; set; }
        public string Parameter { get; set; }
    }
}
