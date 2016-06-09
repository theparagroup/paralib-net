# paralib-net
Various libraries we use for Microsoft .NET application development.

START HERE:

How the paralib-net repositories, solutions and projects are set up:

	<install-dir>
		oovent
			.git
			oovent.sln					
			nuget.config				
			oovent-migrations
			oovent-models
			oovent-mvc
		paralib-net
			.git
			paralib-net.sln	
			nuget.config				
			README.md					(this file)
			paralib-common
				readme.txt				(overview of the paralib)
			paralib-dal
			paralib-migrations
			paralib-mvc
		paralib-net-dependencies
			.git
			lib
			packages
		paralib-net-reference
			.git
			paralib-net-reference.sln
			nuget.config
			paralib-reference-mvc
				readme.txt				(coding guidelines, etc.)
				asp-deepdive.txt		(IIS, ASP.NET, & MVC 'deep dive')

				
				
Note: the "nuget.config" file relocates the packages folder to the
"paralib-net-dependencies" repository:

		<configuration>
		  <config>
			<add key="repositoryPath" value="..\paralib-net-dependencies\packages" />
		  </config>
		</configuration>

We use NuGet in a minimal way. Generally, only the paralib-net library uses NuGet,
with other projects directly referencing the assemblies in the paralib-net-dependencies
project. Packages that make changes to the toolchain (or buildfiles) may need to be
added to each individual project (such as Roslyn and MVC projects). Under no circumstances
do we use NuGet to install javascript libraries, CSS stylesheets, or other trivial
matters.
		
Next you should read the various readmes:

		paralib-net/paralib-common/readme.txt
		paralib-net-reference/paralib-reference-mvc/readme.txt
		paralib-net-reference/paralib-reference-mvc/asp-deepdive.txt
		