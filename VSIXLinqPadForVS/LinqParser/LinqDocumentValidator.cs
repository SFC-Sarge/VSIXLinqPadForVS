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
// File Name        : LinqDocumentValidator.cs
// License          : Open Source Apache License Version 2.0. Eee included License file.
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.Shell.Interop;

using System.Linq;

using type = Microsoft.VisualStudio.Text.Adornments.PredefinedErrorTypeNames;

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
    /// <summary>Class LinqDocument</summary>
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
    public partial class LinqDocument
    {
        /// <summary>Gets a value indicating whether this instance is valid.</summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
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
        public bool IsValid { get; private set; }

        /// <summary>Class LinqErrors</summary>
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
        private class LinqErrors
        {
            /// <summary>Gets the p L001.</summary>
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
            public static LinqError PL001 { get; } = new("PL001", "Unknown token at this location", type.SyntaxError, __VSERRORCATEGORY.EC_ERROR);
            /// <summary>Gets the p L002.</summary>
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
            public static LinqError PL002 { get; } = new("PL002", "Unclosed registry key entry. Add the missing ] character", type.SyntaxError, __VSERRORCATEGORY.EC_ERROR);
            /// <summary>Gets the p L003.</summary>
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
            public static LinqError PL003 { get; } = new("PL003", "Use the backslash character as delimiter instead of forward slash.", type.SyntaxError, __VSERRORCATEGORY.EC_ERROR);
            /// <summary>Gets the p L004.</summary>
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
            public static LinqError PL004 { get; } = new("PL004", "To set a registry key's default value, use '@' without quotation marks", type.Warning, __VSERRORCATEGORY.EC_WARNING);
            /// <summary>Gets the p L005.</summary>
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
            public static LinqError PL005 { get; } = new("PL005", "Value names must be enclosed in quotation marks.", type.SyntaxError, __VSERRORCATEGORY.EC_ERROR);
            /// <summary>Gets the p L006.</summary>
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
            public static LinqError PL006 { get; } = new("PL006", "The variable \"{0}\" doesn't exist.", type.Warning, __VSERRORCATEGORY.EC_WARNING);
            /// <summary>Gets the p L007.</summary>
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
            public static LinqError PL007 { get; } = new("PL007", "Variables must begin and end with $ character.", type.SyntaxError, __VSERRORCATEGORY.EC_ERROR);
            /// <summary>Gets the p L008.</summary>
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
            public static LinqError PL008 { get; } = new("PL008", "This registry key \"{0}\" was already defined earlier in the document", type.Suggestion, __VSERRORCATEGORY.EC_MESSAGE);
        }

        /// <summary>Adds the error.</summary>
        /// <param name="item">The item.</param>
        /// <param name="error">The error.</param>
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
        private void AddError(LinqParseItem item, LinqError error)
        {
            item.Errors.Add(error);
            IsValid = false;
        }

        /// <summary>Linqs the validate document.</summary>
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
        private void LinqValidateDocument()
        {
            IsValid = true;

            foreach (LinqParseItem item in Items)
            {
                // Unknown symbols
                if (item.Type == LinqItemType.Unknown)
                {
                    AddError(item, LinqErrors.PL001);
                }

                // Registry key
                if (item.Type == LinqItemType.RegistryKey)
                {
                    string trimmedText = item.Text.Trim();

                    if (!trimmedText.EndsWith("]"))
                    {
                        AddError(item, LinqErrors.PL002);
                    }
                    //else if (trimmedText.Contains("/") && !trimmedText.Contains("\\/"))
                    //{
                    //    AddError(item, Errors.PL003);
                    //}

                    int index = Items.IndexOf(item);
                    if (Items.Take(index).Any(i => i.Type == LinqItemType.RegistryKey && item.Text == i.Text))
                    {
                        AddError(item, LinqErrors.PL008.WithFormat(trimmedText));
                    }
                }

                // Properties
                else if (item.Type == LinqItemType.Operator)
                {
                    LinqParseItem name = item.Previous;
                    LinqParseItem value = item.Next;

                    if (name?.Type == LinqItemType.String)
                    {
                        if (name.Text == "\"@\"")
                        {
                            AddError(name, LinqErrors.PL004);
                        }
                    }
                    else if (name?.Type == LinqItemType.Literal && name?.Text != "@")
                    {
                        AddError(name, LinqErrors.PL005);
                    }
                }

                // Make sure strings are correctly closed with quotation mark
                if (item.Type == LinqItemType.String)
                {
                    if (!item.Text.EndsWith("\""))
                    {
                        AddError(item, LinqErrors.PL005);
                    }
                }

                // References
                foreach (LinqParseItem reference in item.References)
                {
                    string refTrim = reference.Text.Trim();

                    //if (!refTrim.EndsWith("$"))
                    //{
                    //    AddError(reference, Errors.PL007);
                    //}
                    //else
                    //{
                    if (refTrim.EndsWith("$"))
                    {
                        if (!LinqPredefinedVariables.Variables.Any(v => v.Key.Equals(refTrim.Trim('$'), StringComparison.OrdinalIgnoreCase)))
                        {
                            AddError(reference, LinqErrors.PL006.WithFormat(refTrim));
                        }
                    }
                    //}
                }
            }
        }
    }
}
