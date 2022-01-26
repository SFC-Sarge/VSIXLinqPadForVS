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
// File Name        : LinqDocumentParser.cs
// License          : Open Source Apache License Version 2.0. Eee included License file.
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.TextManager.Interop;

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


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
    public partial class LinqDocument : IVsColorizer
    {
        /// <summary>The regex property</summary>
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
        private static readonly Regex _regexProperty = new(@"^(?<name>""[^""]+""|@)(\s)*(?<equals>=)\s*(?<value>((dword:|hex).+|"".+))", RegexOptions.Compiled);
        /// <summary>The regex reference</summary>
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
        private static readonly Regex _regexRef = new(@"\$[\w]+\$?", RegexOptions.Compiled);

        /// <summary>Parses this instance.</summary>
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
        public void Parse()
        {
            int start = 0;

            List<LinqParseItem> items = new();

            foreach (string line in _lines)
            {
                IEnumerable<LinqParseItem> current = ParseLine(start, line, items);
                items.AddRange(current);
                start += line.Length;
            }

            Items = items;
        }

        /// <summary>The current entry</summary>
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
        private LinqEntry _currentEntry = null;

        /// <summary>Parses the line.</summary>
        /// <param name="start">The start.</param>
        /// <param name="line">The line.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>IEnumerable&lt;LinqParseItem&gt;.</returns>
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
        private IEnumerable<LinqParseItem> ParseLine(int start, string line, List<LinqParseItem> tokens)
        {
            string trimmedLine = line.Trim();
            List<LinqParseItem> items = new();

            // Comment
            if (trimmedLine.StartsWith(Constants.CommentChars[0]) || trimmedLine.StartsWith(Constants.CommentChars[1]))
            {
                items.Add(ToParseItem(line, start, LinqItemType.Comment, false));
            }
            // Identifier
            else if (trimmedLine.StartsWith("<Query Kind="))
            {
                items.Add(ToParseItem(line, start, LinqItemType.Identifier, false));
            }
            // CSharp_Keywords
            else if (trimmedLine.StartsWith("using"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("namespace"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("public partial class"))
            {
                // Reference URL for Access Modifiers: ()
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("class") || trimmedLine.StartsWith("abstract class"))
            {
                // Reference URL for abstract class modifier: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract)
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("const") || trimmedLine.StartsWith("public const"))
            {
                // Reference URL for Access Modifiers: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const)
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("struct") || trimmedLine.StartsWith("enum"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("public async") || trimmedLine.StartsWith("private async"))
            {
                // Reference URL for Access Modifiers: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/async)
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("public event") || trimmedLine.StartsWith("private event") || trimmedLine.StartsWith("private protected event") || trimmedLine.StartsWith("internal event") || trimmedLine.StartsWith("protected internal event") || trimmedLine.StartsWith("protected event"))
            {
                // Reference URL for Access Modifiers: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/event)
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("public") || trimmedLine.StartsWith("private") || trimmedLine.StartsWith("private protected") || trimmedLine.StartsWith("internal") || trimmedLine.StartsWith("protected internal") || trimmedLine.StartsWith("protected") || trimmedLine.StartsWith("public abstract"))
            {
                // Reference URL for Access Modifiers: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/access-modifiers)
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("static"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("void"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("foreach"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("IEnumerable"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("Console") || trimmedLine.StartsWith("Debug"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.Number, false));
            }
            else if (trimmedLine.StartsWith("var"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("string"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("int"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("double"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.CSharp_Keywords, false));
            }
            else if (trimmedLine.StartsWith("Enumerable"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.Literal, false));
            }
            //Punctuation
            else if (trimmedLine.StartsWith("{"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.Punctuation, false));
            }
            else if (trimmedLine.StartsWith("}"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.Punctuation, false));
            }
            else if (trimmedLine.Contains("("))
            {
                items.Add(ToParseItem(line, start, LinqItemType.Punctuation, false));
            }
            else if (trimmedLine.Contains(")"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.Punctuation, false));
            }
            // Registry key
            else if (trimmedLine.StartsWith("["))
            {
                items.Add(ToParseItem(line, start, LinqItemType.Punctuation, false));
                //LinqParseItem key = new LinqParseItem(start, line, this, LinqItemType.RegistryKey);
                //_currentEntry = new LinqEntry(key, this);
                //items.Add(_currentEntry);
                //items.Add(key);
                //AddVariableReferences(key);
            }
            else if (trimmedLine.StartsWith("]"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.Punctuation, false));
                //LinqParseItem key = new LinqParseItem(start, line, this, LinqItemType.RegistryKey);
                //_currentEntry = new LinqEntry(key, this);
                //items.Add(_currentEntry);
                //items.Add(key);
                //AddVariableReferences(key);
            }
            // Property
            else if (tokens.Count > 0 && IsMatch(_regexProperty, trimmedLine, out Match matchHeader))
            {
                LinqParseItem name = ToParseItem(matchHeader, start, "name", false);
                LinqParseItem equals = ToParseItem(matchHeader, start, "equals", LinqItemType.Operator, false);
                LinqParseItem value = ToParseItem(matchHeader, start, "value");

                if (_currentEntry != null)
                {
                    LinqProperty prop = new LinqProperty(name, value);
                    _currentEntry.Properties.Add(prop);
                }

                items.Add(name);
                items.Add(equals);
                items.Add(value);
            }
            else if (trimmedLine.Length > 0 && trimmedLine.EndsWith(";", StringComparison.Ordinal))
            {
                items.Add(ToParseItem(line, start, LinqItemType.Preprocessor, false));
            }
            // Unknown
            else if (trimmedLine.Length > 0)
            {
                // Check for line splits which is a line ending with a backslash
                bool lineSplit = tokens.LastOrDefault()?.Text.TrimEnd().EndsWith("\\") == true;
                LinqItemType type = lineSplit ? tokens.Last().Type : LinqItemType.Unknown;
                items.Add(new LinqParseItem(start, line, this, type));
            }

            return items;
        }

        /// <summary>Determines whether the specified regex is match.</summary>
        /// <param name="regex">The regex.</param>
        /// <param name="line">The line.</param>
        /// <param name="match">The match.</param>
        /// <returns><c>true</c> if the specified regex is match; otherwise, <c>false</c>.</returns>
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
        public static bool IsMatch(Regex regex, string line, out Match match)
        {
            match = regex.Match(line);
            return match.Success;
        }

        /// <summary>To the parse item.</summary>
        /// <param name="line">The line.</param>
        /// <param name="start">The start.</param>
        /// <param name="type">The type.</param>
        /// <param name="supportsVariableReferences">if set to <c>true</c> [supports variable references].</param>
        /// <returns>LinqParseItem.</returns>
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
        private LinqParseItem ToParseItem(string line, int start, LinqItemType type, bool supportsVariableReferences = true)
        {
            LinqParseItem item = new LinqParseItem(start, line, this, type);

            if (supportsVariableReferences)
            {
                AddVariableReferences(item);
            }

            return item;
        }

        /// <summary>To the parse item.</summary>
        /// <param name="match">The match.</param>
        /// <param name="start">The start.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="type">The type.</param>
        /// <param name="supportsVariableReferences">if set to <c>true</c> [supports variable references].</param>
        /// <returns>LinqParseItem.</returns>
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
        private LinqParseItem ToParseItem(Match match, int start, string groupName, LinqItemType type, bool supportsVariableReferences = true)
        {
            Group group = match.Groups[groupName];
            return ToParseItem(group.Value, start + group.Index, type, supportsVariableReferences);
        }

        /// <summary>To the parse item.</summary>
        /// <param name="match">The match.</param>
        /// <param name="start">The start.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="supportsVariableReferences">if set to <c>true</c> [supports variable references].</param>
        /// <returns>LinqParseItem.</returns>
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
        private LinqParseItem ToParseItem(Match match, int start, string groupName, bool supportsVariableReferences = true)
        {
            Group group = match.Groups[groupName];
            LinqItemType type = group.Value.StartsWith("\"") ? LinqItemType.String : LinqItemType.Literal;
            return ToParseItem(group.Value, start + group.Index, type, supportsVariableReferences);
        }

        /// <summary>Adds the variable references.</summary>
        /// <param name="token">The token.</param>
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
        private void AddVariableReferences(LinqParseItem token)
        {
            foreach (Match match in _regexRef.Matches(token.Text))
            {
                LinqParseItem reference = ToParseItem(match.Value, token.Span.Start + match.Index, LinqItemType.Reference, false);
                token.References.Add(reference);
            }
        }

        /// <summary>Gets the state maintenance flag.</summary>
        /// <param name="pfFlag">The pf flag.</param>
        /// <returns>System.Int32.</returns>
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
        /// <exception cref="System.NotImplementedException"></exception>
        public int GetStateMaintenanceFlag(out int pfFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>Gets the start state.</summary>
        /// <param name="piStartState">Start state of the pi.</param>
        /// <returns>System.Int32.</returns>
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
        /// <exception cref="System.NotImplementedException"></exception>
        public int GetStartState(out int piStartState)
        {
            throw new NotImplementedException();
        }

        /// <summary>Colorizes the line.</summary>
        /// <param name="iLine">The i line.</param>
        /// <param name="iLength">Length of the i.</param>
        /// <param name="pszText">The PSZ text.</param>
        /// <param name="iState">State of the i.</param>
        /// <param name="pAttributes">The p attributes.</param>
        /// <returns>System.Int32.</returns>
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
        /// <exception cref="System.NotImplementedException"></exception>
        public int ColorizeLine(int iLine, int iLength, IntPtr pszText, int iState, uint[] pAttributes)
        {
            throw new NotImplementedException();
        }

        /// <summary>Gets the state at end of line.</summary>
        /// <param name="iLine">The i line.</param>
        /// <param name="iLength">Length of the i.</param>
        /// <param name="pText">The p text.</param>
        /// <param name="iState">State of the i.</param>
        /// <returns>System.Int32.</returns>
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
        /// <exception cref="System.NotImplementedException"></exception>
        public int GetStateAtEndOfLine(int iLine, int iLength, IntPtr pText, int iState)
        {
            throw new NotImplementedException();
        }

        /// <summary>Closes the colorizer.</summary>
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
        /// <exception cref="System.NotImplementedException"></exception>
        public void CloseColorizer()
        {
            throw new NotImplementedException();
        }
    }
}
