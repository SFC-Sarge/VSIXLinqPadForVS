using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

using System.ComponentModel.Composition;

namespace VSIXLinqPadForVS
{
    /// <summary>
    /// Classification type definition export for LinqEditorClassifier
    /// </summary>
    internal static class LinqEditorClassifierClassificationDefinition
    {
        public const string LinqBold = "linq_bold";

        // This disables "The field is never used" compiler's warning. Justification: the field is used by MEF.
#pragma warning disable 169

        /// <summary>
        /// Defines the "LinqEditorClassifier" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("LinqEditorClassifier")]
        private static ClassificationTypeDefinition typeDefinition;

        [Export]
        [FileExtension(".linq")]
        [ContentType("CSharp")]
        internal static FileExtensionToContentTypeDefinition LinqFileExtensionDefinition { get; set; }

        [Export, Name(LinqBold)]
        [BaseDefinition(PredefinedClassificationTypeNames.Text)]
        public static ClassificationTypeDefinition LinqClassificationBold { get; set; }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = LinqEditorClassifierClassificationDefinition.LinqBold)]
        [Name(LinqEditorClassifierClassificationDefinition.LinqBold)]
        internal sealed class LinqBoldFormatDefinition : ClassificationFormatDefinition
        {
            public LinqBoldFormatDefinition()
            {
                IsBold = true;
                DisplayName = "Linq Bold";
            }
        }

#pragma warning restore 169
    }
}
