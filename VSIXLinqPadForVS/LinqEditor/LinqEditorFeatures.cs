using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.BraceCompletion;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.Collections.Generic;
using System.ComponentModel.Composition;

using VSIXLinqPadForVS.LinqParser;

namespace VSIXLinqPadForVS.LinqEditor
{
    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(IClassificationTag))]
    [ContentType(Constants.PkgdefLanguageName)]
    public class LinqSyntaxHighligting : TokenClassificationTaggerBase
    {
        public override Dictionary<object, string> ClassificationMap { get; } = new()
        {
            { LinqItemType.RegistryKey, PredefinedClassificationTypeNames.SymbolDefinition },
            { LinqItemType.String, PredefinedClassificationTypeNames.String },
            { LinqItemType.Literal, PredefinedClassificationTypeNames.Literal },
            { LinqItemType.Comment, PredefinedClassificationTypeNames.Comment },
            { LinqItemType.Reference, PredefinedClassificationTypeNames.SymbolReference },
            { LinqItemType.Operator, PredefinedClassificationTypeNames.Operator },
            { LinqItemType.Preprocessor, PredefinedClassificationTypeNames.PreprocessorKeyword },
        };
    }

    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(IStructureTag))]
    [ContentType(Constants.PkgdefLanguageName)]
    public class LinqOutlining : TokenOutliningTaggerBase
    { }

    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(IErrorTag))]
    [ContentType(Constants.PkgdefLanguageName)]
    public class LinqErrorSquigglies : TokenErrorTaggerBase
    { }

    [Export(typeof(IAsyncQuickInfoSourceProvider))]
    [ContentType(Constants.PkgdefLanguageName)]
    internal sealed class LinqTooltips : TokenQuickInfoBase
    { }

    [Export(typeof(IBraceCompletionContextProvider))]
    [BracePair('(', ')')]
    [BracePair('[', ']')]
    [BracePair('{', '}')]
    [BracePair('"', '"')]
    [BracePair('$', '$')]
    [ContentType(Constants.PkgdefLanguageName)]
    [ProvideBraceCompletion(Constants.PkgdefLanguageName)]
    internal sealed class LinqBraceCompletion : BraceCompletionBase
    { }

    [Export(typeof(IAsyncCompletionCommitManagerProvider))]
    [ContentType(Constants.PkgdefLanguageName)]
    internal sealed class LinqCompletionCommitManager : CompletionCommitManagerBase
    {
        public override IEnumerable<char> CommitChars => new char[] { ' ', '\'', '"', ',', '.', ';', ':', '\\', '$' };
    }

    [Export(typeof(IViewTaggerProvider))]
    [TagType(typeof(TextMarkerTag))]
    [ContentType(Constants.PkgdefLanguageName)]
    internal sealed class LinqBraceMatchingTaggerProvider : BraceMatchingBase
    {
        // This will match parenthesis, curly brackets, and square brackets by default.
        // Override the BraceList property to modify the list of braces to match.
    }

    [Export(typeof(IViewTaggerProvider))]
    [ContentType(Constants.PkgdefLanguageName)]
    [TagType(typeof(TextMarkerTag))]
    public class LinqSameWordHighlighter : SameWordHighlighterBase
    { }
}
