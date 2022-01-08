using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Editor.DragDrop;
using Microsoft.VisualStudio.Utilities;

namespace VSIXLinqPadForVS
{
    [Export(typeof(IDropHandlerProvider))]
    [DropFormat("CF_VSSTGPROJECTITEMS")]
    [DropFormat("FileDrop")]
    [Name("IgnoreDropHandler")]
    [ContentType(Constants.LanguageName)]
    [Order(Before = "DefaultFileDropHandler")]
    internal class LinqDropHandlerProvider : IDropHandlerProvider
    {
        public IDropHandler GetAssociatedDropHandler(IWpfTextView view) =>
            view.Properties.GetOrCreateSingletonProperty(() => new LinqDropHandler(view));
    }
}