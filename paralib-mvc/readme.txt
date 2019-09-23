How to Setup New Paralib MVC Project

0] Run Visual Studio 2015 as Administrator

1] Create new project:

		Framework 4.6
		ASP.NET Web Application
		Empty 4.6 Template
		Uncheck Everything
		Delete "packages.config"

		
2] Update Rosyln:

	Update DotNetCompilerPlatorm referenece to 1.0.1 from paralib-dependencies

	Change this in project file to point to 1.0.1
	
		<Import Project="..\..\paralib-net-dependencies\packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.1.0.0 ....

	Remove EnsureNuGetPackageBuildImports target in project file

	Note: even though the file name is 1.0.1, the assembly version is 1.0.0.0

	Note: if you get "compilation error", kill all "VBCSCompiler.exe" tasks
	
3] Change Project Properties/Web

		Use Local IIS on port 90
			http://localhost:90/project

		Create Virtual Directory
	
4] Create an ASPX page using c# v6 to ensure things are working

		 <%= $"hello : {DateTime.Now}" %>
	
5] Reference the "MVC 5.2.3" assembly:

		..\..\paralib-net-dependencies\packages\Microsoft.AspNet.Mvc.5.2.3\lib\net45\System.Web.Mvc.dll	
	
6] Reference Razor 3.2.3 and related assemblies:

		..\..\paralib-net-dependencies\packages\Microsoft.AspNet.Razor.3.2.3\lib\net45\System.Web.Razor.dll
		..\..\paralib-net-dependencies\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.Helpers.dll
		..\..\paralib-net-dependencies\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.WebPages.dll
		..\..\paralib-net-dependencies\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.WebPages.Deployment.dll
		..\..\paralib-net-dependencies\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.WebPages.Razor.dll
		..\..\paralib-net-dependencies\packages\Microsoft.Web.Infrastructure.1.0.0.0\lib\net40\Microsoft.Web.Infrastructure.dll	
	

	
7] Create a global event handler (Global.asax), using the "Global Application Class" template.

8] Add the following to Application_Start
		
		RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
		RouteTable.Routes.MapMvcAttributeRoutes();

		You'll want to add the following using declarations to resolve the classes and
		extension methods:

			using System.Web.Mvc;
			using System.Web.Routing;
	
9]	Create a "Views" folder and add the web.config template from here:

		..\paralib-net-dependencies\templates\mvc5\Views\Web.config	
		
10] Include in project and update application namespace

11] Create a controller. The containing folder name doesn't matter, but the class
should derive from Controller and the class name must end in "Controller".
We use attribute-based routing:

		using System.Web.Mvc;

		namespace MvcTheHardWay
		{
			public class HelloController: Controller
			{
				[Route("~/hello")]
				public ActionResult Index()
				{
					return View();
				}
			}
		}

12] Create a view and ensure Razor is working with C# v6

13] Reference WebApi2 assemblies

		..\..\paralib-net-dependencies\packages\Microsoft.AspNet.WebApi.Core.5.2.3\lib\net45\System.Web.Http.dll
		..\..\paralib-net-dependencies\packages\Microsoft.AspNet.WebApi.WebHost.5.2.3\lib\net45\System.Web.Http.WebHost.dll
		..\..\paralib-net-dependencies\packages\Microsoft.AspNet.WebApi.Client.5.2.3\lib\net45\System.Net.Http.Formatting.dll	
		..\..\paralib-net-dependencies\packages\Newtonsoft.Json.8.0.3\lib\net45
		
14] Add binding redirect for Newtonsoft JSON to the web config
		
      <dependentAssembly>
        <assemblyIdentity name="Newtonsoft.Json" culture="neutral" publicKeyToken="30ad4fe6b2a6aeed" />
        <bindingRedirect oldVersion="0.0.0.0-10.0.0.0" newVersion="8.0.0.0" />
      </dependentAssembly>

15] Create a webapi2 controller
	  
    [RoutePrefix("api/v1")]
    public class TestController : ApiController
    {
        //api/v1/test
        [HttpGet]
        [Route("test")]
        public string Test()
        {
            return $"testing : {DateTime.Now}";
        }
    }
		
16] Add paralib projects to solution
		
		paralib-common
		paralib-gen
		parahtml
		paralib-mvc

17] Remove MVC and WebApi configuration from global asax

18] Add paralib-mvc to project

19] Run project. paralib-mvc will auto-configure and write sample entries to web config

20] Edit auto-generated paralib settings in web.config	

21] Now you can create a base class for your WebApi2 controllers (which will inherit routing
    attributes), and create a BasicAuthAttribute that you can add to your controllers:
	
		public class BasicAuthAttribute : Para.BasicAuthAttribute
		{
			public BasicAuthAttribute() : base("My Realm") { }

			protected override IPrincipal OnAuthenticate(string user, string password)
			{
				if (user=="test" && password=="foobar")
				{
					return new ParaPrinciple(user, new string[] { });
				}
				else
				{
					return null;
				}

			}
		}
	
	
