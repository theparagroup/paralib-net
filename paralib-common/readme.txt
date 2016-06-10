=============================================================
====	Configuration
=============================================================




=============================================================
====	Logging
=============================================================

We user Log4Net for logging.

none, basic, mvc

private static ILog _logger = LogManager.GetLogger(typeof(HelloController));

base class

Info, Warn, ect

migration helpers

Debugging log4net problems (view output in console or trace):

		config file:
	
			<log4net debug="true">
			</log4net>

		app setting:
	
			<appSettings>
				<add key="log4net.Internal.Debug" value="true"/>
			</appSettings>

		programatically:
	
			 log4net.helpers.LogLog.InternalDebugging property=true;

..........................................................................................
