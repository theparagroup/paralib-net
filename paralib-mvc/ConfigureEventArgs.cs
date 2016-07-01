using System;

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
