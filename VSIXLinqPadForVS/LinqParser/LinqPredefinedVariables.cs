//***********************************************************************
// Assembly         : VSIXLinqPadForVS
// Author UserID    : sfcsarge
// Author Full Name : Danny C. McNaught
// Author Phone     : #-###-###-####
// Company Name     : Computer Question// Created          : 01-26-2022
//
// Created By       : Danny C. McNaught
// Last Modified By : Danny C. McNaught
// Last Modified On : 01-26-2022
// Change Request # :
// Version Number   :
// Description      :
// File Name        : LinqPredefinedVariables.cs
// License          : Open Source Apache License Version 2.0. Eee included License file.
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

/// <summary>
/// The LinqParser namespace.
/// </summary>
/// <remarks>
///  	<para><b>History:</b></para>
///  	<list type="table">
///  		<listheader>
///  			<devName>Developer\Date\Time</devName>
///  			<devCompany>Developer Company</devCompany>
///  			<devPhone>Developer Phone</devPhone>
///  			<devEmail>Developer Email</devEmail>
///  			<devMachine>Developer On</devMachine>
///  			<description>Description</description>
///  		</listheader>
///  		<item>
///  				<devName>
/// 		Developer: Danny C. McNaught
/// 		<para>Date: Wednesday, January 26, 2022</para>
/// 		<para>Time: 17:17</para>
/// 	</devName>
///  			<devCompany>Computer Question</devCompany>
///  			<devPhone>#-###-###-####</devPhone>
///  				<devEmail>
/// 		<a href="mailto:danny.mcnaught@dannymcnaught.com">mailto:danny.mcnaught@dannymcnaught.com</a>
/// 		<para><a href="mailto:">mailto:</a></para>
/// 		<para><a href="mailto:">mailto:</a></para>
/// 	</devEmail>
///  			<devMachine>WINDOWS11DEV</devMachine>
///  			<description>Created XML Comment</description>
///  		</item>
///  		<item>
///  				<devName>
/// 		Developer: Danny C. McNaught
/// 		<para>Date: Wednesday, January 26, 2022</para>
/// 		<para>Time: 17:17</para>
/// 	</devName>
///  			<devCompany>Computer Question</devCompany>
///  			<devPhone>#-###-###-####</devPhone>
///  				<devEmail>
/// 		<a href="mailto:danny.mcnaught@dannymcnaught.com">mailto:danny.mcnaught@dannymcnaught.com</a>
/// 		<para><a href="mailto:">mailto:</a></para>
/// 		<para><a href="mailto:">mailto:</a></para>
/// 	</devEmail>
///  			<devMachine>WINDOWS11DEV</devMachine>
///  			<description>Updated XML Comment</description>
///  		</item>
///  	</list>
/// </remarks>
namespace VSIXLinqPadForVS.LinqParser
{
    /// <summary>Class LinqPredefinedVariables</summary>
    /// <remarks>
    /// <para><b>History:</b></para>
    /// <list type="table">
    /// <item>
    /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
    /// </item>
    /// <item>
    /// <description><b>Code changed on Visual Studio Host Machine:</b><para>WINDOWS11DEV</para></description>
    /// </item>
    /// <item>
    /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:17</para><b>Code Changes:</b><para>Created XML Comment</para></description>
    /// </item>
    /// </list>
    /// </remarks>
    internal sealed class LinqPredefinedVariables
    {
        /// <summary>The variables</summary>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>WINDOWS11DEV</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:17</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static Dictionary<string, string> Variables = new()
        {
            { "AppDataLocalFolder", "The sub-folder under %LOCALAPPDATA% for this application." },
            { "ApplicationExtensionsFolder", "n/a" },
            { "AppName", "The qualified name of the application that is passed to the AppEnv.dll entry points. The qualified name consists of the application name, an underscore, and the class identifier (CLSID of the application automation object, which is also recorded as the value of the ThisVersionDTECLSID setting in the project .pkgdef file." },
            { "BaseInstallDir", "The full path of the location where Visual Studio was installed." },
            { "CommonFiles", "The value of the %CommonProgramFiles% environment variable." },
            { "Initialization", "n/a" },
            { "MyDocuments", "The full path of the My Documents folder of the current user." },
            { "PackageFolder", "The full path of the directory that contains the package assembly files for the application." },
            { "ProgramFiles", "The value of the %ProgramFiles% environment variable." },
            { "RootFolder", "The full path of the root directory of the application." },
            { "RootKey", "The root registry key for the application. By default the root is in HKEY_CURRENT_USER\\Software\\CompanyName\\ProjectName\\VersionNumber (when the application is running, _Config is appended to this key. It is set by the RegistryRoot value in the SolutionName.pkgdef file." },
            { "ShellFolder", "The full path of the location where Visual Studio was installed." },
            { "System", "The Windows\\system32 folder." },
            { "WinDir", "The Windows folder." },
        };
    }
}
