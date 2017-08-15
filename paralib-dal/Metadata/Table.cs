using System;
using System.Collections.Generic;

namespace com.paralib.Dal.Metadata
{
    public class Table
    {
        public string Name { get; set; }
        public Dictionary<string, Column> Columns { get; set; }
        public List<Column> PrimaryKeys { get; set; }
        public Dictionary<string, Relationship> ForeignKeys { get; set; }
        public Dictionary<string, Relationship> References { get; set; }
    }
}
