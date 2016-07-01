using System;
using System.Collections.Generic;
using com.paralib.Configuration;
using com.paralib.Ado;
using com.paralib.Logging;
using System.Collections;

namespace com.paralib
{

    public partial class Paralib
    {
        public static class Configuration
        {

            public static AppSettings AppSettings { get; } = new AppSettings();
            public static ConnectionStrings ConnectionStrings { get; } = new ConnectionStrings();


         


        }
    }

}
