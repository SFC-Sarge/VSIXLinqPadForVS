using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace VSIXLinqPadForVS
{
    internal static class FileAndContentTypeDefinitions
    {
        /// <summary>
        /// LINQ Content Type
        /// </summary>
        [Export]
        [Name(Constants.LanguageName)]
        [BaseDefinition("CSharp")]
        internal static ContentTypeDefinition linqContentTypeDefinition { get; set; }

        /// <summary>
        /// Linq File extensions
        /// </summary>
        [Export]
        [FileExtension(Constants.FileExtension)]
        [ContentType(Constants.LanguageName)]
        internal static FileExtensionToContentTypeDefinition linqFileExtension { get; set; }
    }

}
