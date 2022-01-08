﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Microsoft.VisualStudio.Core.Imaging;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Threading;
using Microsoft.VisualStudio.Utilities;

namespace VSIXLinqPadForVS
{
    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(TokenTag))]
    [ContentType(Constants.LanguageName)]
    [Name(Constants.LanguageName)]
    internal sealed class TokenTaggerProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag =>
            buffer.Properties.GetOrCreateSingletonProperty(() => new TokenTagger(buffer)) as ITagger<T>;
    }

    internal class TokenTagger : ITagger<TokenTag>, IDisposable
    {
        private readonly Document _document;
        private readonly ITextBuffer _buffer;
        private Dictionary<MarkdownObject, ITagSpan<TokenTag>> _tagsCache;
        private bool _isDisposed;
        private static readonly ImageId _errorIcon = KnownMonikers.StatusWarning.ToImageId();

        internal TokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
            _document = buffer.GetDocument();
            _document.Parsed += ReParse;
            _tagsCache = new Dictionary<MarkdownObject, ITagSpan<TokenTag>>();

            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await TaskScheduler.Default;
                ReParse(_document);
            }).FireAndForget();
        }

        public IEnumerable<ITagSpan<TokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            return _tagsCache.Values;
        }

        private void ReParse(Document document)
        {
            // Make sure this is running on a background thread.
            ThreadHelper.ThrowIfOnUIThread();

            Dictionary<MarkdownObject, ITagSpan<TokenTag>> list = new();

            foreach (MarkdownObject item in document.Markdown.Descendants())
            {
                if (document.IsParsing)
                {
                    // Abort and wait for the next parse event to finish
                    return;
                }

                AddTagToList(list, item);
            }

            _tagsCache = list;

            SnapshotSpan span = new(_buffer.CurrentSnapshot, 0, _buffer.CurrentSnapshot.Length);
            TagsChanged?.Invoke(this, new SnapshotSpanEventArgs(span));
        }

        private void AddTagToList(Dictionary<MarkdownObject, ITagSpan<TokenTag>> list, MarkdownObject item)
        {
            SnapshotSpan span = new(_buffer.CurrentSnapshot, GetApplicapleSpan(item));
            TokenTag tag = new(GetItemType(item), item is FencedCodeBlock)
            {
                Errors = item.GetErrors(_document.FileName).ToList(),
                GetTooltipAsync = GetTooltipAsync,
                GetOutliningText = GetOutliningText
            };

            list.Add(item, new TagSpan<TokenTag>(span, tag));
        }

        private static string GetOutliningText(string text)
        {
            string firstLine = text.Split('\n').FirstOrDefault()?.Trim();
            string language = "";

            if (firstLine.Length > 3)
            {
                language = " " + firstLine.Substring(3).Trim().ToUpperInvariant();
            }

            return $"{language} Code Block ";
        }

        private Task<object> GetTooltipAsync(SnapshotPoint triggerPoint)
        {
            LinkInline item = _document.Markdown.Descendants()
                .OfType<LinkInline>()
                .Where(l => l.Span.Start <= triggerPoint.Position && l.Span.End >= triggerPoint.Position)
                .FirstOrDefault();

            // Error messages
            IEnumerable<ErrorListItem> errors = item?.GetErrors(_document.FileName);
            if (errors != null && errors.Any())
            {
                ContainerElement elm = new(
                    ContainerElementStyle.Wrapped,
                    new ImageElement(_errorIcon),
                    string.Join(Environment.NewLine, errors.Select(e => e.Message))
                );

                return Task.FromResult<object>(elm);
            }

            return Task.FromResult<object>(null);
        }

        private static Span GetApplicapleSpan(MarkdownObject mdobj)
        {
            if (mdobj is LinkInline link && link.UrlSpan.HasValue)
            {
                if (link.Reference == null)
                {
                    return new Span(link.UrlSpan.Value.Start, link.UrlSpan.Value.Length);
                }

                return new Span(link.LabelSpan.Value.Start, link.LabelSpan.Value.Length);
            }

            return mdobj.ToSpan();
        }

        private static string GetItemType(MarkdownObject mdobj)
        {
            return mdobj switch
            {
                HeadingBlock => LinqClassificationTypes.MarkdownHeader,
                CodeBlock or CodeInline => LinqClassificationTypes.MarkdownCode,
                QuoteBlock => LinqClassificationTypes.MarkdownQuote,
                LinkInline => LinqClassificationTypes.MarkdownLink,
                EmphasisInline ei when ei.DelimiterCount == 2 && ei.DelimiterChar == '~' => LinqClassificationTypes.MarkdownStrikethrough,
                EmphasisInline ei when ei.DelimiterCount == 1 => LinqClassificationTypes.MarkdownItalic,
                EmphasisInline ei when ei.DelimiterCount == 2 => LinqClassificationTypes.MarkdownBold,
                HtmlBlock html when html.Type == HtmlBlockType.Comment => LinqClassificationTypes.MarkdownComment,
                HtmlBlock or HtmlInline or HtmlEntityInline => LinqClassificationTypes.MarkdownHtml,
                _ => null,
            };
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _document.Parsed -= ReParse;
                _document.Dispose();
            }

            _isDisposed = true;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
    }
}
