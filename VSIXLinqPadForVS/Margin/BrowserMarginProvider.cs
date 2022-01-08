using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace VSIXLinqPadForVS
{
    [Export(typeof(IWpfTextViewMarginProvider))]
    [Name(nameof(LinqPreviewMarginProvider))]
    [Order(After = PredefinedMarginNames.RightControl)]
    [MarginContainer(PredefinedMarginNames.Right)]
    [ContentType(Constants.LanguageName)]
    [TextViewRole(PredefinedTextViewRoles.Debuggable)] // This is to prevent the margin from loading in the diff view
    public class LinqPreviewMarginProvider : IWpfTextViewMarginProvider
    {
        public IWpfTextViewMargin CreateMargin(IWpfTextViewHost wpfTextViewHost, IWpfTextViewMargin marginContainer)
        {
            if (!AdvancedOptions.Instance.EnablePreviewWindow)
            {
                return null;
            }

            return wpfTextViewHost.TextView.Properties.GetOrCreateSingletonProperty(() => new BrowserMargin(wpfTextViewHost.TextView));
        }
    }
}
