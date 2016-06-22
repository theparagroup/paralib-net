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

