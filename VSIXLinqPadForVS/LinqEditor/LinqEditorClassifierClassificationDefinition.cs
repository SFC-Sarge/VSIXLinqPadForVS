using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

using System.ComponentModel.Composition;

namespace VSIXLinqPadForVS
{
    /// <summary>
    /// Classification type definition export for EditorClassifier1
    /// </summary>
    internal static class LinqEditorClassifierClassificationDefinition
    {
        // This disables "The field is never used" compiler's warning. Justification: the field is used by MEF.
#pragma warning disable 169

        /// <summary>
        /// Defines the "EditorClassifier1" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("LinqEditorClassifier")]
        private static ClassificationTypeDefinition typeDefinition;

#pragma warning restore 169
    }
}
