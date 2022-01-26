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
// File Name        : LinqComparisonOperators.cs
// License          : Open Source Apache License Version 2.0. Eee included License file.
// <summary></summary>
// ***********************************************************************
using System.Linq;

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
    /// <summary>Class LinqComparisonOperators</summary>
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
    public class LinqComparisonOperators
    {
        // Comparison-operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/comparison-operators)
        // Less-than-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/comparison-operators#less-than-operator-)
        // Greater-than-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/comparison-operators#greater-than-operator-)
        // Less-than-or-equal-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/comparison-operators#less-than-or-equal-operator-)
        // Greater-than-or-equal-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/comparison-operators#greater-than-or-equal-operator-)
        /// <summary>The equality operators</summary>
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
        public static readonly string[] EqualityOperators = "< > <= >=".Split().ToArray();

        /// <summary>Enum CSharp_EqualityOperators
        /// </summary>
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
        public enum CSharp_EqualityOperators
        {
            /// <summary>The less than</summary>
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
            LessThan,
            /// <summary>The greater than</summary>
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
            GreaterThan,
            /// <summary>The less than or equal</summary>
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
            LessThanOrEqual,
            /// <summary>The greater than or equal</summary>
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
            GreaterThanOrEqual
        }
    }
}
