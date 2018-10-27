using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html.Tags
{
    public interface ITag
    {
        string TagName { get; }
        string Id { get; }   
    }
}
