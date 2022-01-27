using Microsoft.VisualStudio.Core.Imaging;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

using VSIXLinqPadForVS.LinqParser;

namespace VSIXLinqPadForVS.LinqEditor
{
    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(TokenTag))]
    [ContentType(Constants.LinqLanguageName)]
    [Name(Constants.LinqLanguageName)]
    internal sealed class LinqTokenTaggerProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag =>
            buffer.Properties.GetOrCreateSingletonProperty(() => new LinqTokenTagger(buffer)) as ITagger<T>;
    }

    internal class LinqTokenTagger : TokenTaggerBase, IDisposable
    {
        private readonly LinqDocument _document;
        private static readonly ImageId _errorIcon = KnownMonikers.StatusWarningNoColor.ToImageId();
        private bool _isDisposed;

        internal LinqTokenTagger(ITextBuffer buffer) : base(buffer)
        {
            _document = buffer.GetDocument();
            _document.Processed += LinqDocumentProcessed;
        }

        private void LinqDocumentProcessed(LinqDocument document)
        {
            _ = TokenizeAsync();
        }

        public override Task TokenizeAsync()
        {
            List<ITagSpan<TokenTag>> list = new();

            foreach (LinqParseItem item in _document.Items)
            {
                if (_document.IsProcessing)
                {
                    // Abort and wait for the next parse event to finish
                    return Task.CompletedTask;
                }

                AddTagToList(list, item);

                foreach (LinqParseItem variable in item.References)
                {
                    AddTagToList(list, variable);
                }
            }

            OnTagsUpdated(list);
            return Task.CompletedTask;
        }

        private void AddTagToList(List<ITagSpan<TokenTag>> list, LinqParseItem item)
        {
            bool hasTooltip = !item.IsValid;
            bool supportsOutlining = item is LinqEntry entry && entry.Properties.Any();
            IEnumerable<ErrorListItem> errors = CreateErrorListItems(item);

            TokenTag tag = CreateToken(item.Type, hasTooltip, supportsOutlining, errors);

            SnapshotSpan span = new(Buffer.CurrentSnapshot, item);
            list.Add(new TagSpan<TokenTag>(span, tag));
        }

        private IEnumerable<ErrorListItem> CreateErrorListItems(LinqParseItem item)
        {
            ITextSnapshotLine line = Buffer.CurrentSnapshot.GetLineFromPosition(item.Span.Start);

            foreach (LinqError error in item.Errors)
            {
                yield return new ErrorListItem
                {
                    ProjectName = _document.ProjectName ?? "",
                    FileName = _document.FileName,
                    Message = error.Message,
                    ErrorCategory = error.Category,
                    Severity = error.Severity,
                    Line = line.LineNumber,
                    Column = item.Span.Start - line.Start.Position,
                    BuildTool = Vsix.Name,
                    ErrorCode = error.ErrorCode
                };
            }
        }

        public override Task<object> GetTooltipAsync(SnapshotPoint triggerPoint)
        {
            LinqParseItem item = _document.FindItemFromPosition(triggerPoint.Position);

            // Error messages
            if (item?.IsValid == false)
            {
                ContainerElement elm = new(
                    ContainerElementStyle.Wrapped,
                    new ImageElement(_errorIcon),
                    string.Join(Environment.NewLine, item.Errors.Select(e => e.Message)));

                return Task.FromResult<object>(elm);
            }

            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _document.Processed -= LinqDocumentProcessed;
                _document.Dispose();
            }

            _isDisposed = true;
        }
    }
}
