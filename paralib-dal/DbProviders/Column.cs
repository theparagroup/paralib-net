using System;

namespace com.paralib.Dal.DbProviders
{
    public class Column
    {
        public string Name { get; set; }
        public int Ordinal { get; set; }
        public string DbType { get; set; }
        public Type ClrType { get; set; }
        public string ParaType { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsForeign { get; set; }
        public bool IsNullable { get; set; }
        public int? Length { get; set; }
        public byte? Precision { get; set; }
        public int? Scale { get; set; }
    }
}
