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
    public static class LinqEditorClassifierClassificationDefinition
    {
        public const string LinqBold = "linq_bold";
        public const string LinqItalic = "linq_italic";
        public const string LinqComment = PredefinedClassificationTypeNames.Comment;


        // This disables "The field is never used" compiler's warning. Justification: the field is used by MEF.
#pragma warning disable 169

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("LinqEditorClassifier")]
        [FileExtension(".linq")]
        [ContentType("CSharp")]
        public static FileExtensionToContentTypeDefinition LinqFileExtensionDefinition { get; set; }

        [Export, Name(LinqBold)]
        [BaseDefinition(PredefinedClassificationTypeNames.Text)]
        public static ClassificationTypeDefinition LinqClassificationBold { get; set; }

        [Export, Name(LinqItalic)]
        [BaseDefinition(PredefinedClassificationTypeNames.Text)]
        public static ClassificationTypeDefinition LinqClassificationItalic { get; set; }


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

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = LinqEditorClassifierClassificationDefinition.LinqItalic)]
        [Name(LinqEditorClassifierClassificationDefinition.LinqItalic)]
        internal sealed class LinqItalicFormatDefinition : ClassificationFormatDefinition
        {
            public LinqItalicFormatDefinition()
            {
                IsItalic = true;
                DisplayName = "Linq Italic";
            }
        }


#pragma warning restore 169
    }
}
