using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

using System.ComponentModel.Composition;
using System.Windows.Media;

namespace VSIXLinqPadForVS.LinqEditor
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "LinqEditorClassifier")]
    [Name("LinqEditorClassifier")]
    [UserVisible(true)] // This should be visible to the end user
    [Order(Before = Priority.Default)] // Set the priority to be after the default classifiers
    internal sealed class LinqEditorClassifierFormat : ClassificationFormatDefinition
    {
        public LinqEditorClassifierFormat()
        {
            this.DisplayName = "LinqEditorClassifier"; // Human readable version of the name
            //this.BackgroundColor = Colors.BlueViolet;
            //this.TextDecorations = System.Windows.TextDecorations.Underline;
        }
    }
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "LinqEditorClassifier")]
    [Name("MarkerFormatDefinition/HighlightWordFormatDefinition")]
    [UserVisible(true)]
    internal class HighlightWordFormatDefinition : MarkerFormatDefinition
    {
        public HighlightWordFormatDefinition()
        {
            this.BackgroundColor = Colors.DarkBlue;
            this.ForegroundColor = Colors.LightBlue;
            this.DisplayName = "Highlight Word";
            this.ZOrder = 5;
        }
    }
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "LinqEditorClassifier")]
    [Name("MarkerFormatDefinition/RedMarkerFormatDefinition")]
    [UserVisible(true)]
    internal class RedMarkerFormatDefinition : MarkerFormatDefinition
    {
        public RedMarkerFormatDefinition()
        {
            this.BackgroundColor = Colors.Red;
            this.ForegroundColor = Colors.Blue;
            this.DisplayName = "Red Marker"; //this value should be localized
            this.ZOrder = 5;
        }
    }


}
