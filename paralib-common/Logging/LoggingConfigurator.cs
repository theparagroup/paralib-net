using System;
using log4net.Repository.Hierarchy;
using log4net.Repository;
using System.IO;
using System.Reflection;
using log4net.Config;
using System.Text;
using System.Text.RegularExpressions;
using com.paralib.Configuration;
using com.paralib.Ado;

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

        public static void ConfigureFromOverridesConfig()
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

        public static LogLevels DefaultLevel
        {
            get
            {
                return LogLevels.All;
            }
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

            //this is the master switch if if enabled (e.g., <paralib><logging> is present)
            //log4net will not be autoconfigured if paralib logging is off
            if (Paralib.Logging.Enabled)
            {

                //programatically make changes per settings object
                Hierarchy h = GetHierarchy();

                //let's default to All
                h.Root.Level = LogManager.GetLog4NetLevel(DefaultLevel);

                //start with standard log4net configuration
                //use paralib.config if it has log4net section, otherwise try app/web config
                if (ConfigurationManager.HasLog4NetOverride)
                {
                    ConfigureFromOverridesConfig();
                }
                else
                {
                    if (ConfigurationManager.HasLog4Net)
                    {
                        ConfigureFromDotNetConfig();
                    }
                }

                /* debug from config is set in a static constructor, 
                   so all we can do here is allow a fourth way to enable (including programatically)

                        <configuration>
                            <appSettings>
                                <add key="log4net.Internal.Debug" value="true"/>
                            </appSettings>
                        </configuration>

                        <log4net debug="true">
                        </log4net>

                        <paralib>
                            <logging debug="true"/>
                        </paralib>

                */
                if (Paralib.Logging.Debug)
                {
                    log4net.Util.LogLog.InternalDebugging = true;
                }

                //logger levels (not the same as capture)
                //      All < DEBUG < INFO < WARN < ERROR < FATAL < OFF

                //override root level only if specified (default in config file is unspecified)
                if (Paralib.Logging.Level != LogLevels.Unspecified)
                {
                    h.Root.Level = LogManager.GetLog4NetLevel(Paralib.Logging.Level);
                }

                //sync config with log4net:
                //
                //  <logging enabled="true" level="(set by log4net or in paralib)" debug="(set true in paralib or by log4net)">
                //      <logs>
                //          <log name="set by log4net">
                //          <log name="set in paralib">
                Paralib.Logging.Level = LogManager.GetLogLevel(h.Root.Level);
                Paralib.Logging.Debug = log4net.Util.LogLog.InternalDebugging;

                foreach (var appender in h.Root.Appenders)
                {
                    Paralib.Logging.InternalLogs.Add(new Log(appender.Name, LogTypes.Log4Net, appender.GetType().Name,true));
                }

                //add paralib configured loggers to log4net
                foreach (var log in Paralib.Logging.Logs)
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
                                h.Root.AddAppender(CreateParaAdoNetAppender(log.Name, log.Capture, log.Pattern, log.Table, log.Fields, Paralib.Dal.Databases[log.Database]));
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

                    LogLevels minLevel = (LogLevels)Enum.Parse(typeof(LogLevels), levels[0], true);
                    LogLevels maxLevel = (LogLevels)Enum.Parse(typeof(LogLevels), levels[1], true);

                    if ((minLevel==LogLevels.Off)||(maxLevel==LogLevels.Off)) continue;

                    if ((minLevel == LogLevels.All) || (maxLevel == LogLevels.All))
                    {
                        AddFilter(appender, LogLevels.Debug, LogLevels.Fatal);
                        continue;
                    }

                    AddFilter(appender, minLevel, maxLevel);

                }
                else
                {

                    LogLevels levelToMatch = (LogLevels)Enum.Parse(typeof(LogLevels), rule, true);

                    if (levelToMatch == LogLevels.Off) continue;

                    if (levelToMatch == LogLevels.All)
                    {
                        AddFilter(appender, LogLevels.Debug, LogLevels.Fatal);
                        continue;
                    }

                    AddFilter(appender, levelToMatch);

                }


            }

            //finally, deny all
            appender.AddFilter(new log4net.Filter.DenyAllFilter());



        }

        internal static void AddFilter(log4net.Appender.AppenderSkeleton appender, LogLevels levelToMatch)
        {
            var filter = new log4net.Filter.LevelMatchFilter();
            filter.AcceptOnMatch = true;
            filter.LevelToMatch = LogManager.GetLog4NetLevel(levelToMatch);
            appender.AddFilter(filter);

        }

        internal static void AddFilter(log4net.Appender.AppenderSkeleton appender, LogLevels levelMin,LogLevels levelMax)
        {
            var filter = new log4net.Filter.LevelRangeFilter();
            filter.AcceptOnMatch = true;
            filter.LevelMin = LogManager.GetLog4NetLevel(levelMin);
            filter.LevelMax = LogManager.GetLog4NetLevel(levelMax);
            appender.AddFilter(filter);
        }

        internal static log4net.Appender.IAppender CreateParaConsoleAppender(string name, string capture, string pattern)
        {
            ParaConsoleAppender appender = new ParaConsoleAppender();
            appender.Name = name;

            log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout();
            layout.ConversionPattern = pattern ?? ParaConsoleAppender.DefaultPattern;
            layout.ActivateOptions();

            appender.Layout = layout;
            appender.ActivateOptions();

            AddFilters(appender, capture);

            return appender;
        }


        internal static log4net.Appender.IAppender CreateParaRollingFileAppender(string name, string capture, string pattern, string path)
        {
            ParaRollingFileAppender appender = new ParaRollingFileAppender();
            appender.Name = name;
            appender.File = path ?? ParaRollingFileAppender.DefaultPath;
            appender.AppendToFile = true;
            appender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Size;
            appender.MaxSizeRollBackups = 10;
            appender.MaximumFileSize = "10MB";
            appender.ImmediateFlush = true;

            log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout();
            layout.ConversionPattern = pattern ?? ParaRollingFileAppender.DefaultPattern;
            layout.ActivateOptions();

            appender.Layout = layout;
            appender.ActivateOptions();

            AddFilters(appender, capture);

            return appender;
        }

        public static log4net.Appender.IAppender CreateParaAdoNetAppender(string name, string capture, string pattern, string table, string fields, Database database)
        {
            ParaAdoNetAppender appender = new ParaAdoNetAppender();
            appender.Name = name;
            appender.BufferSize = 1;

            //set up connection
            appender.ConnectionType = database.ConnectionType;
            appender.ConnectionString = database.GetConnectionString(true);

            //for commandtext
            string fieldlist = "";
            string paramlist = "";

            //add parameters
            //NOTE: we don't support patterns with nested commas like:
            //      %utcdate{HH:mm:ss,fff}
            pattern = pattern ?? ParaAdoNetAppender.DefaultPattern;
            string[] parameters = pattern.Split(',');

            foreach (string parameter in parameters)
            {
                if (paramlist != "") paramlist += ",";

                //log4net will honor leading/trailing spaces so we trim
                paramlist += "@"+AddParameter(appender, parameter.Trim()); 
            }

            //build commandtext
            table = table ?? ParaAdoNetAppender.DefaultTable;
            fields = fields ?? ParaAdoNetAppender.DefaultFields;

            string[] columns = fields.Split(',');

            foreach (string column in columns)
            {
                if (fieldlist != "") fieldlist += ",";
                fieldlist += $"[{column.Trim()}]";
            }

            appender.CommandText = $"INSERT INTO [{table}] ({fieldlist}) VALUES ({paramlist})";

            appender.ActivateOptions();

            AddFilters(appender, capture);

            return appender;
        }

        internal static string ParseNameFromPattern(string pattern)
        {
            //%-20.20name{option}
            string name = Regex.Replace(pattern, @"[%\s\d-.}]", "");
            name = name.Replace('{', '_');
            return name;
        }

        internal static int ParseLengthFromPattern(string pattern)
        {
            //%-20.20name{option}
            Match match = Regex.Match(pattern, @"\.\d+");

            if (match.Success)
            {
                return int.Parse(match.Value.Replace(".", ""));
            }

            return -1;
        }

        internal static string AddParameter(log4net.Appender.AdoNetAppender appender, string pattern)
        {

            //%date
            //%.255something
            //%property{foo}

            string parameterName = ParseNameFromPattern(pattern);
            int length = ParseLengthFromPattern(pattern);


            log4net.Appender.AdoNetAppenderParameter param = new log4net.Appender.AdoNetAppenderParameter();
            param.ParameterName = parameterName;

            if ((parameterName == "date") || (parameterName == "d"))
            {
                param.DbType = System.Data.DbType.DateTime;

                //TODO should this be an option?                    
                //param.Layout = new log4net.Layout.RawUtcTimeStampLayout();
                param.Layout = new log4net.Layout.RawTimeStampLayout();
            }
            else
            {
                param.DbType = System.Data.DbType.String;
                param.Size = length;
                param.Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.PatternLayout(pattern));
            }


            appender.AddParameter(param);

            return parameterName;
        }



    }
}
