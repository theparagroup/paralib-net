using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;


namespace com.paraquery.Html
{
    public abstract class StyleBase : IComplexValue
    {
        public object Properties { set; get; }

       

        protected virtual string GetProperties()
        {
            var properties = PropertyDictionary.Properties(this);
            return properties.ToDeclaration();
        }

        public string ToValue()
        {
            return GetProperties();
        }

    }
}
