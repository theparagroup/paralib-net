using System;

namespace com.paralib.Data
{
    public class TimeType : ParaType
    {
        public TimeType(string name) : base(name, typeof(TimeSpan)) { }
    }
}
