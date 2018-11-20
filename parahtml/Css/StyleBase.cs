using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Mapping;
using com.paralib.Gen;
using com.parahtml.Core;

namespace com.parahtml.Css
{
    public abstract class StyleBase : IDynamicValueContainer
    {
        protected AutoDictionary<string, object> _dynamicValues = new AutoDictionary<string, object>();

        //protected void _set<V>(string propertyName, V value)
        //{
        //    _dynamicValues[propertyName] = value;
        //}

        protected V _get<V>(string propertyName) where V: new()
        {
            if (!_dynamicValues.ContainsKey(propertyName))
            {
                _dynamicValues[propertyName]=new V();
            }

            return (V)_dynamicValues[propertyName];
        }

        bool IDynamicValueContainer.HasValue(string propertyName)
        {
            return _dynamicValues.ContainsKey(propertyName);
        }
    }
}
