using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Migrations.CodeGen
{
    public class FileOptions
    {
        public string Path { get; set; }
        public string SubDirectory { get; set; }
        public bool Replace { get; set; }
    }
}
