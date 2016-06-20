using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Logging
{
    public class Log
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public LogTypes LogType { get; set; }
        public string Pattern { get; set; }
        public string Capture { get; set; }
        public string Connection { get; set; }
        public string Path { get; set; }
        public string ConnectionType { get; set; }
        public string LoggerType { get; set; }
    }
}
