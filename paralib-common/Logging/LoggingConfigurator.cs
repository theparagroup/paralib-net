using System;
using log4net.Repository.Hierarchy;
using log4net.Repository;
using com.paralib.Configuration;
using System.IO;
using System.Reflection;
using log4net.Config;
using System.Text;

namespace com.paralib.Logging
{
    public class LoggingConfigurator
    {

        public static Hierarchy GetHierarchy()
        {
            /*
                LogManager.GetRepository() will be default return a Hierarchy object,
                unless you have registered a custom repository selector (and you haven't) like this:

                    <appSettings>
                        <add key="log4net.RepositorySelector" value="com.paralib.CustomRepositorySelector" />
                    </appSettings>
            */

            ILoggerRepository repository = log4net.LogManager.GetRepository();

            if (repository is Hierarchy)
            {
                return (Hierarchy)repository;
            }
            else
            {
                throw new ParalibException("Paralib requires log4net to use the DefaultRepositorySelector");
            }

        }


        public static void ResetConfiguration()
        {
            log4net.LogManager.ResetConfiguration();
        }

        public static void ConfigureFromDotNetConfig()
        {
            XmlConfigurator.Configure();
        }

        public static void ConfigureFromParalibConfig()
        {
            XmlConfigurator.Configure(new FileInfo(ConfigurationManager.ParalibConfigPath));
        }

        public static void ConfigureFromResourceStream(string resourceName)
        {
            Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            XmlConfigurator.Configure(s);
            s.Close();
        }

        public static void ConfigureFromXml(string xml)
        {
            MemoryStream s = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            XmlConfigurator.Configure(s);
            s.Close();
        }

        public static void Configure()
        {
            //TODO do something about connection timeout or throw an error or something when connectionstring is wrong
            //com.paralib.Logging.LoggingConfiguration.Configure(com.paralib.Logging.LoggingModes.Mvc);

            /*
                log4net's XmlConfigurator basically does nothing unless you have explicitly
                configured things. So missing <log4net> section, no <root>, etc. are fine.
            */

            //reset congfig
            ResetConfiguration();

            if (Paralib.Configuration.Logging.Enabled)
            {

                //use paralib.config if it has log4net section
                if (ConfigurationManager.HasLog4NetOverride)
                {
                    ConfigureFromParalibConfig();
                }
                else
                {
                    ConfigureFromDotNetConfig();
                }

                //programatically make changes per settings object
                Hierarchy h = GetHierarchy();

                h.Threshold = LogManager.GetLog4NetLevel(Paralib.Configuration.Logging.Level);

                /*
                    See also:

                        <configuration>
                            <appSettings>
                                <add key="log4net.Internal.Debug" value="true"/>
                            </appSettings>
                        </configuration>
                */

                log4net.Util.LogLog.InternalDebugging = Paralib.Configuration.Logging.Debug;


                //for now, just sync the appenders (logs)
                //< log name = "logger1" enabled = "true|false" type = "file" capture = "All|Fatal,Error,Warn,Info,Debug" path = "application.log" />
                //< log name = "logger2" enabled = "true|false" type = "database" capture = "All|Fatal,Error,Warn,Info,Debug" connection = "<default>|mvc" connectionType = "System.Data.SqlClient.SqlConnection" />

                
                foreach (var a in h.Root.Appenders)
                {
                    Paralib.Configuration.Logging.InternalLogs.Add(new Log() { Name = a.Name, LoggerType = a.GetType().Name });
                }
            }

        }


    }
}
