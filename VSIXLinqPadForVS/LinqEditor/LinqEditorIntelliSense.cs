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
// File Name        : LinqEditorIntelliSense.cs
// License          : Open Source Apache License Version 2.0. Eee included License file.
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.Core.Imaging;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion;
using Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion.Data;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Microsoft.Win32;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using VSIXLinqPadForVS.LinqParser;

/// <summary>
/// The LinqEditor namespace.
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
namespace VSIXLinqPadForVS.LinqEditor
{
    /// <summary>Class LinqEditorIntelliSense</summary>
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
    [Export(typeof(IAsyncCompletionSourceProvider))]
    [Name(Constants.LinqLanguageName)]
    [ContentType(Constants.LinqLanguageName)]
    public class LinqEditorIntelliSense : IAsyncCompletionSourceProvider
    {
        /// <summary>Creates an instance of <see cref="T:Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion.IAsyncCompletionSource" /> for the specified <see cref="T:Microsoft.VisualStudio.Text.Editor.ITextView" />.
        /// Called on the UI thread.</summary>
        /// <param name="textView">Text view that will host the completion. Completion acts on buffers of this view.</param>
        /// <returns>Instance of <see cref="T:Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion.IAsyncCompletionSource" /></returns>
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
        public IAsyncCompletionSource GetOrCreate(ITextView textView) =>
            textView.Properties.GetOrCreateSingletonProperty(() => new AsyncLinqCompletionSource());
    }

    /// <summary>Class AsyncLinqCompletionSource</summary>
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
    public class AsyncLinqCompletionSource : IAsyncCompletionSource
    {
        /// <summary>The reference icon</summary>
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
        private static readonly ImageElement _referenceIcon = new(KnownMonikers.LocalVariable.ToImageId(), "Variable");

        /// <summary>Gets the completion context asynchronous.</summary>
        /// <param name="session">The session.</param>
        /// <param name="trigger">The trigger.</param>
        /// <param name="triggerLocation">The trigger location.</param>
        /// <param name="applicableToSpan">The applicable to span.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;CompletionContext&gt;.</returns>
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
        public Task<CompletionContext> GetCompletionContextAsync(IAsyncCompletionSession session, CompletionTrigger trigger, SnapshotPoint triggerLocation, SnapshotSpan applicableToSpan, CancellationToken cancellationToken)
        {
            LinqDocument document = session.TextView.TextBuffer.GetDocument();
            LinqParseItem item = document.FindItemFromPosition(triggerLocation.Position);
            IEnumerable<CompletionItem> items = null;

            if (item?.Type == LinqItemType.Reference)
            {
                items = GetReferenceCompletion();
            }
            else if (item?.Type == LinqItemType.RegistryKey)
            {
                items = GetRegistryKeyCompletion(item, triggerLocation);
            }

            return Task.FromResult(items == null ? null : new CompletionContext(items.ToImmutableArray()));
        }

        /// <summary>Gets the registry key completion.</summary>
        /// <param name="item">The item.</param>
        /// <param name="triggerLocation">The trigger location.</param>
        /// <returns>IEnumerable&lt;CompletionItem&gt;.</returns>
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
        private IEnumerable<CompletionItem> GetRegistryKeyCompletion(LinqParseItem item, SnapshotPoint triggerLocation)
        {
            ITextSnapshotLine line = triggerLocation.GetContainingLine();
            int column = triggerLocation.Position - line.Start - 1;
            int previousKey = item.Text.LastIndexOf('\\', column);

            if (previousKey > -1)
            {
                IEnumerable<string> prevKeys = item.Text.Substring(0, previousKey).Split('\\').Skip(1);
                RegistryKey root = VSRegistry.RegistryRoot(__VsLocalRegistryType.RegType_Configuration);
                RegistryKey parent = root;

                foreach (string subKey in prevKeys)
                {
                    parent = parent.OpenSubKey(subKey);
                }

                return parent?.GetSubKeyNames()?.Select(s => new CompletionItem(s, this, _referenceIcon));
            }

            return null;
        }

        /// <summary>Gets the reference completion.</summary>
        /// <returns>IEnumerable&lt;CompletionItem&gt;.</returns>
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
        private IEnumerable<CompletionItem> GetReferenceCompletion()
        {
            foreach (string key in LinqPredefinedVariables.Variables.Keys)
            {
                CompletionItem completion = new CompletionItem(key, this, _referenceIcon, ImmutableArray<CompletionFilter>.Empty, "", $"${key}$", key, key, ImmutableArray<ImageElement>.Empty);
                completion.Properties.AddProperty("description", LinqPredefinedVariables.Variables[key]);
                yield return completion;
            }
        }

        /// <summary>Returns tooltip associated with provided <see cref="T:Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion.Data.CompletionItem" />.
        /// The returned object will be rendered by <see cref="T:Microsoft.VisualStudio.Text.Adornments.IViewElementFactoryService" />. See its documentation for default supported types.
        /// You may export a <see cref="T:Microsoft.VisualStudio.Text.Adornments.IViewElementFactory" /> to provide a renderer for a custom type.
        /// Since this method is called on a background thread and on multiple platforms, an instance of UIElement may not be returned.</summary>
        /// <param name="session">Reference to the active <see cref="T:Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion.IAsyncCompletionSession" /></param>
        /// <param name="item"><see cref="T:Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion.Data.CompletionItem" /> which is a subject of the tooltip</param>
        /// <param name="token">Cancellation token that may interrupt this operation</param>
        /// <returns>An object that will be passed to <see cref="T:Microsoft.VisualStudio.Text.Adornments.IViewElementFactoryService" />. See its documentation for supported types.</returns>
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
        public Task<object> GetDescriptionAsync(IAsyncCompletionSession session, CompletionItem item, CancellationToken token)
        {
            if (item.Properties.TryGetProperty("description", out string description))
            {
                return Task.FromResult<object>(description);
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>Initializes the completion.</summary>
        /// <param name="trigger">The trigger.</param>
        /// <param name="triggerLocation">The trigger location.</param>
        /// <param name="token">The token.</param>
        /// <returns>CompletionStartData.</returns>
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
        public CompletionStartData InitializeCompletion(CompletionTrigger trigger, SnapshotPoint triggerLocation, CancellationToken token)
        {
            if (trigger.Character == '\n' || trigger.Character == ']' || trigger.Reason == CompletionTriggerReason.Deletion)
            {
                return CompletionStartData.DoesNotParticipateInCompletion;
            }

            LinqDocument document = triggerLocation.Snapshot.TextBuffer.GetDocument();
            LinqParseItem item = document?.FindItemFromPosition(triggerLocation.Position);

            if (item?.Type == LinqItemType.Reference)
            {
                SnapshotSpan tokenSpan = new SnapshotSpan(triggerLocation.Snapshot, item);
                return new CompletionStartData(CompletionParticipation.ProvidesItems, tokenSpan);
            }
            else if (item?.Type == LinqItemType.RegistryKey && item.Text.IndexOf("var", StringComparison.OrdinalIgnoreCase) > -1)
            {
                int column = triggerLocation.Position - item.Span.Start;

                if (column < 1)
                {
                    return CompletionStartData.DoesNotParticipateInCompletion; ;
                }

                int start = item.Text.LastIndexOf('\\', column - 1) + 1;
                int end = item.Text.IndexOf('\\', column);
                int close = item.Text.IndexOf('}', column);
                int textEnd = item.Text.IndexOf('}', column);
                end = end >= start ? end : close;
                end = end >= start ? end : textEnd;
                end = end >= start ? end : item.Text.TrimEnd().Length;

                if (end >= start)
                {
                    if (close == -1 || column <= close)
                    {
                        SnapshotSpan span = new SnapshotSpan(triggerLocation.Snapshot, item.Span.Start + start, end - start);
                        return new CompletionStartData(CompletionParticipation.ProvidesItems, span);
                    }
                }
            }

            return CompletionStartData.DoesNotParticipateInCompletion;
        }
    }
}