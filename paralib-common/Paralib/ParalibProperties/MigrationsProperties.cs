using System;

namespace com.paralib.ParalibProperties
{
    public class MigrationsProperties
    {
        public bool Devmode { get; internal set; }
        public string Database { get; internal set; }
        public string Commands { get; internal set; }

        public CodegenProperties Codegen { get; } = new CodegenProperties();

    }
}
