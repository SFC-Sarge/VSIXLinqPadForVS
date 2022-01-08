using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace VSIXLinqPadForVS
{
    internal static class LinqClassificationTypes
    {
        public const string MarkdownBold = "md_bold2";
        public const string MarkdownItalic = "md_italic2";
        public const string MarkdownStrikethrough = "md_strikethrough";
        public const string MarkdownHeader = "md_header2";
        public const string MarkdownCode = "md_code2";
        public const string MarkdownQuote = "md_quote2";
        public const string MarkdownHtml = "md_html2";
        public const string MarkdownLink = "md_link";
        public const string MarkdownComment = PredefinedClassificationTypeNames.Comment;

        [Export, Name(MarkdownBold)]
        [BaseDefinition(PredefinedClassificationTypeNames.Text)]
        public static ClassificationTypeDefinition LinqClassificationBold { get; set; }

        [Export, Name(MarkdownItalic)]
        [BaseDefinition(PredefinedClassificationTypeNames.Text)]
        public static ClassificationTypeDefinition LinqClassificationItalic { get; set; }

        [Export, Name(MarkdownStrikethrough)]
        [BaseDefinition(PredefinedClassificationTypeNames.Text)]
        public static ClassificationTypeDefinition LinqClassificationStrikethrough { get; set; }

        [Export, Name(MarkdownHeader)]
        [BaseDefinition(PredefinedClassificationTypeNames.SymbolDefinition)]
        public static ClassificationTypeDefinition LinqClassificationHeader { get; set; }

        [Export, Name(MarkdownCode)]
        [BaseDefinition(PredefinedClassificationTypeNames.Text)]
        public static ClassificationTypeDefinition LinqClassificationCode { get; set; }

        [Export, Name(MarkdownQuote)]
        [BaseDefinition(PredefinedClassificationTypeNames.Text)]
        public static ClassificationTypeDefinition LinqClassificationQuote { get; set; }

        [Export, Name(MarkdownHtml)]
        [BaseDefinition(PredefinedClassificationTypeNames.MarkupNode)]
        public static ClassificationTypeDefinition LinqClassificationHtml { get; set; }

        [Export, Name(MarkdownLink)]
        [BaseDefinition(PredefinedClassificationTypeNames.Text)]
        public static ClassificationTypeDefinition LinqClassificationLink { get; set; }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LinqClassificationTypes.MarkdownBold)]
    [Name(LinqClassificationTypes.MarkdownBold)]
    internal sealed class LinqBoldFormatDefinition : ClassificationFormatDefinition
    {
        public LinqBoldFormatDefinition()
        {
            IsBold = true;
            DisplayName = "Markdown Bold";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LinqClassificationTypes.MarkdownItalic)]
    [Name(LinqClassificationTypes.MarkdownItalic)]
    internal sealed class LinqItalicFormatDefinition : ClassificationFormatDefinition
    {
        public LinqItalicFormatDefinition()
        {
            IsItalic = true;
            DisplayName = "Markdown Italic";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LinqClassificationTypes.MarkdownStrikethrough)]
    [Name(LinqClassificationTypes.MarkdownStrikethrough)]
    internal sealed class LinqStrikethroughFormatDefinition : ClassificationFormatDefinition
    {
        public LinqStrikethroughFormatDefinition()
        {
            TextDecorations = new TextDecorationCollection()
            {
                new TextDecoration(){ Location = TextDecorationLocation.Strikethrough }
            };
            DisplayName = "Markdown Strikethrough";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LinqClassificationTypes.MarkdownHeader)]
    [Name(LinqClassificationTypes.MarkdownHeader)]
    [UserVisible(true)]
    internal sealed class LinqHeaderFormatDefinition : ClassificationFormatDefinition
    {
        public LinqHeaderFormatDefinition()
        {
            IsBold = true;
            DisplayName = "Markdown Header";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LinqClassificationTypes.MarkdownCode)]
    [Name(LinqClassificationTypes.MarkdownCode)]
    [UserVisible(true)]
    internal sealed class LinqCodeFormatDefinition : ClassificationFormatDefinition
    {
        public LinqCodeFormatDefinition()
        {
            FontTypeface = new Typeface("Courier New");
            DisplayName = "Markdown Code";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LinqClassificationTypes.MarkdownQuote)]
    [Name(LinqClassificationTypes.MarkdownQuote)]
    [UserVisible(true)]
    internal sealed class LinqQuoteFormatDefinition : ClassificationFormatDefinition
    {
        public LinqQuoteFormatDefinition()
        {
            BackgroundColor = Colors.LightGray;
            BackgroundOpacity = .4;
            DisplayName = "Markdown Quote";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LinqClassificationTypes.MarkdownHtml)]
    [Name(LinqClassificationTypes.MarkdownHtml)]
    [UserVisible(true)]
    internal sealed class LinqHtmlFormatDefinition : ClassificationFormatDefinition
    {
        public LinqHtmlFormatDefinition()
        {
            DisplayName = "Markdown HTML";
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = LinqClassificationTypes.MarkdownLink)]
    [Name(LinqClassificationTypes.MarkdownLink)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    internal sealed class LinqLinkFormatDefinition : ClassificationFormatDefinition
    {
        public LinqLinkFormatDefinition()
        {
            TextDecorations = new TextDecorationCollection()
            {
                new TextDecoration(){ Location = TextDecorationLocation.Underline, PenOffset = 2 }
            };
            DisplayName = "Markdown Link";
        }
    }
}
