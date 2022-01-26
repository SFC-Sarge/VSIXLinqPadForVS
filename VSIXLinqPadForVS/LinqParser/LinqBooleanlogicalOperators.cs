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
// File Name        : LinqBooleanlogicalOperators.cs
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
/// 		<para>Time: 17:16</para>
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
/// 		<para>Time: 17:16</para>
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
    /// <summary>Class LinqBooleanlogicalOperators</summary>
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
    /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:16</para><b>Code Changes:</b><para>Created XML Comment</para></description>
    /// </item>
    /// </list>
    /// </remarks>
    public class LinqBooleanlogicalOperators
    {
        // Boolean logical Arithmetic Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators)
        // Unary Boolean logical Negation Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-negation-operator-)
        // Binary Boolean logical AND Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-and-operator-)
        // Binary Boolean logical OR Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-or-operator-)
        // Binary Boolean logical Exclusive OR Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-exclusive-or-operator-)
        // Binary Boolean Conditional logical AND Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#conditional-logical-and-operator-)
        // Binary Boolean Conditional logical OR Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#conditional-logical-and-operator-)
        /// <summary>The booleanlogical operators</summary>
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
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:16</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static readonly string[] BooleanlogicalOperators = "! & | ^ && ||".Split().ToArray();

        /// <summary>Enum CSharp_BooleanlogicalOperators
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
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:16</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public enum CSharp_BooleanlogicalOperators
        {
            /// <summary>The negation</summary>
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
            /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:16</para><b>Code Changes:</b><para>Created XML Comment</para></description>
            /// </item>
            /// </list>
            /// </remarks>
            negation,
            /// <summary>The and</summary>
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
            /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:16</para><b>Code Changes:</b><para>Created XML Comment</para></description>
            /// </item>
            /// </list>
            /// </remarks>
            and,
            /// <summary>The or</summary>
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
            /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:16</para><b>Code Changes:</b><para>Created XML Comment</para></description>
            /// </item>
            /// </list>
            /// </remarks>
            or,
            /// <summary>The xor</summary>
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
            /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:16</para><b>Code Changes:</b><para>Created XML Comment</para></description>
            /// </item>
            /// </list>
            /// </remarks>
            Xor,
            /// <summary>The conditional and</summary>
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
            /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:16</para><b>Code Changes:</b><para>Created XML Comment</para></description>
            /// </item>
            /// </list>
            /// </remarks>
            Conditional_and,
            /// <summary>The conditional or</summary>
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
            /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:16</para><b>Code Changes:</b><para>Created XML Comment</para></description>
            /// </item>
            /// </list>
            /// </remarks>
            Conditional_or
        }
    }
}
