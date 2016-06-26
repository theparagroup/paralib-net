using System;
using com.paralib.Ado;

namespace com.paralib.Configuration
{
    public class ConnectionStrings
    {
        public string this[string name]
        {
            get
            {
                return ConfigurationManager.GetConnectionString(name);
            }
        }
    }
}
