using System;

namespace com.paralib.Data
{
    public class GuidType : ParaType
    {
        public GuidType(string name) : base(name, typeof(Guid)) { }
    }
}
