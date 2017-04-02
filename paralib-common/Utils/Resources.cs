using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Utils
{
    public class Resources
    {
        public static Stream GetManifestResouceStream(string name)
        {
            return GetManifestResouceStream(Assembly.GetCallingAssembly(),name);
        }

        public static Stream GetManifestResouceStream(Assembly assembly, string name)
        {
            //name => namespace.file.extension
            //(make sure marked as 'Embedded Resource')
            return assembly.GetManifestResourceStream(name);
        }

        public static string ReadManifestResouceString(string name)
        {
            return ReadManifestResouceString(Assembly.GetCallingAssembly(), name);
        }

        public static string ReadManifestResouceString(Assembly assembly, string name)
        {
            return Streams.ReadStream(assembly.GetManifestResourceStream(name));
        }
    }
}
