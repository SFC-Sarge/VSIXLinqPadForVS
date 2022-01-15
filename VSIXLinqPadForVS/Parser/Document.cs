﻿using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSIXLinqPadForVS.Parser
{
    public partial class Document : IDisposable
    {
        private string[] _lines;
        private bool _isDisposed;
        private readonly ITextBuffer _buffer;

        protected Document(string[] lines)
        {
            _lines = lines;
            ProcessAsync().FireAndForget();
        }

        public Document(ITextBuffer buffer)
            : this(buffer.CurrentSnapshot.Lines.Select(line => line.GetTextIncludingLineBreak()).ToArray())
        {
            _buffer = buffer;
            _buffer.Changed += BufferChanged;
            FileName = buffer.GetFileName();

#pragma warning disable VSTHRD104 // Offer async methods
            ThreadHelper.JoinableTaskFactory.Run(async () =>
            {
                Project project = await VS.Solutions.GetActiveProjectAsync();
                ProjectName = project?.Name;
            });
#pragma warning restore VSTHRD104 // Offer async methods
        }

        public bool IsProcessing { get; private set; }

        public string ProjectName { get; protected set; }

        public string FileName { get; protected set; }

        public List<ParseItem> Items { get; private set; } = new();

        public void UpdateLines(string[] lines)
        {
            _lines = lines;
        }

        private void BufferChanged(object sender, TextContentChangedEventArgs e)
        {
            UpdateLines(_buffer.CurrentSnapshot.Lines.Select(line => line.GetTextIncludingLineBreak()).ToArray());
            ProcessAsync().FireAndForget();
        }

        public static Document FromLines(params string[] lines)
        {
            Document doc = new Document(lines);
            return doc;
        }

        public async Task ProcessAsync()
        {
            IsProcessing = true;
            bool success = false;

            await TaskScheduler.Default;

            try
            {
                Parse();
                ValidateDocument();
                success = true;
            }
            catch (Exception ex)
            {
                await ex.LogAsync();
            }
            finally
            {
                IsProcessing = false;

                if (success)
                {
                    Processed?.Invoke(this);
                }
            }
        }

        public ParseItem FindItemFromPosition(int position)
        {
            ParseItem item = Items.LastOrDefault(t => t.Span.Contains(position));
            ParseItem reference = item?.References.FirstOrDefault(v => v != null && v.Span.Contains(position - 1));

            // Return the reference if it exist; otherwise the item
            return reference ?? item;
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                if (_buffer != null)
                {
                    _buffer.Changed -= BufferChanged;
                }
            }

            _isDisposed = true;
        }

        public event Action<Document> Processed;
    }

    public static class DocumentExtensions
    {
        public static Document GetDocument(this ITextBuffer buffer)
        {
            return buffer.Properties.GetOrCreateSingletonProperty(() => new Document(buffer));
        }
    }
}
