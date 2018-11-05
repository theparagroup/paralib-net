using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    /*
        This dictionary is more forgiving (doesn't throw) if the key doesn't exist.
    */

    public class NameValuePairs : Dictionary<string, string>
    {
        public new string this[string index]
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
                    return null;
                }
            }
        }

        public new void Add(string name, string value)
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
