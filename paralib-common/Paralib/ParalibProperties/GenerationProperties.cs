using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.ParalibProperties
{
    public class GenerationProperties
    {
        public bool Enabled { get; internal set; }
        public string Path { get; internal set; }
        public string Namespace { get; internal set; }
        public bool Replace { get; internal set; }

        public string Implements { get; internal set; }
        public string Ctor { get; internal set; }


    }
}
