using System;

namespace com.paralib.Data
{
    public class DecimalType : ParaType
    {
        public DecimalType(string name) : base(name, typeof(decimal)) { }
    }
}
