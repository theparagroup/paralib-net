using System;

namespace com.paralib.Data
{
    public abstract class ParaType
    {
        public string Name { get; private set; }
        public Type Type { get; private set; }

        public ParaType(string name, Type type)
        {
            Name = name;
            Type = type;
        }


        public abstract string Validate(string displayName, object value);



    }

    

}
