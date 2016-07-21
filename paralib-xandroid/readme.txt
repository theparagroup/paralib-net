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


Download correct version of NDK and unzip it beside SDK (google provides no help finding these)

	ndk_r10e (Jan 2015)
		Windows 32-bit : http://dl.google.com/android/ndk/android-ndk-r10e-windows-x86.exe


Goto Tools/Options/Xamarin and click "change" on all the paths, this will change the registry entries:

	HKEY_CURRENT_USER\Software\Novell\Mono for Android

issues:

Release:

	you can only deploy an apk with release build
	choose your CPU architecture

Don't even try to use the built-in Android emulator. Either use Gennymotion or a real device.

Genymotion Issues:
	soemthing like this:
		A numeric comparison was attempted on "$(_DeviceSdkVersion)" that evaluates to "" instead of a number, in condition "$(_DeviceSdkVersion) >= 21"

	make sure you set the android sdk path in genymotion to the same used by xamarin


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



=============================================================
====	Useful ADB Commands
=============================================================

C:\Program Files (x86)\Android\android-sdk\platform-tools

./adb devices

./adb -s c6cfb0701b2c3d05 shell pwd

./adb -s 0a00da34 shell pm list packages

./adb -s c6cfb0701b2c3d05 shell run-as App1.App1 ls //data/data/App1.App1

(i can't get runas to work on my note 4, and there is an "8k bug")

./adb backup -apk com.twitter.android

./adb install thisIsTheAPKName.apk

./adb -s c6cfb0701b2c3d05 logcat *:E

"C:\Program Files (x86)\Android\android-sdk\platform-tools\adb.exe" -s c6cfb0701b2c3d05 pull //system/etc/permissions/handheld_core_hardware.xml
[100%] //system/etc/permissions/handheld_core_hardware.xml

"C:\Program Files (x86)\Android\android-sdk\platform-tools\adb.exe" -s c6cfb0701b2c3d05 push handheld_core_hardware.xml //system/etc/permissions/handheld_core_hardware.xml
[100%] //system/etc/permissions/

android.hardware.usb.host.xml
handheld_core_hardware.xml
tablet_core_hardware.xml


=============================================================
====	Versions
=============================================================

Ice Cream Sandwich
14/4.0		
15/4.0.3

Jelly Bean
16/4.1.2
17/4.2.2
18/4.3.1

KitKat
19/4.4.2
20/4.4W.2

Lollipop
21/5.0.1
22/5.1.1