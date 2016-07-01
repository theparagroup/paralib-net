using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.SettingsOptions
{
    public class GenerationOptions
    {
        public bool Enabled { get; set; }
        public string Path { get; set; }
        public string Namespace { get; set; }
        public bool Replace { get; set; }
        public string Implements { get; set; }
        public string Ctor { get; set; }
    }
}
