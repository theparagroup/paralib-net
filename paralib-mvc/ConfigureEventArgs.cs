using System;
using com.paralib.Configuration;

namespace com.paralib.Mvc
{
    public class ConfigureEventArgs:EventArgs
    {
        public Settings Settings { get; private set; }

        public ConfigureEventArgs(Settings settings)
        {
            Settings = settings;
        }
    }
}
