using System;

namespace com.paralib.Dal.DbProviders
{
    public class Relationship
    {
        public string Name { get; set; }
        public string OnTable { get; set; }
        public string OnColumn { get; set; }
        public string OtherTable { get; set; }
        public string OtherColumn { get; set; }
    }
}
