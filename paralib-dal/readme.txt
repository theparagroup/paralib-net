=============================================================
====	Creating a Standard User (SQL Server)
=============================================================

Create Login
	user/pass
	disable policies

Create user in database
	general
		user
		login
		schema=dbo
	membership
		db_owner

if the user already exists

	ALTER USER user_name WITH LOGIN = login_name


=============================================================
====	Correct NTFS Permmissions for SQL Server Files
=============================================================

Note: running Management Studio as administrator works as well.

Using Management Studio to attach/create, make sure the folder has full control
permissions for the user Studio is running under.

Note the following user will be given acccess as well:

	"NT SERVICE\MSSQL$SQLEXPRESS12"



