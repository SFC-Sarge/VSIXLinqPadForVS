using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

using System.ComponentModel.Composition;

namespace VSIXLinqPadForVS.LinqEditor
{
    [Export(typeof(IClassifierProvider))]
    [ContentType(Constants.LinqLanguageName)] // This classifier applies to all Linq Language files.
    internal class LinqEditorClassifierProvider : IClassifierProvider
    {
        // Disable "Field is never assigned to..." compiler's warning. Justification: the field is assigned by MEF.
#pragma warning disable 649

        [Import]
        private IClassificationTypeRegistryService classificationRegistry;

#pragma warning restore 649

        #region IClassifierProvider

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            return buffer.Properties.GetOrCreateSingletonProperty<LinqEditorClassifier>(creator: () => new LinqEditorClassifier(this.classificationRegistry));
        }

        #endregion
    }
}
