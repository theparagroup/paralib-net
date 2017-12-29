using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Migrations
{
    public class ExtendedProperties
    {
        public const string ParatypeAttribute = nameof(ParatypeAttribute);

        public string PrincipalNavigationProperty { get; set; }
        public string DependentNavigationProperty { get; set; }

    }
}
