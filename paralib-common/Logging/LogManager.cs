using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Logging
{
    public class LogManager
    {
        /*
            TODO make some improvements!

            0] use: 
                    var _logger= GetLogger(typeof(Paralib));
                    var _logger= GetLogger(nameof(Paralib));

            1] add the new compiler attributes to ILog and add these to Thread.Properties %P/&property
            2] do the same for IPrincipal?
            3] quick & dirty logging using stackframe to get type? bad performance but easy: Paralib.Log.Info()


            [MethodImpl(MethodImplOptions.NoInlining)]
            public void Log([CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
            {

                StackFrame frame = new StackFrame(1);
                var method = frame.GetMethod();
                var type = method.DeclaringType;
                var name = method.Name;

                Console.WriteLine("{2} is called by {0}.{1}", type, memberName, nameof(Log));
            }
        */
        public static ILog GetLogger(Assembly repositoryAssembly, Type type)
        {
            return new Logger(log4net.LogManager.GetLogger(repositoryAssembly, type.FullName));
        }

        public static ILog GetLogger(Type type)
        {
            return new Logger(log4net.LogManager.GetLogger(Assembly.GetCallingAssembly(), type.FullName));
        }

        public static ILog GetLogger(Assembly repositoryAssembly, string name)
        {
            return new Logger(log4net.LogManager.GetLogger(repositoryAssembly, name));
        }

        public static ILog GetLogger(string name)
        {
            return new Logger(log4net.LogManager.GetLogger(Assembly.GetCallingAssembly(), name));
        }


        internal static log4net.Core.Level GetLog4NetLevel(LogLevels level)
        {
            switch (level)
            {
                case LogLevels.Debug:
                    return log4net.Core.Level.Debug;
                case LogLevels.Info:
                    return log4net.Core.Level.Info;
                case LogLevels.Warn:
                    return log4net.Core.Level.Warn;
                case LogLevels.Error:
                    return log4net.Core.Level.Error;
                case LogLevels.Fatal:
                    return log4net.Core.Level.Fatal;
                default:
                    throw new ParalibException($"Unknown or bad logging level [{level}]");
            }
        }



    }
}
