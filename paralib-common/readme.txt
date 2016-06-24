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


=============================================================
====	Validation
=============================================================


can have serviceproviders, and context items, but these not supported in MVC

can implement IValidateObject

MVC will addtionally validate based on data type

[Required] are fired first, validators should return valid for null data

validators should throw exceptions if datatypes wrong

validators should use newer "IsValid(object value, ValidationContext validationContext)"

validators should use two parameter ValidationResult(message,new string[]{validationContext.MemberName})

validators should use validationContext.DisplayName

[Display] 
[Range]
[RegularExpression]


=============================================================
====	JSON
=============================================================

using Newtonsoft.Json;

string json = Newtonsoft.Json.JsonConvert.SerializeObject(q.ToList());

avoid circular references:

	mark to ignore:

		public class User
		{
			public int Id;
			public string Name;

			[JsonIgnore]
			public virtual UserType UserType { get; set; }
		}
	
		public class UserType
		{
			public int Id;
			public string Name;

			[JsonIgnore]
			public virtual List<User> Users { get; set; }
		}	

	OR ignore loops:	
	
		string json = Newtonsoft.Json.JsonConvert.SerializeObject(q.ToList(), 
			new Newtonsoft.Json.JsonSerializerSettings(){ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore});

	OR project:

		var q = from u in db.Users
			select new User() { Id = u.Id, Email = u.Email+u.UserType.Name, Password = u.Password, UserTypeId = u.UserTypeId};

