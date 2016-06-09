=============================================================
====	Naming Conventions
=============================================================

Unless there is a decent reason, we follow Microsoft conventions.

lower underscored="hello_there"
upper underscored="Hello_There"
lower dotted = "hello.there"
upper dotted = "Hello.There"
upper camel (UC) = "HelloThere"
lower camel (LC)= "helloThere"

namespaces
	prefixed with reverse DNS name, lower dotted
	then upper dotted

typenames
	class -> TypeName
	struct -> TypeName
	inteferface -> ITypeName
	enum -> TypeNames (plural, e.g. RenderModes)

members
	private fields ->  _memberName (lower camel with underscore prefix)
	public fields -> MemberName (upper camel)
	methods, public/private properties -> MemberName (upper camel)
	members involving enums -> TypeName (singular, e.g. RenderMode)

migrations
	filename should be "MMDDYYYYSSSS-TypeName.cs"
		where SSSS is a squence
		TypeName is something meaningful (e.g. "InitialSchema")
	MMDDYYYYSSSS goes in MigrationAttribute
	TypeName is the name of the class

database
	tables -> lower underscore, plural (e.g. users)
	columns -> lower underscore
	generally:
		table names should match models names (e.g. user_roles -> UserRole)
		column names should match member names (e.g. role_id -> RoleId)

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
