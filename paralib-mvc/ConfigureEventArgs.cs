using System;
using com.paralib.common.Configuration;

namespace com.paralib.mvc
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
