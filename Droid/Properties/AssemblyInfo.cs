﻿using System.Reflection;
using System.Runtime.CompilerServices;
using Android;
using Android.App;
using Xamarin.Forms.Xaml;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle("Tabs")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("${AuthorCopyright}")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion("1.0.*")]

// The following attributes are used to specify the signing key for the assembly, 
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: UsesPermission(Manifest.Permission.AccessFineLocation)]
[assembly: UsesPermission(Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Manifest.Permission.Internet)]