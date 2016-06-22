using System;
using System.Collections.Generic;
using NET = System.ComponentModel.DataAnnotations;

namespace com.paralib.DataAnnotations
{
    public class ValidationError
    {
        public string MemberName { get; internal set; }
        public string ErrorMessage { get; internal set; }
    }
}
