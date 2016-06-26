using System;
using System.Collections.Generic;
using System.Collections;
using com.paralib.Ado;

namespace com.paralib.Configuration
{
    public class DatabaseDictionary : IEnumerable<Database>
    {
        private Dictionary<string, Database> _databases;
        public string Default { get; private set; }
        public bool Sync { get; private set; }

        internal DatabaseDictionary(Dictionary<string, Database> databases, string defaultDatabase, bool syncDatabases)
        {
            _databases = databases;
            Default = defaultDatabase;
            Sync = syncDatabases;
        }

        public Ado.Database this[string name]
        {
            get
            {
                return _databases[name ?? Default];
            }
        }

        public IEnumerator<Database> GetEnumerator()
        {
            return _databases.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _databases.Values.GetEnumerator();
        }
    }

}
