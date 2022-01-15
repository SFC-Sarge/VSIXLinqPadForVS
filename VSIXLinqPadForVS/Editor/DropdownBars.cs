﻿using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Collections;
using System.Linq;
using VSIXLinqPadForVS.Parser;

namespace VSIXLinqPadForVS.Editor
{
    internal class DropdownBars : TypeAndMemberDropdownBars, IDisposable
    {
        private readonly LanguageService _languageService;
        private readonly IWpfTextView _textView;
        private readonly Document _document;
        private bool _disposed;
        private bool _bufferHasChanged;

        public DropdownBars(IVsTextView textView, LanguageService languageService) : base(languageService)
        {
            _languageService = languageService;
            _textView = textView.ToIWpfTextView();
            _document = _textView.TextBuffer.GetDocument();
            _document.Processed += OnDocumentProcessed;

            InitializeAsync(textView).FireAndForget();
        }

        // This moves the caret to trigger initial drop down load
        private Task InitializeAsync(IVsTextView textView)
        {
            return ThreadHelper.JoinableTaskFactory.StartOnIdle(() =>
            {
                textView.SendExplicitFocus();
                _textView.Caret.MoveToNextCaretPosition();
                _textView.Caret.PositionChanged += CaretPositionChanged;
                _textView.Caret.MoveToPreviousCaretPosition();
            }).Task;
        }

        private void CaretPositionChanged(object sender, CaretPositionChangedEventArgs e) => SynchronizeDropdowns();
        private void OnDocumentProcessed(Document document)
        {
            _bufferHasChanged = true;
            SynchronizeDropdowns();
        }

        private void SynchronizeDropdowns()
        {
            if (_document.IsProcessing)
            {
                return;
            }

            _ = ThreadHelper.JoinableTaskFactory.StartOnIdle(() =>
            {
                _languageService.SynchronizeDropdowns();
            }, VsTaskRunContext.UIThreadIdlePriority);
        }

        public override bool OnSynchronizeDropdowns(LanguageService languageService, IVsTextView textView, int line, int col, ArrayList dropDownTypes, ArrayList dropDownMembers, ref int selectedType, ref int selectedMember)
        {
            if (_bufferHasChanged || dropDownMembers.Count == 0)
            {
                dropDownMembers.Clear();

                _document.Items.OfType<Entry>()
                    .Select(entry => CreateDropDownMember(entry, textView))
                    .ToList()
                    .ForEach(ddm => dropDownMembers.Add(ddm));
            }

            if (dropDownTypes.Count == 0)
            {
                string thisExt = $"{Vsix.Name} ({Vsix.Version})";
                dropDownTypes.Add(new DropDownMember(thisExt, new TextSpan(), 126, DROPDOWNFONTATTR.FONTATTR_GRAY));
            }

            DropDownMember currentDropDown = dropDownMembers
                .OfType<DropDownMember>()
                .Where(d => d.Span.iStartLine <= line)
                .LastOrDefault();

            selectedMember = dropDownMembers.IndexOf(currentDropDown);
            selectedType = 0;
            _bufferHasChanged = false;

            return true;
        }

        private static DropDownMember CreateDropDownMember(Entry entry, IVsTextView textView)
        {
            TextSpan textSpan = GetTextSpan(entry.RegistryKey, textView);
            string text = entry.RegistryKey.Text.Trim();
            return new DropDownMember(text, textSpan, 126, DROPDOWNFONTATTR.FONTATTR_PLAIN);
        }

        private static TextSpan GetTextSpan(ParseItem item, IVsTextView textView)
        {
            TextSpan textSpan = new();

            textView.GetLineAndColumn(item.Span.Start, out textSpan.iStartLine, out textSpan.iStartIndex);
            textView.GetLineAndColumn(item.Span.End + 1, out textSpan.iEndLine, out textSpan.iEndIndex);

            return textSpan;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _textView.Caret.PositionChanged -= CaretPositionChanged;
            _document.Processed -= OnDocumentProcessed;
        }
    }
}
