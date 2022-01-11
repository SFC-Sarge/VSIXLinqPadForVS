﻿using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Commanding;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Editor.Commanding.Commands;
using Microsoft.VisualStudio.Utilities;
using VSIXLinqPadForVS;

namespace LinqEditor2022
{
    [Export(typeof(ICommandHandler))]
    [Name(nameof(ToggleTaskCommand))]
    [ContentType(Constants.LanguageName)]
    [TextViewRole(PredefinedTextViewRoles.PrimaryDocument)]
    public class ToggleTaskCommand : ICommandHandler<CommitUniqueCompletionListItemCommandArgs>
    {
        private static readonly Regex _regex = new(@"\* \[( |x|X)\]", RegexOptions.Compiled);
        public string DisplayName => GetType().Name;

        public bool ExecuteCommand(CommitUniqueCompletionListItemCommandArgs args, CommandExecutionContext executionContext)
        {
            int position = args.TextView.Caret.Position.BufferPosition.Position;
            ITextSnapshotLine line = args.TextView.TextBuffer.CurrentSnapshot.GetLineFromPosition(position);

            string lineText = line.GetText();
            Match match = _regex.Match(lineText);

            if (match.Success)
            {
                Span span = new(line.Start + match.Index, match.Length);

                if (match.Value.Contains("[ ]"))
                {
                    line.Snapshot.TextBuffer.Replace(span, "* [x]");
                }
                else
                {
                    line.Snapshot.TextBuffer.Replace(span, "* [ ]");
                }

                return true;
            }

            return false;
        }

        public CommandState GetCommandState(CommitUniqueCompletionListItemCommandArgs args)
        {
            return CommandState.Available;
        }
    }
}