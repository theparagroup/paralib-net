using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Mapping
{
    /*
        This dictionary is more forgiving (doesn't throw) if the key doesn't exist.
    */

    public class AutoDictionary<K,V> : Dictionary<K,V>
    {
        public new V this[K index]
        {
            set
            {
                if (ContainsKey(index))
                {
                    base[index] = value;
                }
                else
                {
                    Add(index, value);
                }
            }
            get
            {
                if (ContainsKey(index))
                {
                    return base[index];
                }
                else
                {
                    return default(V);
                }
            }
        }

        public new void Add(K name, V value)
        {
            if (ContainsKey(name))
            {
                base[name] = value;
            }
            else
            {
                base.Add(name, value);
            }
        }


    }
}
