using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace com.paralib.Mvc.Infrastructure.WebApi2
{


    public class ParaExceptionLogger: ExceptionLogger
    {
        //TODO FIX THIS WITH ParaWebApiExceptionLogger ATTRIBUTE
        //MAKE SURE EVENT HANDLERS ARE STATIC

        //public static EventHandler<ParaExceptionArgs> LogEvent;

        //public delegate void ParaExceptionLoggerEvent(ExceptionLoggerContext context);
        //public static event ParaExceptionLoggerEvent LogEvent;

        public static event Action<ExceptionLoggerContext> LogEvent;

        public override void Log(ExceptionLoggerContext context)
        {

            if (LogEvent!=null)
            {
                LogEvent(context);
            }

        }
    }
}
