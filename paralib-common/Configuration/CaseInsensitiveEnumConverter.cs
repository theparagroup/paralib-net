using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace com.paralib.Configuration
{
    public class CaseInsensitiveEnum<T> : ConfigurationConverterBase
    {
        public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
        {
            return Enum.Parse(typeof(T), (string)data, true);
        }
    }
}
