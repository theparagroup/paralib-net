using System;
using System.Text.RegularExpressions;

namespace com.paralib.Data
{
    public class KeyType : ParaType
    {

        public KeyType(string name) : base(name, typeof(int)) { }

        public override string Validate(string displayName, object value)
        {
            return null;
        }


    }



}
