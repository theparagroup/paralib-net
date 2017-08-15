using System;
using System.Collections.Generic;

namespace com.paralib.Dal.Metadata
{
    public class Relationship
    {
        public string Name { get; set; }
        public string OnTable { get; set; }
        public string OtherTable { get; set; }
        public List<ColumnPair> Columns { get; set; }
    }
}
