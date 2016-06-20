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

                //programatically make changes per settings object
                Hierarchy h = GetHierarchy();

                /* debug from config is set in a static constructor, 
                   so all we can do here is allow a third way to enable

                        <configuration>
                            <appSettings>
                                <add key="log4net.Internal.Debug" value="true"/>
                            </appSettings>
                        </configuration>
                */
                if (Paralib.Configuration.Logging.Debug)
                {
                    log4net.Util.LogLog.InternalDebugging = true;
                }

                //use standard log4net configuration, add manual appenders
                //use paralib.config if it has log4net section, otherwise try app/web config
                if (ConfigurationManager.HasLog4NetOverride)
                {
                    ConfigureFromParalibConfig();
                }
                else
                {
                    ConfigureFromDotNetConfig();
                }

                //override root level (if specified)
                if (Paralib.Configuration.Logging.Level != LogLevels.None)
                {
                    h.Root.Level = LogManager.GetLog4NetLevel(Paralib.Configuration.Logging.Level);
                }

                //sync config with log4net
                Paralib.Configuration.Logging.Level = LogManager.GetLogLevel(h.Root.Level);
                Paralib.Configuration.Logging.Debug = log4net.Util.LogLog.InternalDebugging;

                foreach (var appender in h.Root.Appenders)
                {
                    Paralib.Configuration.Logging.InternalLogs.Add(new Log() { Name = appender.Name, LogType = LogTypes.Log4Net, LoggerType = appender.GetType().Name });
                }

                //add paralib configured loggers to log4net
                foreach (var log in Paralib.Configuration.Logging.Logs)
                {
                    if (log.Enabled)
                    {
                        //TODO filters
                        switch (log.LogType)
                        {
                            case LogTypes.Console:
                                h.Root.AddAppender(CreateParaConsoleAppender(log.Name, log.Capture, log.Pattern));
                                break;
                            case LogTypes.File:
                                h.Root.AddAppender(CreateParaRollingFileAppender(log.Name, log.Capture, log.Pattern, log.Path));
                                break;
                            case LogTypes.Database:
                                break;
                            case LogTypes.Log4Net:
                                //ignore the manually configured appenders from above
                                break;
                            default:
                                throw new ParalibException($"Can't Create LogType {log.LogType}");
                        }

                    }


                }

                h.Configured = true;
            }

        }

        internal static void AddFilters(log4net.Appender.AppenderSkeleton appender, string capture)
        {
            //no filter
            if (capture == null) return;

            //capture= "Debug, Fatal, Debug-Warn"
            //note: range filter will prevent additional filters from running on deny
            string[] rules = capture.Split(',');

            foreach (string rule in rules)
            {

                if (rule.Contains("-"))
                {
                    string[] levels = rule.Split('-');

                    var filter = new log4net.Filter.LevelRangeFilter();
                    filter.AcceptOnMatch = true;
                    filter.LevelMin = LogManager.GetLog4NetLevel((LogLevels)Enum.Parse(typeof(LogLevels), levels[0], true));
                    filter.LevelMax = LogManager.GetLog4NetLevel((LogLevels)Enum.Parse(typeof(LogLevels), levels[1], true));
                    appender.AddFilter(filter);

                }
                else
                {
                    var filter = new log4net.Filter.LevelMatchFilter();
                    filter.AcceptOnMatch = true;
                    filter.LevelToMatch = LogManager.GetLog4NetLevel((LogLevels)Enum.Parse(typeof(LogLevels),rule, true));
                    appender.AddFilter(filter);
                }


            }

            //finally, deny all
            appender.AddFilter(new log4net.Filter.DenyAllFilter());



        }

        internal static log4net.Appender.IAppender CreateParaConsoleAppender(string name, string capture, string pattern)
        {
            ParaConsoleAppender appender = new ParaConsoleAppender();
            appender.Name = name;

            log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout();
            layout.ConversionPattern = pattern??ParaConsoleAppender.DefaultPattern;
            layout.ActivateOptions();

            appender.Layout = layout;
            appender.ActivateOptions();

            AddFilters(appender, capture);

            return appender;
        }


        internal static log4net.Appender.IAppender CreateParaRollingFileAppender(string name, string capture, string pattern, string file)
        {
            ParaRollingFileAppender appender = new ParaRollingFileAppender();
            appender.Name = name;
            appender.File = file??"application.log";
            appender.AppendToFile = true;
            appender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Size;
            appender.MaxSizeRollBackups = 10;
            appender.MaximumFileSize = "10MB";

            log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout();
            layout.ConversionPattern = pattern ?? ParaRollingFileAppender.DefaultPattern;
            layout.ActivateOptions();

            appender.Layout = layout;
            appender.ActivateOptions();

            AddFilters(appender, capture);

            return appender;
        }

        internal static void AddParameter(log4net.Appender.AdoNetAppender appender, string pattern)
        {
            log4net.Appender.AdoNetAppenderParameter param = new log4net.Appender.AdoNetAppenderParameter();

            string parameterName = "";

            //%date
            //%something
            //%property{foo}



            param.ParameterName = parameterName;

            if (true)
            {
                param.DbType = System.Data.DbType.String;
                param.Size = -1;
                param.Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.PatternLayout(pattern));
            }
            else
            {
                param.DbType = System.Data.DbType.DateTime;
                param.Layout = new log4net.Layout.RawUtcTimeStampLayout();
            }


            appender.AddParameter(param);
        }



    }
}
