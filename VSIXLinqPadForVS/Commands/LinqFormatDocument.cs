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
// File Name        : LinqFormatDocument.cs
// License          : Open Source Apache License Version 2.0. Eee included License file.
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.Text;

using System.Linq;
using System.Text;

using VSIXLinqPadForVS.LinqParser;

/// <summary>
/// The Commands namespace.
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
namespace VSIXLinqPadForVS.Commands
{
    /// <summary>Class LinqFormatDocument</summary>
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
    public class LinqFormatDocument
    {
        /// <summary>Initializes the asynchronous.</summary>
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
        public static async Task InitializeAsync()
        {
            // We need to manually intercept the FormatDocument command, because language services swallow formatting commands.
            await VS.Commands.InterceptAsync(Microsoft.VisualStudio.VSConstants.VSStd2KCmdID.FORMATDOCUMENT, () =>
            {
                return ThreadHelper.JoinableTaskFactory.Run(async () =>
                {
                    DocumentView doc = await VS.Documents.GetActiveDocumentViewAsync();

                    if (doc?.TextBuffer != null && doc.TextBuffer.ContentType.IsOfType(Constants.LinqLanguageName))
                    {
                        LinqFormat(doc.TextBuffer);
                        return CommandProgression.Stop;
                    }

                    return CommandProgression.Continue;
                });
            });
        }

        /// <summary>Linqs the format.</summary>
        /// <param name="buffer">The buffer.</param>
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
        private static void LinqFormat(ITextBuffer buffer)
        {
            LinqDocument doc = buffer.GetDocument();
            StringBuilder sb = new();

            foreach (LinqParseItem item in doc.Items)
            {
                if (item is LinqEntry entry)
                {
                    bool insertLineBefore = true;

                    if (!entry.Properties.Any() && NextEntry(entry) is LinqEntry next)
                    {
                        string currentKey = entry.RegistryKey.Text.Trim().TrimEnd(']');
                        string nextKey = next.RegistryKey.Text.Trim().TrimEnd(']');

                        if (nextKey.IndexOf(currentKey, StringComparison.OrdinalIgnoreCase) > -1)
                        {
                            insertLineBefore = false;
                        }
                    }

                    sb.AppendLine();

                    if (insertLineBefore)
                    {
                        sb.AppendLine(entry.GetFormattedText());
                    }
                    else
                    {
                        sb.Append(entry.GetFormattedText());
                    }

                }
                else if (item.Type == LinqItemType.Preprocessor)
                {
                    sb.AppendLine(item.Text.Trim());
                }
                else if (item.Type == LinqItemType.Literal)
                {
                    sb.AppendLine(item.Text.Trim());
                }
                else if (item.Type == LinqItemType.Punctuation)
                {
                    sb.AppendLine(item.Text.Trim());
                }
                else if (item.Type == LinqItemType.Comment)
                {
                    sb.AppendLine(item.Text.Trim());
                }
                else if (item.Type == LinqItemType.CSharp_Keywords)
                {
                    sb.AppendLine(item.Text.Trim());
                }
                else if (item.Type == LinqItemType.Identifier)
                {
                    sb.AppendLine(item.Text.Trim());
                }
                else if (item.Type == LinqItemType.Number)
                {
                    sb.AppendLine(item.Text.Trim());
                }
                else if (item.Type == LinqItemType.CSharp_ContextualKeywords)
                {
                    sb.AppendLine(item.Text.Trim());
                }
            }

            Span wholeDocSpan = new Span(0, buffer.CurrentSnapshot.Length);
            buffer.Replace(wholeDocSpan, sb.ToString());
        }

        /// <summary>Nexts the entry.</summary>
        /// <param name="current">The current.</param>
        /// <returns>LinqEntry.</returns>
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
        private static LinqEntry NextEntry(LinqEntry current)
        {
            return current.Document.Items
                .OfType<LinqEntry>()
                .FirstOrDefault(e => e.Span.Start >= current.Span.End);
        }
    }
}