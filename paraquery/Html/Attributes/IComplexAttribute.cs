using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Attributes
{
    //note this must be implmented explicitly (see attributedictionary)

    public interface IComplexAttribute
    {
        string Value { get; }
    }
}
