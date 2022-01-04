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
        public override string GetTitle(int toolWindowId) => "My Linq Query Tool Window";

        public override Type PaneType => typeof(Pane);

        public override async Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            Project project = await VS.Solutions.GetActiveProjectAsync();
            ToolWindowMessenger toolWindowMessenger = await Package.GetServiceAsync<ToolWindowMessenger, ToolWindowMessenger>();
            return new MyToolWindowControl(project, toolWindowMessenger);
        }

        [Guid("a3ac07c0-309a-4679-bf3b-a2de12944d66")]
        internal class Pane : ToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
                ToolBar = new CommandID(PackageGuids.VSIXLinqPadForVS, PackageIds.TWindowToolbar);

            }
        }
    }
}