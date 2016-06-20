=============================================================
====	Configuration
=============================================================




=============================================================
====	Logging
=============================================================

We've wrapped Log4Net for logging, but it's completely backward-compatible.

Improvements:
	simplified configuration
	simpliflied ILog (optional arguments, convienence methods)
	improved ILog (new compiler attibutes & properties)
	easier runtime inspection
	support for per-developer logging configuration

private static ILog _logger = LogManager.GetLogger(typeof(HelloController));

migration helpers

configuration:

	pure log4net
	paralib only
		use para.config if it exists, else in appweb config
		programatic is possible
		configuration event
 		logging is off by default unless <logging> element exists
	paralib+log4net
		use para.config if it exists, else in app/web config
		log4net is autoconfigured only if 
			paralib logging is enabled
			<log4net> exists in either paralib.config, app/web
		paralib level will override log4net level
		debug cannot be overridden (just enabled in various places)
		loggers configured in paralib and log4net are merged

note: unlike appsettings and connectionstrings, paralib & log4net settings
in paralib.config and app/web config are not merged! but paralib and log4net 
settings are! example:

		<paralib> in paralib.config + <log4net> in paralib.config
		<paralib> in paralib.config + <log4net> in app/web
		<paralib> in app/web + <log4net> in paralib.config
		<paralib> in app/web + <log4net> in app/web
