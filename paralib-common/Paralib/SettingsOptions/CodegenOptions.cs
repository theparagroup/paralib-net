using System;

namespace com.paralib.SettingsOptions
{
    public class CodegenOptions
    {
        public string Path { get; set; }
        public string Namespace { get; set; }
        public string[] Skip { get; set; }
        public string Convention { get; set; }

        public ModelOptions Model { get; set; } = new ModelOptions();
        public LogicOptions Logic { get; set; } = new LogicOptions();
        public MetadataOptions Metadata { get; set; } = new MetadataOptions();
        public EfOptions Ef { get; set; } = new EfOptions();
        public NhOptions Nh { get; set; } = new NhOptions();
    }
}
