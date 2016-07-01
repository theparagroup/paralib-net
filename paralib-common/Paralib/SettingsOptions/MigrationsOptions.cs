using System;

namespace com.paralib.SettingsOptions
{
    public class MigrationsOptions
    {
        public bool Devmode { get; set; }
        public string Database { get; set; }
        public string Commands { get; set; }

        public CodegenOptions Codegen { get; set; } = new CodegenOptions();
    }
}
