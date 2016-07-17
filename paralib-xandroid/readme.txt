=============================================================
====	Xamarin Install
=============================================================

install java 1.7
install android sdk
install xamarin for visual studio

Run SDK Manager (as admin)
install
	Android SDK Tools
	Android SDK Platform-tools
	Android SDK Build Tool (latest - but see note about v24 and and Java8 below)
	SDK Platforms for APIs you want to target


issues:

Don't even try to use the built-in Android emulator. Either use Gennymotion or a real device.

Xamarin.iOS.CSharp.targets not found (shared projects):

	https://blogs.msdn.microsoft.com/smondal/2015/08/24/the-imported-project-cprogram-filesx86msbuildmicrosoftwindowsxamlv14-08-1microsoft-windows-ui-xaml-csharp-targets-was-not-found/

	open:
		C:\Program Files (x86)\MSBuild\Microsoft\VisualStudio\v14.0\CodeSharing\Microsoft.CodeSharing.CSharp.targets

	replace this:
	
		<Import Project="$(MSBuildExtensionsPath32)\Microsoft\WindowsXaml\v$(VisualStudioVersion)\Microsoft.Windows.UI.Xaml.CSharp.targets" Condition="Exists(‘$(MSBuildExtensionsPath32)\Microsoft\WindowsXaml\v$(VisualStudioVersion)\Microsoft.Windows.UI.Xaml.CSharp.targets’)"/>
		<Import Project="$(MSBuildBinPath)\Microsoft.CSharp.Targets" Condition="!Exists(‘$(MSBuildExtensionsPath32)\Microsoft\WindowsXaml\v$(VisualStudioVersion)\Microsoft.Windows.UI.Xaml.CSharp.targets’)" />  

	with this:

		<Import Project="$(MSBuildExtensionsPath32)\Microsoft\WindowsXaml\v$(VisualStudioVersion)\Microsoft.Windows.UI.Xaml.CSharp.targets" Condition="false"/>
		<Import Project="$(MSBuildBinPath)\Microsoft.CSharp.Targets" Condition="true" />


Java Version Issues:
	Android SDK Build Tools version 24 uses Java8, but Xamarin requires Java7. If you have issues, downgrade to build tools version 23.0.3.



Driver Issues:

	Choose the google driver:

		C:\Program Files (x86)\Android\android-sdk\extras\google\usb_driver

	Or download the driver from the manufacuturer, such as Samsung, as here:

	http://developer.samsung.com/technical-doc/view.do?v=T000000117#none


Mono.AndroidTools.InstallFailedException: Failure [INSTALL_FAILED_UPDATE_INCOMPATIBLE]:

	Uninstall the app via Application Manager. It may be listed under the "package name".
