using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Mvc.Tables
{
    public class TableDefinition<T> : TableDefinition
    {
        public TableDefinition(IEnumerable<T> source) : base(source, typeof(T))
        {
        }
    }
}
