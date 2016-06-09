

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

		<appSettings>
			<add key="log4net.Internal.Debug" value="true"/>
		</appSettings>
  

..........................................................................................
