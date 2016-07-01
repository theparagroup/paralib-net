using System;

namespace com.paralib.ParalibProperties
{
    public class CodegenProperties
    {
        public string Path { get; internal set; }
        public string Namespace { get; internal set; }
        public string[] Skip { get; internal set; }
        public string Convention { get; internal set; }

        public ModelGenerationProperties Model { get; } = new ModelGenerationProperties();
        public GenerationProperties Logic { get; } = new GenerationProperties();
        public GenerationProperties Metadata { get; } = new GenerationProperties();
        public GenerationProperties Ef { get; } = new GenerationProperties();
        public GenerationProperties Nh { get; } = new GenerationProperties();

    }
}
