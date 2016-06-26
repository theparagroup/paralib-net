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

        public Database this[string name]
        {
            get
            {
                string key = name ?? Default;

                if (key!=null && _databases.ContainsKey(key))
                {
                    return _databases[key];
                }
                else
                {
                    if (name != null)
                    {
                        throw new ParalibException($"Database [{name}] does not exist.");
                    }
                    else
                    {
                        throw new ParalibException($"There is no default database configured.");
                    }

                }

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
