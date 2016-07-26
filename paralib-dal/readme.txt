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
