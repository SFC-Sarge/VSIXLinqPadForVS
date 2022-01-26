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
// File Name        : LinqMemberAccessOperators.cs
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
    /// <summary>Class LinqMemberAccessOperators</summary>
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
    public class LinqMemberAccessOperators
    {
        // Member-access-operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators)
        // Member-access-expression . Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#member-access-expression-)
        // Indexer-operator [] Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#indexer-operator-)
        // Null-conditional-operator ?. Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-)
        // Null-conditional-operator ?[] Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-)
        // Invocation-expression () Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#invocation-expression-)
        // index-from-end-operator ^ Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#index-from-end-operator-)
        // Range-operator .. Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#range-operator-)
        /// <summary>The member access operators</summary>
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
        public static readonly string[] MemberAccessOperators = ". [] ?. ?[] () ^ ..".Split().ToArray();

        /// <summary>Enum CSharp_MemberAccessOperators
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
        public enum CSharp_MemberAccessOperators
        {
            /// <summary>The member access expression</summary>
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
            MemberAccessExpression,
            /// <summary>The indexer</summary>
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
            Indexer,
            /// <summary>The null conditional q mark period</summary>
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
            NullConditionalQMarkPeriod,
            /// <summary>The null conditional q mark indexer</summary>
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
            NullConditionalQMarkIndexer,
            /// <summary>The invocation expression</summary>
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
            InvocationExpression,
            /// <summary>The index from end</summary>
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
            IndexFromEnd,
            /// <summary>The range</summary>
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
            Range


        }
    }
}
