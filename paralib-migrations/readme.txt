=============================================================
====	Database Conventions
=============================================================

We use FluentMigrator to create our databases, even when using EF
or other ORMs (no code first).

tables -> lower underscore, plural (e.g. users)
columns -> lower underscore

generally:
	table names should match models names (e.g. user_roles -> UserRole)
	column names should match member names (e.g. role_id -> RoleId)


	[users]						User
		id							Id
		first_name					FirstName
		role_id						RoleId
									Roles

	[roles]						Role
		id							Id
		name						Name


column order (generally):
		primary keys
		foreign keys (lookups)
		foreign keys (entities)
		dependent data in order of significance
	
synthetic keys only

all tables have an "id" primary key (even many-to-many)

identity
		use identity for data that users create
		use identity for lookup data
			except when there is code that relies on the values
				(and there usually should be a matching enumeration)
				(start these values at 1)
		

=============================================================
====	Migration Naming Conventions
=============================================================

filename should be "MMDDYYYYSSSS-TypeName.cs"
	where SSSS is a squence
	TypeName is something meaningful (e.g. "InitialSchema")

MMDDYYYYSSSS goes in MigrationAttribute
TypeName is the name of the class
Down() before Up() in file

=============================================================
====	Standard Datatypes, Helpers
=============================================================

Commonly used schema fragments are located in the paralib-migrations 
project.

