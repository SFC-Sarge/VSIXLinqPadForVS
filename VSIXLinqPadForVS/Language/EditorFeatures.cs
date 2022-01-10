using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinqEditor2022;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion;
using Microsoft.VisualStudio.Shell.TableManager;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.BraceCompletion;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Threading;
using Microsoft.VisualStudio.Utilities;

namespace VSIXLinqPadForVS
{
    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(IClassificationTag))]
    [ContentType(Constants.LanguageName)]
    public class SyntaxHighligting : TokenClassificationTaggerBase
    {
        public override Dictionary<object, string> ClassificationMap { get; } = new()
        {
            { LinqEditorClassifierClassificationDefinition.LinqComment, LinqEditorClassifierClassificationDefinition.LinqComment },
            { LinqEditorClassifierClassificationDefinition.LinqItalic, LinqEditorClassifierClassificationDefinition.LinqItalic },
            { LinqEditorClassifierClassificationDefinition.LinqBold, LinqEditorClassifierClassificationDefinition.LinqBold },
        };
    }

    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(IStructureTag))]
    [ContentType(Constants.LanguageName)]
    public class Outlining : TokenOutliningTaggerBase
    { }

    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(IErrorTag))]
    [ContentType(Constants.LanguageName)]
    public class ErrorSquigglies : TokenErrorTaggerBase
    { }

    [Export(typeof(IAsyncQuickInfoSourceProvider))]
    [ContentType(Constants.LanguageName)]
    internal sealed class Tooltips : TokenQuickInfoBase
    { }

    [Export(typeof(IBraceCompletionContextProvider))]
    [BracePair('(', ')')]
    [BracePair('[', ']')]
    [BracePair('{', '}')]
    [BracePair('"', '"')]
    [BracePair('*', '*')]
    [ContentType(Constants.LanguageName)]
    [ProvideBraceCompletion(Constants.LanguageName)]
    internal sealed class BraceCompletion : BraceCompletionBase
    { }

    [Export(typeof(IAsyncCompletionCommitManagerProvider))]
    [ContentType(Constants.LanguageName)]
    internal sealed class CompletionCommitManager : CompletionCommitManagerBase
    {
        public override IEnumerable<char> CommitChars => new char[] { ' ', '\'', '"', ',', '.', ';', ':', '\\', '$' };
    }

    [Export(typeof(IViewTaggerProvider))]
    [TagType(typeof(TextMarkerTag))]
    [ContentType(Constants.LanguageName)]
    internal sealed class BraceMatchingTaggerProvider : BraceMatchingBase
    {
        // This will match parenthesis, curly brackets, and square brackets by default.
        // Override the BraceList property to modify the list of braces to match.
    }

    [Export(typeof(IViewTaggerProvider))]
    [ContentType(Constants.LanguageName)]
    [TagType(typeof(TextMarkerTag))]
    public class SameWordHighlighter : SameWordHighlighterBase
    { }

    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType(Constants.LanguageName)]
    [TextViewRole(PredefinedTextViewRoles.PrimaryDocument)]
    public class HideMargings : WpfTextViewCreationListener
    {
        private readonly Regex _taskRegex = new(@"(?<keyword>TODO|HACK|UNDONE):(?<phrase>.+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private TableDataSource _dataSource;
        private DocumentView _docView;

        [Import] internal IBufferTagAggregatorFactoryService _bufferTagAggregator = null;
        private Document _document;

        protected override void Created(DocumentView docView)
        {
            _document = docView.TextBuffer.GetDocument();
_docView ??= docView;
            _dataSource ??= new TableDataSource(docView.TextBuffer.ContentType.DisplayName);

            _docView.TextView.Options.SetOptionValue(DefaultTextViewHostOptions.GlyphMarginName, false);
            _docView.TextView.Options.SetOptionValue(DefaultTextViewHostOptions.SelectionMarginName, true);
            _docView.TextView.Options.SetOptionValue(DefaultTextViewHostOptions.ShowEnhancedScrollBarOptionName, false);

            _document.Parsed += OnParsed;
        }

        private void OnParsed(Document document)
        {
            throw new NotImplementedException();
        }


        protected override void Closed(IWpfTextView textView)
        {
            _dataSource.CleanAllErrors();
            _document.Parsed -= OnParsed;
        }
    }
}

