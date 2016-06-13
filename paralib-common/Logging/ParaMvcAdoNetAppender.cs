using System;
using log4net.Appender;
using log4net.Core;

namespace com.paralib.Logging
{
    public class ParaMvcAdoNetAppender : ParaAdoNetAppender
    {

        protected override void Append(LoggingEvent loggingEvent)
        {
            loggingEvent.Properties["user"] = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            base.Append(loggingEvent);
        }
    }
}