using System;
using System.Reflection;
using com.paralib.Logging;
using System.Collections.Generic;

namespace com.paralib
{
    public partial class Paralib
    {
        public static class Migrations
        {
            public static bool Devmode { get; internal set; }
            public static string Database { get; internal set; }
            public static string Commands { get; internal set; }
        }
    }
}
