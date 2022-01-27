using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

using System.Collections.Generic;

namespace VSIXLinqPadForVS.LinqEditor
{
    internal class LinqEditorClassifier : IClassifier
    {
        private readonly IClassificationType classificationType;

        internal LinqEditorClassifier(IClassificationTypeRegistryService registry)
        {
            classificationType = registry.GetClassificationType("LinqEditorClassifier");
        }

        #region IClassifier

#pragma warning disable 67

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

#pragma warning restore 67

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var result = new List<ClassificationSpan>()
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), this.classificationType)
            };

            return result;
        }

        #endregion
    }
}
