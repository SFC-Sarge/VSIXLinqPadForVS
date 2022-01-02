using Community.VisualStudio.Toolkit;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Imaging;

using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace VSIXLinqPadForVS
{
    public class MyToolWindow : BaseToolWindow<MyToolWindow>
    {
        public override string GetTitle(int toolWindowId) => "LinqPad Dump";

        public override Type PaneType => typeof(Pane);

        public override async Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            Project project = await VS.Solutions.GetActiveProjectAsync();
            ToolWindowMessenger toolWindowMessenger = await Package.GetServiceAsync<ToolWindowMessenger, ToolWindowMessenger>();
            return new MyToolWindowControl(project, toolWindowMessenger);
        }

        [Guid("23e7c5a3-6e43-41e5-81fd-bf2297047dce")]
        internal class Pane : ToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
            }
        }
    }
}